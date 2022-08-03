using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public string pole;
    public bool on = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == pole)
        {
            on = true;
            Destroy(collision.gameObject);
        }
    }
}
