using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float timer;

    private void Start()
    {
        //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        timer--;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
