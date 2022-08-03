using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiner : MonoBehaviour
{
    public float speed;

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
