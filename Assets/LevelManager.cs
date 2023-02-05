using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private int spawnCounter;
    private void Awake()
    {
        Instance = this;
    }

    private List<ShroomNode> nodeList = new List<ShroomNode>();

    public void SpawnShroom(GameObject _prefab, Vector2 _position)
    {
        ShroomNode node = Instantiate(_prefab, _position, Quaternion.identity).GetComponent<ShroomNode>();
        node.gameObject.name = gameObject.name + " ID: " + spawnCounter;
        nodeList.Add(node);
        spawnCounter++;
    }

    public List<ShroomNode> GetNodeList()
    {
        return nodeList;
    }
}
