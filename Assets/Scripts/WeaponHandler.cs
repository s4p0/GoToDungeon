using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private Weapon weapon;
    private GameObject weaponPrefab;
    private SpriteRenderer render;
    private Animator animator;


    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void AnimateAttack()
    {
        animator.SetTrigger("hit");
    }

    public void AddWeapon(Weapon weapon)
    {
        RemoveWeapon();

        this.weapon = weapon;

        //weaponPrefab = Instantiate<GameObject>(weapon.WeaponPrefab, transform);
        //EnableOrDisaableWeaponCollider(false);
    }

    public void RemoveWeapon()
    {
        if (weaponPrefab != null)
            Destroy(weaponPrefab);   
        
        weapon = null;
    }

    private void EnableCollider()
    {
        EnableOrDisaableWeaponCollider(true);
    }
    private void DisableCollider()
    {
        EnableOrDisaableWeaponCollider(false);
    }

    void EnableOrDisaableWeaponCollider(bool enable)
    {
        if (weaponPrefab != null)
            weaponPrefab.GetComponent<BoxCollider2D>().enabled = enable;
    }

}
