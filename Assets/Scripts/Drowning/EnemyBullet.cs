using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void checkforDeath(Collider2D collision)
    {
        if (collision == null &&  (collision.gameObject.layer != (int)Layers.Paredes || collision.gameObject.layer != (int)Layers.Player)) { return; }
        if(collision.gameObject.layer == (int)Layers.Paredes) { ImDead?.Invoke(gameObject); return; }
        DamageEntity?.Invoke(collision.gameObject, gameObject, damage);
        disableBullet();
    }
}
