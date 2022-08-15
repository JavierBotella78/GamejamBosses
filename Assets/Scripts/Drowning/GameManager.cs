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
    [SerializeField] private GameObject _bullets; //
    [SerializeField] private GameObject uimanager; //

    private List<Bullet> _bulletCmps = new List<Bullet>();

    private void OnEnable() { Subscription(true); }
    private void OnDisable() { Subscription(false); }
    private void Subscription(bool subscribe)
    {
        if (subscribe)
        {
            suscribePlayerEvents();
            suscribeBulletsEvents();
        }
        else
        {
            unsuscribePlayerEvents();
            unsuscribeBulletsEvents();
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

    private void suscribeBulletsEvents()
    {
        if (_bullets == null) { return; }
        for (int i = 0; i < _bullets.transform.childCount; i++)
        {
            Bullet bullet = _bullets.transform.GetChild(i).GetComponent<Bullet>();
            bullet.ImDead += disableBullet;
        }
    }

    private void unsuscribeBulletsEvents()
    {
        if (_bullets == null) { return; }
        for (int i = 0; i < _bullets.transform.childCount; i++)
        {
            Bullet bullet = _bullets.transform.GetChild(i).GetComponent<Bullet>();
            bullet.ImDead -= disableBullet;
        }
    }

    private void disableBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    private void generateBullet(Vector2 position, float speed)
    {
        Vector2 bulletPosition = new Vector2(position.x, position.y);
        for (int i = 0; i < _bullets.transform.childCount; i++)
        {
            GameObject Bullet = _bullets.transform.GetChild(i).gameObject;
            if (Bullet.activeInHierarchy == false) {
                Bullet.SetActive(true);
                Bullet.transform.localPosition = new Vector3(bulletPosition.x, bulletPosition.y, 0);
                _bulletCmps[i].speed = speed;
                break;
            }
        }
        //GameObject bullet = Instantiate(bala);
    }

    private void Awake()
    {
        initComponents();
    }

    private void initComponents()
    {
        if (_bullets == null) { return; }
        for (int i = 0; i < _bullets.transform.childCount; i++)
        {
            Bullet bullet = _bullets.transform.GetChild(i).GetComponent<Bullet>();
            _bulletCmps.Add(bullet);
        }
    }

    /*private void generateBullet(Vector2 position, GameObject bala, float speed)
    {
        Vector2 bulletPosition = new Vector2(position.x, position.y);

        GameObject bullet = Instantiate(bala);
        bullet.transform.localPosition = new Vector3(bulletPosition.x, bulletPosition.y, 0);
        bullet.GetComponent<Bullet>().speed = speed;

    }*/
}
