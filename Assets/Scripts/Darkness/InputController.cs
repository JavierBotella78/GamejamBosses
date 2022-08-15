using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float hSpeed = 5.0f;
    [SerializeField] private float jumpForce = 25.0f;


    float hInput;
    bool jump = false;
    bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");

        if (!jump && grounded)
        {
            jump = Input.GetKeyDown(KeyCode.W);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
            grounded = true;
    }
}
