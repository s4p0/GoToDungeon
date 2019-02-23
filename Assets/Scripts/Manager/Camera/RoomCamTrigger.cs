﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class RoomCamTrigger : MonoBehaviour
{
    
    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //var cam = FindObjectOfType<Camera>();
            //var position = cam.gameObject.transform.position;

            var col = GetComponent<PolygonCollider2D>();
            var center = col.bounds.center;

            //position.x = center.x;
            //position.y = center.y;

            //cam.gameObject.transform.position = position;
            Assets.Scripts.Manager.CameraManager.Camera.TranslateCamera(center);
        }
    }
    
    
    
}