using Assets.Scripts.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hero : Character
{
    private WeaponHandler weaponHandler;
    private Animator heroAnimator;
    private Weapon currentWeapon;

    private int weaponDurability;

    public int totalHearts = 3;
    public int numberOfWeapons = 2;


    public override void Start()
    {
        base.Start();

        //GameManager.Manager.Pickups.Subscribe(Assets.Scripts.Pickups.PickupEnum.Weapon, WeaponPickup);

        weaponHandler = FindObjectOfType<WeaponHandler>();
        weaponHandler.GetComponent<Animator>();

        //UIManager.Instance.totalHearts = totalHearts;
        //UIManager.Instance.health = health;

        //UIManager.Instance.UpdateHealthAndHearts(health, totalHearts);

    }
    
    public override void OnReceiveDamage(int amount, Vector2? contact = null)
    {
        health -= amount;
        
        UIManager.Instance.UpdateHealthAndHearts(health, totalHearts);

        CharacterAnimator.SetTrigger("strike");
    }

    public void Attack()
    {
        weaponHandler.AnimateAttack();
    }

    public void DropWeapon()
    {
        weaponHandler.RemoveWeapon();
        collectedWeapons.Pop();
        AddWeaponOnPeek();
    }

    private void WeaponPickup(object sender, PickupEventArgs e)
    {
        var weaponEvt = e as WeaponPickupEventArgs;
        if(weaponEvt.Character == this)
        {
            currentWeapon = weaponEvt.Weapon.Clone();

            weaponDurability = currentWeapon.durability;
            collectedWeapons.Push(currentWeapon);
            AddWeaponOnPeek();
        }
        
    }

    void AddWeaponOnPeek()
    {
        if(collectedWeapons.Count > 0)
            weaponHandler.AddWeapon(collectedWeapons.Peek());
    }

    

    public int Power
    {
        get { return strength + (currentWeapon != null ? currentWeapon.damage : 0); }
    }

    private Stack<Weapon> collectedWeapons = new Stack<Weapon>();
    public IReadOnlyCollection<Weapon> CollectedWeapon
    {
        get { return collectedWeapons; }
    }

    internal void IncreaseHitCount()
    {
        if (currentWeapon != null)
            currentWeapon.durability = currentWeapon.durability - 1;

        if (currentWeapon.durability <= 0)
        {
            DropWeapon();
        }
    }
}
