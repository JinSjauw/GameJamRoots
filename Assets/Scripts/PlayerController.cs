using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Data")] 
    [SerializeField] private float maxInitialHP;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isProtected;
    private Resource playerHealth;
    private bool canMove = true;

    [Header("Action Data")] [SerializeField]
    private GameObject poopPrefab;
    [SerializeField] private float poopCost;
    
    [Header("Object Data")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D rigidBody;
    private Vector2 movementVector;
    
    public void Move(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        { 
            movementVector = _context.ReadValue<Vector2>();
            //flip sprite
            if (movementVector.x < 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
            
        }else if (_context.canceled)
        {
            movementVector = Vector3.zero;
            animator.SetBool("isMoving", false);
        }
    }

    public void Poop(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            //Play poop anim
            HandlePoop();
        }
    }
    
    private void Start()
    {
        playerHealth = new Health(maxInitialHP);
    }
    private void Update()
    {
        //Move
        HandleMove();
    }
    
    private void HandleMove()
    {
        if(!canMove){ return; }
        animator.SetBool("isMoving", true);
        rigidBody.velocity = movementVector * moveSpeed * Time.fixedDeltaTime;
    }

    private void HandlePoop()
    {
        canMove = false;
        animator.SetTrigger("Poop");
        LevelManager.Instance.SpawnShroom(poopPrefab, transform.position);
        playerHealth.subtract(poopCost);
    }

    public Resource GetPlayerHealth()
    {
        return playerHealth;
    }
    
    public void SetMoveState(int _state)
    {
        if (_state == 1)
        {
            canMove = true;
            
        }
        else if (_state == 0)
        {
            canMove = false;
        }
    }

    public void SetProtection(bool _state)
    {
        isProtected = _state;
    }
}
