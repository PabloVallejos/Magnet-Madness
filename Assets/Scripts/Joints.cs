using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joints : MonoBehaviour
{
    public Rigidbody2D mag;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "N" || collision.gameObject.tag == "S")
        {
            collision.gameObject.transform.SetParent(this.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "N" || collision.gameObject.tag == "S")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
