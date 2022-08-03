using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MZone : MonoBehaviour
{
    public string tg;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == tg)
        {
            collision.gameObject.GetComponent<Magnets>().enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == tg)
        {
            collision.gameObject.GetComponent<Magnets>().enabled = true;
        }
    }
}
