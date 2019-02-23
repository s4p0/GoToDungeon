﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Hero))]
public class Controller : MonoBehaviour
{
    Vector2 velocity;
    
    public float speed = 10;
    private Rigidbody2D body;
    private Hero hero;

    

    public float jumpForce = 1000;

    public Joystick joy;
    public bool isJumping;
    public bool canJump = true;
    public float jumpingTime = .3f;
    private float jumpingStart = 0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hero = GetComponent<Hero>();
    }

    void Update()
    {
        IsMoving();
        IsFireButtonPressed();

        if (Input.GetButtonUp("Jump"))
        {
            canJump = true;
            isJumping = false;
        }
            

        if (canJump && !isJumping && Input.GetButton("Jump"))
        {
            isJumping = true;
            jumpingStart = jumpingTime;
            canJump = false;
        }

        if (jumpingStart <= 0)
            isJumping = false;

        if(isJumping)
        {
            body.AddForce(velocity * jumpForce);
            jumpingStart -= Time.deltaTime;
        }
    }

    (float, float) GetAxis()
    {
        float xAxis, yAxis;


        if (joy == null || !joy.enabled)
        {
            xAxis = Input.GetAxisRaw("Horizontal");
            yAxis = Input.GetAxisRaw("Vertical");
        }
        else
        {
            xAxis = joy.Horizontal;
            yAxis = joy.Vertical;
        }

        return (xAxis, yAxis);
    }

    void IsMoving()
    {
        //float xAxis = Input.GetAxisRaw("Horizontal");
        //float yAxis = Input.GetAxisRaw("Vertical");

        ////var xAxis = joy.Horizontal;
        ////var yAxis = joy.Vertical;

        var (xAxis, yAxis) = GetAxis();
        velocity = new Vector2(xAxis, yAxis);
        velocity.Normalize();
        velocity = velocity * speed;
    }

    void IsFireButtonPressed()
    {
        if(hero != null)
        {
            if (Input.GetButtonDown("Fire1"))
                hero.Attack();
            if (Input.GetButtonDown("Fire2"))
                hero.DropWeapon();
        }
    }


    private void FixedUpdate()
    {
        body.velocity = velocity;
    }
}
