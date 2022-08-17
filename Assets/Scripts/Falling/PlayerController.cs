using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Modificable
    public float jumpForce;
    public float moveSpeed;
    public float jumpCooldown;

    //Privado

    private float jumpTime;

    //Private
    private Rigidbody2D myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        jumpTime = 0;
    }

    void Update()
    {
        //Con el espacio se salta, reseteando la velocidad de caida
        if(Input.GetKeyDown(KeyCode.Space) && jumpTime == 0f)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }

        //Movimiento lateral
        if(Input.GetAxis("Horizontal") < 0)
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, myRigidbody.velocity.y);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
        }

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
        }
    }

}
