using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 200f;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Update()
    {
        DisplayHealth();
    }

    private void DisplayHealth()
    {
        healthText.text = "health : " + health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}
