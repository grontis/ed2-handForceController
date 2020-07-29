using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int amount = 10;
    [SerializeField] private AmmoType type;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(type, amount);
            Destroy(gameObject);
        }
    }
}
