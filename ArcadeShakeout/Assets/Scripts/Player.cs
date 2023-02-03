using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Player : MonoBehaviour
{
    [SerializeField] private FieldOfView fieldOfView;

    public float moveSpeed = 150f;
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private Animator animator;
    private bool isWalking;
    Vector2 movement;

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
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(movement.x, movement.y);
        rb.velocity = rb.velocity.normalized * moveSpeed;

        Vector3 targetPosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDir = (targetPosition - transform.position).normalized;
        fieldOfView.SetAimDirection(aimDir);
        fieldOfView.SetOrigin(transform.position);

        isWalking = rb.velocity != Vector2.zero;
        animator.SetBool("IsWalking", isWalking);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision happened");

        if (collision.gameObject.CompareTag("Skeleton"))
        {
            // Player dies
            Destroy(gameObject);
        }
    }
}
