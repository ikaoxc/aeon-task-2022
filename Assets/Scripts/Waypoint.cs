using System;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Action Reached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out var character) == false) 
            return;
        
        Reached?.Invoke();
    }
}
