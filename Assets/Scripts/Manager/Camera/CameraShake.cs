using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Manager.Camera
{
    public class CameraShake
    {
        // How long the object should shake for.
        public float shakeDuration = 0f;
        private bool hasRequest;

        // Amplitude of the shake. A larger value shakes the camera harder.
        public float shakeAmount = 0.7f;
        public float decreaseFactor = 1.0f;
        public float defaultShakeTime = .1f;

        Vector3 originalPos;

        private UnityEngine.Camera _camera;

        public CameraShake(UnityEngine.Camera camera)
        {
            _camera = camera;
        }

        public void Update()
        {
            if(hasRequest)
            {
                if (shakeDuration > 0)
                {
                    _camera.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                    shakeDuration -= Time.deltaTime * decreaseFactor;
                }
                else
                {
                    shakeDuration = 0f;
                    _camera.transform.localPosition = originalPos;
                    hasRequest = false;
                }
            }
        }

        public void RequestShake()
        {
            shakeDuration = defaultShakeTime;
            hasRequest = true;
        }
    }
}