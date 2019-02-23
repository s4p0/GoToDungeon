using Assets.Scripts.Manager;
using Assets.Scripts.Pickups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [ExecuteInEditMode]
    public class PickUpWeapon : PickUp
    {
        public Weapon weapon;
        private CapsuleCollider2D boxCollider;
        SpriteRenderer rd2d;

        public override PickupEnum PickupType => PickupEnum.Weapon;

        private void Start()
        {
            rd2d = GetComponent<SpriteRenderer>();
            rd2d.sprite = weapon.WeaponSprite;

            var bounds = rd2d.sprite.bounds;
            boxCollider = GetComponent<CapsuleCollider2D>();
            boxCollider.bounds.Encapsulate(bounds);

            boxCollider.offset = new Vector2(0, (bounds.size.y / transform.lossyScale.y) / 2);
            boxCollider.size = new Vector3(bounds.size.x / transform.lossyScale.x,
                                            bounds.size.y / transform.lossyScale.y,
                                            bounds.size.z / transform.lossyScale.z);
        }

        protected override void OnTriggered(Collider2D collision)
        {
            var evt = new Assets.Scripts.Events.WeaponPickedUpEvent()
            {
                Weapon = weapon,
                Collision = collision,
                PickUpWeapon = this
            };

            GlobalEventSystem.Events.Publish(evt);
        }
    }

}