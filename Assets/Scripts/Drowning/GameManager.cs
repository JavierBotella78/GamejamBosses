using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Layers : int
{
    Paredes = 8,
    Balas = 9,
    BalaEnemiga = 10,
    PowerUp = 11,
    Player = 12,
    Enemigo = 13
};
public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _bullets; //
    [SerializeField] private GameObject _enemyBullets; //
    [SerializeField] private GameObject _enemies; //
    [SerializeField] private GameObject _pickUps; //

    [SerializeField] private UIManager uimanager; //

    private List<Bullet> _bulletCmps = new List<Bullet>();
    private List<Bullet> _EnemybulletCmps = new List<Bullet>();

    private void OnEnable() { Subscription(true); }
    private void OnDisable() { Subscription(false); }
    private void Subscription(bool subscribe)
    {
        if (subscribe)
        {
            suscribePlayerEvents();
            suscribeBulletsEvents();
            suscribeEnemyBulletsEvents();
            suscribeEnemyEvents();
            suscribePowerUpEvents();
        }
        else
        {
            unsuscribePlayerEvents();
            unsuscribeBulletsEvents();
            unsuscribeEnemyBulletsEvents();
            unsuscribeEnemyEvents();
            unsuscribePowerUpEvents();
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
            bullet.DamageEntity += changeLifeOfEntity;
        }
    }

    private void unsuscribeBulletsEvents()
    {
        if (_bullets == null) { return; }
        for (int i = 0; i < _bullets.transform.childCount; i++)
        {
            Bullet bullet = _bullets.transform.GetChild(i).GetComponent<Bullet>();
            bullet.ImDead -= disableBullet;
            bullet.DamageEntity -= changeLifeOfEntity;
        }
    }

    private void suscribeEnemyBulletsEvents()
    {
        if (_enemyBullets == null) { return; }
        for (int i = 0; i < _enemyBullets.transform.childCount; i++)
        {
            EnemyBullet bullet = _enemyBullets.transform.GetChild(i).GetComponent<EnemyBullet>();
            bullet.ImDead += disableBullet;
            bullet.DamageEntity += changeLifeOfEntity;
        }
    }

    private void unsuscribeEnemyBulletsEvents()
    {
        if (_enemyBullets == null) { return; }
        for (int i = 0; i < _enemyBullets.transform.childCount; i++)
        {
            EnemyBullet bullet = _enemyBullets.transform.GetChild(i).GetComponent<EnemyBullet>();
            bullet.ImDead -= disableBullet;
            bullet.DamageEntity -= changeLifeOfEntity;
        }
    }

    private void suscribeEnemyEvents()
    {
        if (_enemies == null) { return; }
        for (int i = 0; i < _enemies.transform.childCount; i++)
        {
            Enemy enemy = _enemies.transform.GetChild(i).GetComponent<Enemy>();
            enemy.OnAttack += generateEnemyBullet;
            enemy.DamagePlayer += changeLifeOfEntity;
            enemy.onPickupGenerate += generatePickup;
        }
    }

    private void unsuscribeEnemyEvents()
    {
        if (_enemies == null) { return; }
        for (int i = 0; i < _enemies.transform.childCount; i++)
        {
            Enemy enemy = _enemies.transform.GetChild(i).GetComponent<Enemy>();
            enemy.OnAttack -= generateEnemyBullet;
            enemy.DamagePlayer -= changeLifeOfEntity;
            enemy.onPickupGenerate -= generatePickup;
        }
    }

    private void suscribePowerUpEvents()
    {
        if (_pickUps == null) { return; }
        for (int i = 0; i < _pickUps.transform.childCount; i++)
        {
            PowerUp powerUp = _pickUps.transform.GetChild(i).GetComponent<PowerUp>();
            powerUp.onPickupsGot += changeHealthOfPlayer;
        }
    }

    private void unsuscribePowerUpEvents()
    {
        if (_pickUps == null) { return; }
        for (int i = 0; i < _pickUps.transform.childCount; i++)
        {
            PowerUp powerUp = _pickUps.transform.GetChild(i).GetComponent<PowerUp>();
            powerUp.onPickupsGot -= changeHealthOfPlayer;
        }
    }

    private void disableBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    private void changeLifeOfEntity(GameObject entity, GameObject bullet, float amount)
    {
        Player playr;
        entity.TryGetComponent(out playr);

        Enemy enemy;
        entity.TryGetComponent(out enemy);
        if (playr == null  && enemy == null) { return; }

        if(playr != null)
        {
            uimanager.addLife(amount); return;
        }
             
        
        enemy.changeLife(amount);
    }

    private void generateBullet(Vector2 position, float speed, float damage)
    {
        Vector2 bulletPosition = new Vector2(position.x, position.y);
        for (int i = 0; i < _bullets.transform.childCount; i++)
        {
            GameObject Bullet = _bullets.transform.GetChild(i).gameObject;
            if (Bullet.activeInHierarchy == false) {
                Bullet.SetActive(true);
                Bullet.transform.localPosition = new Vector3(bulletPosition.x, bulletPosition.y, 0);
                _bulletCmps[i].speed = speed;
                _bulletCmps[i].damage = damage;
                break;
            }
        }
    }

    private void generateEnemyBullet(Vector2 position, float speed, float damage)
    {
        Vector2 bulletPosition = new Vector2(position.x, position.y);
        for (int i = 0; i < _enemyBullets.transform.childCount; i++)
        {
            GameObject Bullet = _enemyBullets.transform.GetChild(i).gameObject;
            if (Bullet.activeInHierarchy == false)
            {
                Bullet.SetActive(true);
                Bullet.transform.localPosition = new Vector3(bulletPosition.x, bulletPosition.y, 0);
                _EnemybulletCmps[i].speed = speed;
                _EnemybulletCmps[i].damage = damage;
                break;
            }
        }
        //GameObject bullet = Instantiate(bala);
    }

    private void generatePickup(Vector2 collider, int posibility)
    {
        if (_pickUps == null) { return; }

        int rand = Random.Range(0, 10);
        int pickups = _pickUps.transform.childCount;

        if (rand > posibility) { return; }

        for (int i = 0; i < pickups;)
        {
            GameObject pickup = _pickUps.transform.GetChild(i).gameObject;
            if (pickup.activeInHierarchy == true) { return; }
            float positionX = collider.x;
            float positionY = collider.y;

            pickup.SetActive(true);
            pickup.transform.position = new Vector3(positionX, positionY, 0);
            break;
        }
    }

    private void changeHealthOfPlayer(float amount)
    {
        uimanager.addLife(amount);
    }

    private void Awake()
    {
        initComponents();
    }

    private void initComponents()
    { 
        getBulletsComponents();
        getEnemyBulletsComponents();
    }

    private void getBulletsComponents()
    {
        if (_bullets == null) { return; }
        for (int i = 0; i < _bullets.transform.childCount; i++)
        {
            Bullet bullet = _bullets.transform.GetChild(i).GetComponent<Bullet>();
            _bulletCmps.Add(bullet);
        }
    }

    private void getEnemyBulletsComponents()
    {
        if (_enemyBullets == null) { return; }
        for (int i = 0; i < _enemyBullets.transform.childCount; i++)
        {
            EnemyBullet bullet = _enemyBullets.transform.GetChild(i).GetComponent<EnemyBullet>();
            _EnemybulletCmps.Add(bullet);
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
