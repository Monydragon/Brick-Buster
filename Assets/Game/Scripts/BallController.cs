using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed;
    public Transform paddleBallPos;
    public bool inPlay;
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!inPlay)
        {
            transform.position = paddleBallPos.position;
        }

        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }

        if(inPlay && Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = paddleBallPos.position;
            rb.velocity = Vector2.zero;
            inPlay = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Void")
        {
            rb.velocity = Vector2.zero;
            inPlay = false;
            EventManager.LivesChanged(-1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var brick = collision.gameObject.GetComponent<BrickController>();
        if (brick != null)
        {
            brick.HitBrick();
        }
    }
}
