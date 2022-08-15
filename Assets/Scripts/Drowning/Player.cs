using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // VARIABLES
    public float speed = 8;
    private Vector2 mover;

    [SerializeField] private BulletScriptableObject _bullet;


    public Action<Vector2, GameObject, float> OnSpaceKeyPressed;

    private void movePlayer()
    {
        mover.x = Input.GetAxisRaw("Horizontal") ;
        mover.y = Input.GetAxisRaw("Vertical") * -1;

        transform.position += new Vector3(mover.y, mover.x, 0) * Time.deltaTime * speed;
    }

    private void inputPlayer()
    {
        checkShootButtonPressed();
    }

    private void checkShootButtonPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Jump"))
        {
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            OnSpaceKeyPressed?.Invoke(position, _bullet.GetBullet, _bullet.GetSpeed);
        }
    }

    private void Update()
    {
        inputPlayer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movePlayer();
    }
}
