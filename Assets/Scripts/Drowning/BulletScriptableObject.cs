using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletScriptableObject : ScriptableObject
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;


    public GameObject GetBullet => _bullet;
    public float GetSpeed => _speed;
    public void SetSpeed(float speed) => _speed = speed;
    public float GetDamage => _damage;
    public void SetDamage(float damage) => _damage = damage;

}
