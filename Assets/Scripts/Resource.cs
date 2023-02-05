using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Resource
{
    public Resource(float _amount)
    {
        resourceAmount = _amount;
    }
    
    public float resourceAmount;

    public void add(float _amount)
    {
        resourceAmount += _amount;
        Debug.Log("Added resource!: " + resourceAmount);
    }

    public void subtract(float _amount)
    {
        resourceAmount -= _amount;
    }
}
