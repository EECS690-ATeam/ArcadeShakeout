using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private FieldOfView fieldOfView;

    public static event Action OnPlayerDeath;
    public float moveSpeed = 150f;
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private Animator animator;
    private bool isWalking;
    Vector2 movement;
    public UIManager playerUI;
    //public Timer timeSurvived;

    // Called before Start()
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        movement = rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        // Set player facing direction
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );
        transform.up = direction * -1;

        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Calculate and normalize player movement
        rb.velocity = new Vector2(movement.x, movement.y);
        rb.velocity = rb.velocity.normalized * moveSpeed;

        // Set aim direction
        Vector3 targetPosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDir = (targetPosition - transform.position).normalized;
        fieldOfView.SetAimDirection(aimDir);
        fieldOfView.SetOrigin(transform.position);

        // Animate walking only when walking
        isWalking = rb.velocity != Vector2.zero;
        animator.SetBool("IsWalking", isWalking);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision happened");

        if (collision.gameObject.CompareTag("Skeleton"))
        {
            // Call game over screen
            OnPlayerDeath?.Invoke();
            // Player dies
            Destroy(gameObject);
        }
    }
}
