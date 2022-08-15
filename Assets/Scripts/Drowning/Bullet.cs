using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float damage;

    [SerializeField] protected float moverX;
    private BoxCollider2D _ownCollider;


    public Action<GameObject> ImDead;
    public Action<GameObject, GameObject, float> DamageEntity;

    private void Awake()
    {
        initComponentes();
    }

    private void OnEnable()
    {
        _ownCollider.enabled = true;
    }

    private void initComponentes()
    {
        TryGetComponent(out _ownCollider);
    }
    protected void moveBullet()
    {
        transform.position += new Vector3(moverX, 0, 0) * Time.deltaTime * speed;
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        moveBullet();
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        checkforDeath(collision);
    }

    protected virtual void checkforDeath(Collider2D collision)
    {
        if (collision == null && (collision.gameObject.layer != (int)Layers.Paredes || collision.gameObject.layer != (int)Layers.Enemigo)) { return; }
        if (collision.gameObject.layer == (int)Layers.Paredes) { ImDead?.Invoke(gameObject); return; }
        DamageEntity?.Invoke(collision.gameObject, gameObject, damage);
        disableBullet();
    }

    protected void disableBullet()
    {
        _ownCollider.enabled = false;
        gameObject.SetActive(false);
    }
}
