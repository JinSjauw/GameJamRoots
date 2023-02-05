using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;using Random = UnityEngine.Random;

enum TypeResource
{
    HEALTH = 0,
    WATER = 1,
}

public class ResourceContainer : MonoBehaviour
{
    [SerializeField] private float spawnCostMin, spawnCostMax;
    [SerializeField] private float spawnDelay;
    private float spawnTimer;
    private int spawnCounter;

    [SerializeField] private float connectionRange;
    [SerializeField] private LayerMask nodeLayer;

    [SerializeField] private TypeResource resourceType;
    [SerializeField] private float maxAmount;
    [SerializeField] private float currentAmount;
    [SerializeField] private GameObject resourcePrefab;
    [SerializeField] private ShroomNode endNode;
    [SerializeField] private List<ShroomNode> path = new List<ShroomNode>();

    private void Start()
    {
        endNode = GameObject.FindWithTag("Mother").GetComponent<ShroomNode>();
        //Get the relevant resource
        spawnTimer = spawnDelay;
        currentAmount = maxAmount;
    }

    private void Update()
    {
        DetectShroom();
    }

    private void Spawn()
    {
        GameObject resourceObject = Instantiate(resourcePrefab, 
            new Vector3(transform.position.x + Random.Range(-.5f, .5f), transform.position.y + Random.Range(-.5f, .5f), 0),
            Quaternion.identity);
        float spawnCost = Random.Range(spawnCostMin, spawnCostMax);

        if (currentAmount - spawnCost < 0)
        {
            //Die;
        }
        currentAmount -= spawnCost;
        
        resourceObject.name = resourceObject.name + "ID: " + spawnCounter;
        spawnCounter++;
        ResourceComponent resourceComponent = resourceObject.GetComponent<ResourceComponent>();

        Resource resource = new Health(0);
        
        if (resourceType == TypeResource.WATER)
        {
            resource = new Water(spawnCost);
        }
        else if (resourceType == TypeResource.HEALTH)
        {
            resource = new Health(spawnCost);
        }
        
        resourceComponent.setResource(resource);
        resourceComponent.SetPath(path);
    }
    
    public void DetectShroom()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, connectionRange, nodeLayer);
        if (hit.Length > 0)
        {
           
            spawnTimer -= Time.fixedDeltaTime;
            if (spawnTimer < 0)
            {
                ShroomNode node = hit[0].GetComponent<ShroomNode>();
                if (path.Count <= 0)
                {
                    Pathfinder pathfinder = new Pathfinder();
                    path = pathfinder.FindPath(node, endNode);
                }
                Spawn();
                spawnTimer = spawnDelay;
                
            }
        }
        //Give start node to spawned resource
    }
}
