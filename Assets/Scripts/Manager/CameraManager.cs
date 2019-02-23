using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }

        private UnityEngine.Camera _camera;

        private Camera.CameraShake CameraShake { get; set; }
        private Camera.CameraLerp CameraLerp { get; set; }

        

        public CameraManager()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        private void Awake()
        {
            _camera = FindObjectOfType<UnityEngine.Camera>();

            Initialize();
        }

        private void Initialize()
        {
            CameraShake = new Camera.CameraShake(_camera);
            CameraLerp = new Camera.CameraLerp(_camera, 0.4f);
        }
        
        private void Update()
        {
            CameraShake.Update();
            CameraLerp.Update();
        }

        public void TranslateCamera(Vector3 center)
        {
            Debug.Log("start translate");
            CameraLerp.TranslateCamera(center);
            Debug.Log("finish translate");
        }

        internal void RequestShake()
        {
            Debug.Log("start shake");
            CameraShake.RequestShake();
            Debug.Log("finish shake");
        }
    }
}
