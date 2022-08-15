using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Layers : int
{
    Paredes = 8
};
public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnEnable() { Subscription(true); }
    private void OnDisable() { Subscription(false); }
    private void Subscription(bool subscribe)
    {
        if (subscribe)
        {
            suscribePlayerEvents();
        }
        else
        {
            unsuscribePlayerEvents();
        }
    }

    private void suscribePlayerEvents()
    {
        _player.OnSpaceKeyPressed += generateBullet;
    }

    private void unsuscribePlayerEvents()
    {
        _player.OnSpaceKeyPressed -= generateBullet;
    }

    private void generateBullet(Vector2 position, GameObject bala, float speed)
    {
        Vector2 bulletPosition = new Vector2(position.x, position.y);

        GameObject bullet = Instantiate(bala);
        bullet.transform.localPosition = new Vector3(bulletPosition.x, bulletPosition.y, 0);
        bullet.GetComponent<Bullet>().speed = speed;

    }
}
