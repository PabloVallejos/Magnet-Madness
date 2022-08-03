using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Rigidbody2D bull;
    public Transform muzzle;
    public float Stimer;
    private float timer;

    private void Start()
    {
        timer = Stimer;
    }

    private void FixedUpdate()
    {
        timer--;
        if (timer <= 0) 
        {
            var bul = Instantiate(bull, muzzle.position, gameObject.transform.rotation);
            bul.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 25, ForceMode2D.Impulse);
            timer = Stimer;
        }
    }
}
