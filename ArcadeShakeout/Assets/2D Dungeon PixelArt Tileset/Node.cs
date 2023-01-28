using UnityEngine;
using System.Collections.Generic;

public class Node : MonoBehaviour
{

    public LayerMask obstacleLayer;

    public List<Vector2> availableDirections { get; private set; }

    private void Start() 
    {
        this.availableDirections = new List<Vector2>();
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    private void CheckAvailableDirection(Vector2 direction) 
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 32.0f, 0.0f, direction, 64.0f, this.obstacleLayer);

        if(hit.collider == null) 
        {
            this.availableDirections.Add(direction);
        }
    }
}