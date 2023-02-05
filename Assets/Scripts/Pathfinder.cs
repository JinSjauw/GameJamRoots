using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Pathfinder
{
    private const int STRAIGHT_COST = 10;
    private const int DIAGONAL_COST = 14;
    
    private List<ShroomNode> openList;
    private List<ShroomNode> closedList;
    private List<ShroomNode> nodesList = new List<ShroomNode>();

    private ShroomNode currentNode;
    private List<ShroomNode> path;

    public List<ShroomNode> FindPath(ShroomNode startNode, ShroomNode endNode)
    {
        //Debug.Log("In Find Path!, Startnode: " + startNode.name + " EndNode: " + endNode.name);
        //Need to have an list of nodes
        nodesList = LevelManager.Instance.GetNodeList();
        
        foreach (ShroomNode node in nodesList)
        {
            node.GScore = Mathf.Infinity;
            //Debug.Log("Node: " + node.name + node.GScore);
        }
        
        //Debug.Log("Node List: " + nodesList.Count);
        
        openList = new List<ShroomNode> { startNode };
        closedList = new List<ShroomNode>();
        
        startNode.GScore = 0;
        startNode.HScore = CalculateDistance(startNode.position, endNode.position);

        endNode.GScore = Mathf.Infinity;
        
        //Cycle 
        while (openList.Count > 0)
        {
            //Debug.Log("In THE CYCLE");
            ShroomNode currentNode = getLowestFscore(openList);
            //Debug.Log("Current Node: " + currentNode.name + "Endnode: " + endNode.name);
            
            if (currentNode == endNode)
            {
                //Debug.Log("Before ReturnPath! CurrentNode: " + currentNode.name);
                //Debug.Log("Before ReturnPath!  " + openList.Count);
                return ReturnPath(currentNode);
            }
            
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            List<ShroomNode> neighboursList = currentNode.getNeighbours();
            //Debug.Log("Current Node Neighbours: " + neighboursList.Count);
            int count = 0;
            foreach (ShroomNode neighbourNode in neighboursList)
            {
                float tentativeGCost = currentNode.GScore + CalculateDistance(currentNode.position, neighbourNode.position);
                //Debug.Log("Current Neigbour: " + neighbourNode.name);
                //Debug.Log("Tentative GCost: " + tentativeGCost + " Neighbour GScore: " + neighbourNode.GScore);
                count++;
                if (tentativeGCost < neighbourNode.GScore)
                {
                    neighbourNode.parent = currentNode;
                    neighbourNode.GScore = tentativeGCost;
                    neighbourNode.HScore = CalculateDistance(neighbourNode.position, endNode.position);

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                        //Debug.Log("Added Neighbour to the openList! " + openList.Count);
                    }
                }
            }
        }
    //Debug.Log("No Path Found! " + openList.Count);
    return null;
    }
    
    private List<ShroomNode> ReturnPath(ShroomNode endNode)
    {
        path = new List<ShroomNode>();
        ShroomNode currentNode = endNode;
        path.Add(endNode);
        while (currentNode.parent != null)
        {
            path.Add(currentNode.parent);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        //Debug.Log("Path Count: " + path.Count);
        return path;
    }

    private ShroomNode getLowestFscore(List<ShroomNode> _nodeList)
    {
        if (_nodeList.Count < 0)
        {
            //Debug.Log("List is empty || in getLowestFscore()");
            return null;
        }
        
        ShroomNode lowestFcostNode = _nodeList.First();
        foreach (ShroomNode node in _nodeList)
        {
            if (node.FScore < lowestFcostNode.FScore)
            {
                lowestFcostNode = node;
            }
        }
        return lowestFcostNode;
    }
    
    private float CalculateDistance(Vector2 a, Vector2 b)
    {
        float distanceX = Math.Abs(a.x - b.x);
        float distanceY = Math.Abs(a.y - b.y);
        float remaining = Math.Abs(distanceX - distanceY);
        
        return DIAGONAL_COST * Mathf.Min(distanceX, distanceY) + STRAIGHT_COST * remaining;
    }
}
