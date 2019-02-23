using Assets.Scripts.Pickups;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class PickUp : MonoBehaviour
{
    public abstract PickupEnum PickupType { get; }
    public bool destroyAfter = true;

    protected abstract void OnTriggered(Collider2D collision);

    public virtual void PlayerPickup(PickUp item, Hero hero)
    {
        
    }

    public virtual void EnemyPickup(PickUp item, Enemy hero)
    {
        Debug.Log("Enemy Pickup");
    }

    public virtual void AfterPickup(PickUp pickUp)
    {
        Debug.Log("After Pickup");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggered(collision);

        if (destroyAfter)
            Destroy(gameObject);
    }
}
