using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    public int health;
    public int strength;
    public float runningThreshold = .2f;
    public bool hasRunningAnimation = true;
    
    private Animator animator;
    private Rigidbody2D body;

    public virtual void Start()
    {
        Debug.Log("char start");
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        IsRunning();
        IsFacing();
    }

    public Rigidbody2D CharacterBody
    {
        get { return body; }
    }

    public Animator CharacterAnimator
    {
        get { return animator; }
    }

    public virtual void IsRunning()
    {
        var isRunning = body.velocity.magnitude > runningThreshold;
        SetRunningAnimator(isRunning);
    }

    public virtual void SetRunningAnimator(bool running)
    {
        if(hasRunningAnimation)
            animator.SetBool("run", running);
    }

    public virtual void IsFacing()
    {
        if (body.velocity.x > runningThreshold)
            transform.localScale = new Vector3(1, 1, 1);
        else if(body.velocity.x < runningThreshold * -1)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public virtual void OnDoDamage(int  amount, Character character)
    {
        character.ReceiveDamage(amount);
    }

    public void DoDamage(int amount, Character character)
    {
        OnDoDamage(amount, character);
    }

    public virtual void OnReceiveDamage(int amount, Vector2? point)
    {
        
    }

    public void ReceiveDamage(int amount, Vector2? point = null)
    {
        OnReceiveDamage(amount, point);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if player tag
        // then this is an enemy
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("enemy did contact with player");
            DoDamage(strength, collision.collider.GetComponent<Hero>());
        }
            

        // if weapon
        // the this is a hero
        if (collision.collider.CompareTag("Weapon"))
        {
            Debug.Log("weapon did contact with player/enemy");

            var hero = collision.gameObject.GetComponent<Hero>();
            ReceiveDamage(hero.Power, collision.GetContact(0).point);

            hero.IncreaseHitCount();
        }
    }
}
