using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Modificable
    public float jumpForce;

    public float dashForce;
    public float dashFramesActive;

    public float moveSpeed;

    public float jumpCooldown;
    public float dashCooldown;

    public float maxFallingSpeed;
    public float mortalSpeed;
    
    public float pushForce;

    public float smoothMaxFrames;

    //Privado
    private float jumpTimeRemaining;
    private float dashTimeRemaining;

    private float dashActualFrameActive;
    private bool justMoved;
    private float smoothFrames;







    private float rockAngularVelocity;

    private Vector2 velocityBeforeCollision;

    //Private
    private Rigidbody2D myRigidbody;
    private BoxCollider2D launchingHitbox;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        launchingHitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();

        jumpTimeRemaining = 0f;
        rockAngularVelocity = 300f;
        dashActualFrameActive = 0f;

        justMoved = false;
        smoothFrames = 0;
    }

    void Update()
    {

        //TECLAS
        if(Input.GetKeyDown(KeyCode.Space) && dashTimeRemaining <= 0f && Input.GetAxisRaw("Horizontal") != 0)
        {
            //Dash izquierdo
            if(Input.GetAxisRaw("Horizontal") < 0)
            {
                myRigidbody.velocity = new Vector2(-dashForce, myRigidbody.velocity.y);
                StartCoroutine(StartDashCooldown());
                StartCoroutine(DashAction(-dashForce));
            }

            //Dash derecho
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                myRigidbody.velocity = new Vector2(dashForce, myRigidbody.velocity.y);
                StartCoroutine(StartDashCooldown());
                StartCoroutine(DashAction(dashForce));
            }
        }
        else
        //Con el espacio se salta, reseteando la velocidad de caida
        if(Input.GetKeyDown(KeyCode.Space) && jumpTimeRemaining <= 0f)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            StartCoroutine(StartJumpCooldown());

            launchingHitbox.enabled = true;
            StartCoroutine(StartLaunchCooldown());
        }

        //Movimiento lateral
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x - moveSpeed*Time.deltaTime, myRigidbody.velocity.y);
            justMoved = true;
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x + moveSpeed*Time.deltaTime, myRigidbody.velocity.y);
            justMoved = true;
        }

        //No se hace por ahora
        //Cuando no estoy tocando el movimiento, me acabo de mover y no estoy dasheando, reduzco mo velocidad lateral a la mitad, así no se desliza por hierlo
        if (Input.GetAxisRaw("Horizontal") == 0 && justMoved && dashActualFrameActive == 0f)
        {
            Debug.Log("Parado");
            //myRigidbody.velocity = new Vector2(myRigidbody.velocity.x / 2, myRigidbody.velocity.y);
            StartCoroutine(SmoothMovement());
            justMoved = false;
        }


    }

    private void FixedUpdate()
    {
        //Limitamos la velocidad de caida
        if (myRigidbody.velocity.y < maxFallingSpeed) myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, maxFallingSpeed);

        //Y guardamos cada frame la velocidad del objeto, para saber que velocidad tenia al colisionar
        velocityBeforeCollision = myRigidbody.velocity;
    }




    //-----COLISIONES Y TRIGGERS-----

    //Si impulsa una roca, le mete velocidad
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Solo si el objeto es launchable
        if (collision.gameObject.layer == 14 )
        {
            //Debug.Log("Colisionado");
            Vector2 rockVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
            rockVelocity.y = pushForce;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = rockVelocity;

            collision.gameObject.GetComponent<Rigidbody2D>().angularVelocity = rockAngularVelocity;
        }
    }

    //Si se choca con un obstaculo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Layer de los obstaculos
        if(collision.gameObject.layer == 15)
        {
            Debug.Log(velocityBeforeCollision.y);
            //Si la velocidad es mayor que la soportada, nos morimos
            if(velocityBeforeCollision.y <= mortalSpeed)
            {
                playerDeath();
            }

        }
    }


    //-----FUNCIONES PROPIAS-----

    private void playerDeath()
    {
        Debug.Log("Me he muerto");
    }

    //-----CORRUTINAS-----

    //Empieza el cooldown y lo baja poco a poco hasta que es 0
    private IEnumerator StartJumpCooldown()
    {
        for(jumpTimeRemaining = jumpCooldown; jumpTimeRemaining > 0f; jumpTimeRemaining -= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            //Debug.Log(jumpTimeRemaining);
        }
        jumpTimeRemaining = 0f;
    }

    private IEnumerator StartLaunchCooldown()
    {
        yield return new WaitForSeconds(.1f);
        launchingHitbox.enabled = false;
    }

    private IEnumerator StartDashCooldown()
    {
        for (dashTimeRemaining = dashCooldown; dashTimeRemaining > 0f; dashTimeRemaining -= 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            //Debug.Log(dashTimeRemaining);
        }
        dashTimeRemaining = 0f;
    }

    private IEnumerator DashAction(float dashf)
    {
        //Le quitamos la gravedad al jugador lo movemos poco a poco hacia un lado
        float beforeGravityScale = myRigidbody.gravityScale;
        myRigidbody.gravityScale = 0;
        myRigidbody.velocity = new Vector2(dashf, myRigidbody.velocity.y);

        //Contamos los frames indicados en DashFramesActive
        for (dashActualFrameActive = dashFramesActive; dashActualFrameActive > 0f; dashActualFrameActive -= 1)
        {
            //yield return new WaitForSeconds(Time.deltaTime);
            yield return new WaitForFixedUpdate();
            //Debug.Log(dashActualFrameActive);
        }
        dashActualFrameActive = 0f;

        //Acabamos dejando al jugador con gravedad y controles
        myRigidbody.gravityScale = beforeGravityScale;
    }

    private IEnumerator SmoothMovement()
    {
        for (smoothFrames = smoothMaxFrames; smoothFrames > 0f; smoothFrames -= 1)
        {
            yield return new WaitForFixedUpdate();
            Debug.Log(smoothFrames);
            if(dashActualFrameActive == 0) myRigidbody.velocity = new Vector2(myRigidbody.velocity.x / 1.075f, myRigidbody.velocity.y);


        }
        smoothFrames = 0f;
    }
}
