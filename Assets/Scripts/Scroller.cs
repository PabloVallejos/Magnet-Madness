using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public Transform Ipos;
    public Transform Fpos;
    public float speed;
    public float pos;

    void Update()
    {
        this.gameObject.transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (this.gameObject.transform.position.x <= pos)
        {
            this.gameObject.transform.position = Ipos.position;
        }
    }
}
