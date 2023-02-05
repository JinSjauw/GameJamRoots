using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ResourceComponent : MonoBehaviour
{
    private Resource resource;
    private Pathfinder pathFinder = new Pathfinder();
    [SerializeField] private List<ShroomNode> path;
    private int pathIndex = 0;
    [SerializeField] private float movementSpeed;

    public void SetPath(List<ShroomNode> _path)
    { 
        //Debug.Log("Find Path getting called!");
        path = _path;
        //path = pathFinder.FindPath(_startNode, _endNode);
    }
    
    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        
        if (path == null || path.Count <= 0)
        {
            Debug.Log("Path is null");
            return;
        }
        
        if (Vector2.Distance((Vector2)transform.position, path[pathIndex].position) > 0.01f)
        {
            //Debug.Log("Moving!");
            transform.position = Vector2.MoveTowards(transform.position, path[pathIndex].position, movementSpeed * Time.deltaTime);
        }
        else
        {
            if (pathIndex < path.Count - 1)
            {
                pathIndex++;
            }
        }
    }

    public Resource GetResource()
    {
        return resource;
    }
    
    public void setResource(Resource _resource)
    {
        resource = _resource;
        //Debug.Log("Resource: " + resource.resourceAmount);
    }
}
