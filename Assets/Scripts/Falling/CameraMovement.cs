using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform camaraTransform;
    public Transform playerTransform;
    public GameObject myPlayerGo;
    private Rigidbody2D myplayerSpeed;
    private Vector3 lerpeado;

    void Start()
    {
        //Cojemos la cámara y su transform
        camaraTransform = transform;
        myplayerSpeed = myPlayerGo.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //La posición x e y de la cámara va a seguir a la del jugador en todo momento, la z es la de la camara siempre
        //camaraTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, camaraTransform.position.z);
        lerpeado = Vector3.Lerp(camaraTransform.position, playerTransform.position, 0.3f*Time.fixedDeltaTime *(-1*myplayerSpeed.velocity.y));
        lerpeado.z = camaraTransform.position.z;
        camaraTransform.position = lerpeado;
        Debug.Log(myplayerSpeed.velocity.y);
    }
}
