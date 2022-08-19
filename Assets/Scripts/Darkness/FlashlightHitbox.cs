using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightHitbox : MonoBehaviour
{
    public float damage = 5.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject collisionObj = collision.gameObject;

        if (collisionObj.CompareTag("EnemyHurtbox"))
        {
            GameObject tmpEnemy = collisionObj.transform.parent.gameObject;
            tmpEnemy.GetComponent<EnemyController>().TakeDamage(damage * Time.deltaTime);

            Debug.Log("a");
        }
    }
}