using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultipler = 1.0f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public Vector2 direction { get; private set; }

    public Vector2 nextDirection { get; private set; }

    public Rigidbody2D body { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private void Awake() 
    {
        this.body = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
    }

    private void Start() 
    {
        ResetState();
    }

    public void ResetState() 
    {
        this.speedMultipler = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = this.startingPosition;
        this.GetComponent<Rigidbody2D>().isKinematic = false;
        this.enabled = true;
    }

    private void Update() 
    {
        if(this.nextDirection != Vector2.zero) 
        {
            SetDirection(this.nextDirection);
        }
    }

    private void FixedUpdate() {
        Vector2 position = this.GetComponent<Rigidbody2D>().position;
        Vector2 translation = this.direction * this.speed * this.speedMultipler * Time.fixedDeltaTime;
        this.GetComponent<Rigidbody2D>().MovePosition(position + translation);

    }

    public void SetDirection(Vector2 direction, bool forced = false) 
    {
        if (forced || !Occupied(direction)) 
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }
        else 
        {
            this.nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        //RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 32.0f, 0.0f, direction, 84.0f, this.obstacleLayer);
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * .75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }
 }

