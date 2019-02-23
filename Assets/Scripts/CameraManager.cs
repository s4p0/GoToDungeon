using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera targetCamera;

    private static CameraManager manager;
    private void Awake()
    {
        if (manager == null)
            manager = this;
        else
            Destroy(manager);
    }

    public static CameraManager Instance
    {
        get { return manager; }
    }

    public void UpdatePosition(Vector3 position)
    {
       
        if(targetCamera!= null)
        {
            Debug.Log("Requested a camera update.");
            targetCamera.transform.position = new Vector3(position.x, position.y, targetCamera.transform.position.z);
        }
    }

}
