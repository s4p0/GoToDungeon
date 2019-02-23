using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class CameraManager : MonoBehaviour
    {
        private bool isUpdating;
        public float lerpTime = 2f;
        private float startTime = 0f;
        private float intervalTime = .05f;

        private Camera Main { get; set; }


        public static CameraManager Camera { get; set; }
        public Vector3 OldPosition { get; private set; }
        public Vector3 NewPosition { get; private set; }

        public CameraManager()
        {
            if (Camera == null)
                Camera = this;
            else
                Destroy(this);
        }

        private void Awake()
        {
            Main = FindObjectOfType<Camera>();
            Debug.Log("called");
        }

        private void Update()
        {
            if (startTime >= lerpTime)
            {
                isUpdating = false;
                Time.timeScale = 1f;
            }

            if (isUpdating)
            {
                startTime += Time.unscaledDeltaTime;
                Main.gameObject.transform.position = Vector3.Lerp(OldPosition, NewPosition, startTime / lerpTime );
            }
        }

        public void TranslateCamera(Vector3 center)
        {
            center.z = Main.gameObject.transform.position.z;

            OldPosition = Main.gameObject.transform.position;
            NewPosition = center;

            startTime = 0;
            Time.timeScale = intervalTime;

            isUpdating = true;
        }

    }
}
