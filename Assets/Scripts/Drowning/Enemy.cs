using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BulletScriptableObject _bullet;

    public Action<Vector2, float, float> OnAttack;

    private void Start()
    {
        InvokeRepeating(nameof(Disparar), 1f, 1f);
    }

    private void Disparar()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        OnAttack?.Invoke(position, _bullet.GetSpeed, _bullet.GetDamage);
    }
}
