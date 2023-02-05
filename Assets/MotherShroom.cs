using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MotherShroom : MonoBehaviour
{
    private Resource resource;

    [SerializeField] private float currentAmount;
    [SerializeField] private float maxAmount;
    [SerializeField] private LayerMask resourceLayer;
    [SerializeField] private int detectRange;

    [SerializeField] private float detectTimer;
    private float timer;
    
    private void Start()
    {
        currentAmount = maxAmount;
    }

    private void Update()
    {
        Detect();
    }

    private void Detect()
    {
        timer -= Time.deltaTime;
        //Debug.Log(timer);
        if (timer < 0)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectRange, resourceLayer);
            if (hits.Length > 0)
            {
                //Debug.Log("Hit resc colliders!");
                foreach (var col in hits)
                {
                    if (col.GetComponent<ResourceComponent>() != null)
                    {
                        Absorb(col.GetComponent<ResourceComponent>());
                    }
                }  
            }

            timer = detectTimer;
        }
    }

    private void Absorb(ResourceComponent _resourceComponent)
    {
        currentAmount += _resourceComponent.GetResource().resourceAmount;
        Destroy(_resourceComponent);

        transform.localScale *= 1 + (currentAmount / 10000);
    }
}

