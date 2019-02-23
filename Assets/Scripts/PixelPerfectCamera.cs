using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour
{
    Camera _camera;
    public float pixelsPerUnit = 16;
    [Range(1,4)]
    public float pixelScale = 1;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        _camera.orthographic = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Orthographic size = ((Vert Resolution)/(PPUScale * PPU)) * 0.5
        _camera.orthographicSize = ((Screen.height) / (pixelScale * pixelsPerUnit)) * .5f;
        //_camera.orthographicSize = Screen.height * ((0.5f * pixelsPerUnit) / pixelScale);
    }

    //private void LateUpdate()
    //{
    //    Vector3 position = transform.position;
    //    position.x = (Mathf.Round(transform.position.x * pixelsPerUnit) / pixelsPerUnit) - transform.position.x;
    //    position.y = (Mathf.Round(transform.position.y * pixelsPerUnit) / pixelsPerUnit) - transform.position.y;
    //    transform.position = position;
    //}
}
