using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnets : MonoBehaviour
{
    public Transform tgm;
    public Transform tgr;
    public Transform tgk;
    public string pole;
    public float speed;
    public bool mag;
    public bool rep;
    bool atr;

    private void Start()
    {
        mag = false;
        rep = false;
        atr = false;
    }

    void FixedUpdate()
    {
        if (mag == true && tgm != null)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, tgm.position, step);
        }
        if (rep == true && tgr != null)
        {
            var step = -speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, tgr.position, step);
        }
        if (atr == true && tgk != null)
        {
            var step = speed * Time.deltaTime;
            tgk.position = Vector2.MoveTowards(tgk.position, transform.position, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == pole || collision.gameObject.tag == "M")
        {
            mag = true;
            tgm = collision.gameObject.transform;
            Debug.Log("Got em");
        }
        if (collision.gameObject.tag == gameObject.tag)
        {
            rep = true;
            tgr = collision.gameObject.transform;
        }
        if (collision.gameObject.tag == "Key")
        {
            atr = true;
            tgk = collision.gameObject.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == pole)
        {
            mag = false;
        }
        if (collision.gameObject.tag == "M")
        {
            mag = false;
            gameObject.transform.SetParent(tgm.transform);
        }
        if (collision.gameObject.tag == "Key")
        {
            atr = false;
            tgk.SetParent(gameObject.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == pole || collision.gameObject.tag == "M")
        {
            mag = true;
            tgm = collision.gameObject.transform;
            Debug.Log("Got em");
        }
        if (collision.gameObject.tag == "M")
        {
            mag = false;
            gameObject.transform.SetParent(null);
        }
        if (collision.gameObject.tag == "Key")
        {
            atr = true;
            tgk.SetParent(null);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Out");
        if (collision.gameObject.tag == "Pole" || collision.gameObject.tag == "M")
        {
            mag = false;
            tgm = null;
        }
        if (collision.gameObject.tag == gameObject.tag)
        {
            rep = false;
            tgr = null;
        }
        if (collision.gameObject.tag == "Key")
        {
            atr = false;
            tgk = null;
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == null)
        {
            mag = false;
            tgm = null;
            rep = false;
            tgr = null;
            atr = false;
            tgk = null;
        }
    }*/
}
