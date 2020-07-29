using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private int currentWeapon = 0;
    private int weaponsCount;
    
    // Start is called before the first frame update
    void Start()
    {
        SetWeaponActive();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentWeapon == transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }

            SetWeaponActive();
        }
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }
}
