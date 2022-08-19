using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [SerializeField] private GameObject hitboxFL;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float hSpeed = 5.0f;
    [SerializeField] private float jumpForce = 25.0f;

    float hInput;

    bool mDown = false;
    bool mUp = false;

    bool moveState = true;

    bool jump = false;
    bool grounded = true;


    void Start()
    {
        if (hitboxFL == null)
            hitboxFL = GameObject.Find("Player").transform.Find("PivotFL").Find("Hitbox").gameObject;

        if (rb2d == null)
            rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(hInput * hSpeed, rb2d.velocity.y);

        if (jump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            jump = false;
            grounded = false;
        }
    }

    void Update()
    {
        // Si aprietas el boton izq del raton
        mDown = Input.GetMouseButtonDown(0);
        mUp = Input.GetMouseButtonUp(0);

        if (moveState)
        {
            // Si aprietas las teclas de movimiento (A/D)
            hInput = Input.GetAxisRaw("Horizontal");

            if (!jump && grounded)
            {
                jump = Input.GetKeyDown(KeyCode.W);
            }
        }

        if (mDown)
        {
            // No te puedes mover al usar la linterna
            hitboxFL.SetActive(true);
            mDown = false;

            hInput = 0;
            jump = false;

            moveState = false;
        }

        if (mUp)
        {
            hitboxFL.SetActive(false);
            mUp = false;

            moveState = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            grounded = true;
    }
}
