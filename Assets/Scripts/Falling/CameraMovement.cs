using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMovement : MonoBehaviour
{
    private Transform camaraTransform;
    public Transform playerTransform;
    public GameObject myPlayerGo;
    private Rigidbody2D myplayerSpeed;
    private Vector3 lerpeado;

    void Start()
    {
        //Cojemos la c?mara y su transform
        camaraTransform = transform;
        myplayerSpeed = myPlayerGo.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //La posici?n x e y de la c?mara va a seguir a la del jugador en todo momento, la z es la de la camara siempre
        //camaraTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, camaraTransform.position.z);
        lerpeado = Vector3.Lerp( camaraTransform.position, playerTransform.position, 0.3f*Time.fixedDeltaTime * Math.Abs(myplayerSpeed.velocity.y * 3) );
        //La x, z se mantienen igual al de la c?mara base
        lerpeado.z = camaraTransform.position.z;
        lerpeado.x = camaraTransform.position.x;
        camaraTransform.position = lerpeado;
    }
}
