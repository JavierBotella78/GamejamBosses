using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]public float speed;
    [SerializeField] private float moverX;

    private void moveBullet()
    {
        transform.position += new Vector3(moverX, 0, 0) * Time.deltaTime * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveBullet();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        checkforDeath(collision);
    }

    private void checkforDeath(Collision2D collision)
    {
        if (collision == null || collision.gameObject.layer != (int)Layers.Paredes) { return; }
        //ImDead?.Invoke();
        gameObject.SetActive(false);
    }
}
