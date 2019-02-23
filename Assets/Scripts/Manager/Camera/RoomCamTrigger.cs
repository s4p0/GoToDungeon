using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager
{

    [RequireComponent(typeof(PolygonCollider2D))]
    public class RoomCamTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                var col = GetComponent<PolygonCollider2D>();
                var center = col.bounds.center;

                CameraManager.Instance.TranslateCamera(center);
            }
        }
    }

}