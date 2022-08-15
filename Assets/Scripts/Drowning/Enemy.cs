using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BulletScriptableObject _bullet;

    public Action<Vector2, float, float> OnAttack;
    public Action<GameObject, GameObject, float> DamagePlayer;
    public Action<Vector2, int> onPickupGenerate;
    public float maxhp = 50f;
    public float currenthp;

    private void Awake()
    {
        currenthp = maxhp;
    }
    private void Start()
    {
        InvokeRepeating(nameof(Disparar), 1f, 1f);
    }

    private void Disparar()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        OnAttack?.Invoke(position, _bullet.GetSpeed, _bullet.GetDamage);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        checkforDeath(collision);
    }

    protected virtual void checkforDeath(Collider2D collision)
    {
        if (collision == null && collision.gameObject.layer != (int)Layers.Player) { return; }
        DamagePlayer?.Invoke(collision.gameObject, gameObject, -20);
    }

    public void changeLife(float amount)
    {
        currenthp += amount;
        Debug.Log(amount);
        if (currenthp <= 0) {
            currenthp = 0;
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            onPickupGenerate?.Invoke(position, 5);
            disableEnemy();
        }
    }
    protected void disableEnemy()
    {
        CancelInvoke(nameof(Disparar));
        gameObject.SetActive(false);
    }
}
