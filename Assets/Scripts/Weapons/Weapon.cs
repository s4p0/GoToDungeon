using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon")]
public class Weapon : ScriptableObject
{
    
    public Sprite WeaponSprite;
    public AnimatorOverrideController WeaponController;
    public int damage;
    public int durability;
    public float cooldown;
    

    public Weapon Clone()
    {
        return new Weapon
        {
            cooldown = cooldown,
            damage = damage,
            durability = durability,
        };
    }
}
