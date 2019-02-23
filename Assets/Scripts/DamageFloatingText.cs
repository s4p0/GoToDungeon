using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFloatingText : MonoBehaviour
{
    public float lifetime = .5f;
    public Vector3 offset = new Vector3(0, 2, 0);

    private void Start()
    {
        gameObject.transform.localPosition += offset; 
        Destroy(gameObject, lifetime);
    }
}
