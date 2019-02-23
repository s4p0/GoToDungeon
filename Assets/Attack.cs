using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float _cooldown = .2f;
    public bool isAttacking = false;

    private float _timeAttack;
    private Animator _animator;

    public bool canAttack;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.StopPlayback();
        _animator.enabled = false;

        Assets.Scripts.Manager.GameManager.Manager.Messages.Subscribe("canAttack", SetCanAttack);
        Assets.Scripts.Manager.GameManager.Manager.Pickups.Subscribe(Assets.Scripts.Pickups.PickupEnum.Weapon, WeaponPickupDelegate);

    }

    private void WeaponPickupDelegate(object sender, PickupEventArgs e)
    {
        _cooldown = (e as WeaponPickupEventArgs).Weapon.cooldown;
    }

    private void SetCanAttack(object obj)
    {
        canAttack = (bool)obj;
    }

    private void SetCooldown(float cooldown)
    {
        _cooldown = cooldown;
        Assets.Scripts.Manager.GameManager.Manager.Messages.Publish("cooldownChanged", _cooldown);
    }

    private void Update()
    {

        if(canAttack)
        {
            if (isAttacking)
            {
                _timeAttack -= Time.deltaTime;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                isAttacking = true;

                if (_animator != null)
                    _animator.SetTrigger("attacking");
            }

            if (_timeAttack < 0)
            {
                isAttacking = false;
                _timeAttack = _cooldown;
                Assets.Scripts.Manager.GameManager.Manager.Messages.Publish("cooldownReset", null);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SendMessageUpwards("DoAttack", collision);
    }
}
