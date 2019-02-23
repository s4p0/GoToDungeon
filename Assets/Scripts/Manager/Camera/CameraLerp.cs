
using UnityEngine;

namespace Assets.Scripts.Manager.Camera
{
    public class CameraLerp
    {
        private bool isUpdating;
        public float _lerpTime = 2f;
        private float startTime = 0f;
        private float intervalTime = .0f;

        private UnityEngine.Camera _camera;

        public CameraLerp(UnityEngine.Camera camera, float lerpTime)
        {
            _camera = camera;
            _lerpTime = lerpTime;
        }

        public void Update()
        {
            if (startTime >= _lerpTime)
            {
                isUpdating = false;
                Time.timeScale = 1f;
                OldPosition = NewPosition;
            }

            if (isUpdating)
            {
                startTime += Time.unscaledDeltaTime;
                _camera.gameObject.transform.position = Vector3.Lerp(OldPosition, NewPosition, startTime / _lerpTime);
            }
        }

        public Vector3 OldPosition { get; private set; }
        public Vector3 NewPosition { get; private set; }

        public void TranslateCamera(Vector3 center)
        {
            center.z = _camera.gameObject.transform.position.z;

            if (center == OldPosition)
                return;

            OldPosition = _camera.gameObject.transform.position;
            NewPosition = center;

            startTime = 0;
            Time.timeScale = intervalTime;

            isUpdating = true;
        }



    }
}
