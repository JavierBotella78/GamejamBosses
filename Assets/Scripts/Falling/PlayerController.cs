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
    private float jumpTimeRemaining;

    //Private
    private Rigidbody2D myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        jumpTimeRemaining = 0f;
    }

    void Update()
    {
        //Con el espacio se salta, reseteando la velocidad de caida
        if(Input.GetKeyDown(KeyCode.Space) && jumpTimeRemaining <= 0f)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            //jumpTimeRemaining += 0.2f;
            StartCoroutine(StartJumpCooldown());
            
        }

        //Movimiento lateral
        if (Input.GetAxis("Horizontal") < 0)
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

    //Empieza el cooldown y lo baja poco a poco hasta que es 0
    private IEnumerator StartJumpCooldown()
    {
        for(jumpTimeRemaining = jumpCooldown; jumpTimeRemaining > 0f; jumpTimeRemaining -= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log(jumpTimeRemaining);
        }
        jumpTimeRemaining = 0f;
    }

}
