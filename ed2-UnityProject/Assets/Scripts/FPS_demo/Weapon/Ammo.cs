using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private AmmoSlot[] ammoSlots;
    
    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int amount;
    }

    public int GetCurrentAmount(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).amount;
    }

    public void ReduceCurrentAmount(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).amount--;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int amount)
    {
        GetAmmoSlot(ammoType).amount += amount;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }

        return null;
    }
}
