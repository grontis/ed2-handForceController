using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 40f;

    private PlayerHealth target;
    
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if (target == null)
        {
            return;
        }
        target.TakeDamage(damage);
        
        target.GetComponent<DisplayDamage>().ShowDamageImpact();
    }
}