using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ShroomNode : MonoBehaviour
{
    public Vector2 position;
    public ShroomNode parent = null; //Parent Node of this node << this is the node that was looking before.
    public float FScore { //GScore + HScore
        get { return GScore + HScore; }
    }
    public float GScore; //Current Travelled Distance << distance traveled from the startNode. +1 for every cell traveled
    public float HScore; //Distance estimated based on Heuristic << Euclidean distance to endNode
    
    //The closest Shroom that goes back to the MotherShroom
    [SerializeField] private bool isMother;
    [SerializeField] private float connectionRange;
    [SerializeField] private LayerMask nodeLayer;
    [SerializeField] private List<ShroomNode> neighbourList;

    // Start is called before the first frame update
    void Start()
    {
        position = new Vector2(transform.position.x, transform.position.y);
        neighbourList = new List<ShroomNode>();
        setNeighbours();
    }

    private void setNeighbours()
    {
        //sphere overlap with radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, connectionRange, nodeLayer);
        //Debug.Log(hits.Length);
        
        foreach (Collider2D node in hits )
        {
            ShroomNode neighbour = node.GetComponent<ShroomNode>();
            
            if (neighbour == this)
            {
                continue;
            }
            
            if (!ContainsNeighbour(this) && !neighbour.isMother)
            {
                neighbour.AddNeighbour(this);
            }
            neighbourList.Add(neighbour);
        }
    }
    public void AddNeighbour(ShroomNode _neighbour)
    {
        neighbourList.Add(_neighbour);
    }

    public bool ContainsNeighbour(ShroomNode _neighbour)
    {
        return neighbourList.Contains(_neighbour);
    }
    
    public List<ShroomNode> getNeighbours()
    {
        return neighbourList;
    }
}
