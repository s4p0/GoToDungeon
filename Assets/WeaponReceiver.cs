using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Events;
using UnityEngine;

namespace Weapons
{
    
    public class WeaponReceiver : MonoBehaviour
    {
        public Weapon _weapon;
        private Animator _animator;
        private SpriteRenderer _renderer;

        private bool HasWeapon => _weapon != null;

        private void Start()
        {

            _animator = GetComponent<Animator>();

            _renderer = GetComponent<SpriteRenderer>();
            _renderer.enabled = false;

            //Assets.Scripts.Manager.GameManager.Manager.Pickups.Subscribe(Assets.Scripts.Pickups.PickupEnum.Weapon, WeaponPickupDelegate);

            GlobalEventSystem.Events.Subscribe<Assets.Scripts.Events.WeaponPickedUpEvent>(WeaponListener);

        }

        private void WeaponListener(WeaponPickedUpEvent e)
        {
            ChangeWeapon(e.Weapon);
        }

        public void WeaponPickupDelegate(object sender, Assets.Scripts.Manager.PickupEventArgs e)
        {
            ChangeWeapon((e as Assets.Scripts.Manager.WeaponPickupEventArgs).Weapon);
            Debug.Log("Picked");
        }

        private void ChangeWeapon(object newWeapon)
        {
            _weapon = newWeapon as Weapon;

            if (_weapon != null)
            {
                _animator.runtimeAnimatorController = _weapon.WeaponController;
                _renderer.sprite = _weapon.WeaponSprite;

                _renderer.enabled = true;
                _animator.enabled = true;

                Assets.Scripts.Manager.GameManager.Manager.Messages.Publish("canAttack", true);
            }
        }

        private void DropWeapon()
        {
            _weapon = null;
            _animator.enabled = false;
            _renderer.enabled = false;
        }

    }

}