using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera FPCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private ParticleSystem flash;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private Ammo ammoSlot;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private float timeBetweenShots = 0.5f;
    [SerializeField] private TextMeshProUGUI ammoText;
    
    private bool canShoot = true;

    private HFConfig hfConfig;

    private void Start()
    {
        hfConfig = FindObjectOfType<HFConfig>();
    }

    private void OnEnable()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hfConfig.controllerInput.IsConnected)
        {
            DisplayAmmo();
            
            if (hfConfig.controllerInput.GetSensorValue(hfConfig.fire) > 800 && canShoot) 
            {
                StartCoroutine(Shoot());
            }
        }
        
        //UNCOMMENT FOR Mouse firing input
        if (Input.GetButtonDown("Fire1") && canShoot) 
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmount(AmmoType.Bullets);
        ammoText.text = "AMMO : " + currentAmmo.ToString();
    }

    public void FireWeapon()
    {
        
    }
    
    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmount(ammoType) > 0)
        {
            ammoSlot.ReduceCurrentAmount(ammoType);
            PlayFlash();
            ProcessRaycast();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayFlash()
    {
        flash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null || target.IsDead()) return;
            target.TakeDamage(damage);
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject effect = Instantiate(hitEffect, hit.transform.position, Quaternion.LookRotation(hit.normal));
        Destroy(effect, 0.1f);
    }
}
