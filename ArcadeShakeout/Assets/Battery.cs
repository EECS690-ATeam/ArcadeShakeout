using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public FieldOfView fieldOfView;
    public float timeToAdd = 10f;
    private Rigidbody2D rb;
    private BoxCollider2D batteryCollider;

    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = FindObjectOfType<FieldOfView>();
        rb = GetComponent<Rigidbody2D>();
        batteryCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fieldOfView.AddTime(timeToAdd);
            Destroy(gameObject);
        }
    }
}
