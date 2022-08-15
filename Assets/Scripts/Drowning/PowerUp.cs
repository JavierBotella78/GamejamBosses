using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Action<float> onPickupsGot;
    [SerializeField] private float healAmount = 10f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        checkToHealPlayer();
    }

    // Si el nivel de explosion es < 2 se suma 1 a este, si es == 2 se cambia la musica
    private void checkToHealPlayer()
    {
        onPickupsGot?.Invoke(healAmount);

        gameObject.SetActive(false);
    }
}
