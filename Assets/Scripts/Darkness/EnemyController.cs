using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxHealth = 100;
    public float actualHealth;

    [SerializeField] private float recoveryRate = 10.0f;

    private bool tookDmg = false;

    public SpriteRenderer enemySprite;


    // Start is called before the first frame update
    void Start()
    {
        if (enemySprite == null)
            enemySprite = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();

        actualHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!tookDmg && actualHealth < maxHealth)
        {
            actualHealth += recoveryRate * Time.deltaTime;

            if (actualHealth >= maxHealth)
                actualHealth = maxHealth;
        }

        tookDmg = false;

        float healthPrctg = actualHealth / maxHealth; // Con decimales (Ex -> 0,1)

        enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, healthPrctg);
    }

    public void TakeDamage(float damage) 
    {
        tookDmg = true;

        actualHealth -= damage;

        if (actualHealth <= 0)
            gameObject.SetActive(false);
    }
}
