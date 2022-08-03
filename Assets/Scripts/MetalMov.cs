using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalMov : MonoBehaviour
{
    public GameObject[] points;
    public float speed;
    public int i;
    private bool dir;

    private void Start()
    {
        dir = true;
        i = 0;
    }

    void FixedUpdate()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, points[i].transform.position, step);
        if (gameObject.transform.position == points[i].transform.position)
        {
            Debug.Log("Reached point " + i);
            if (dir == true)
            {
                i++;
                if (i > points.Length - 1)
                {
                    dir = false;
                    i--;
                }
            } 
            else 
            { 
                i--;
                if (i < 0)
                {
                    dir = true;
                    i++;
                }
            }
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == points[i])
        {
            Debug.Log("Reached point " + i);
            if (dir == true)
            {
                i++;
            } else { i--; }
        }
        if (collision.gameObject.transform == points[points.Length - 1])
        {
            dir = false;
        }
        if (collision.gameObject.transform == points[0])
        {
            dir = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == points[i])
        {
            Debug.Log("Reached point " + i);
            if (dir == true)
            {
                i++;
            }
            else { i--; }
        }
        if (collision.gameObject.transform == points[points.Length - 1])
        {
            dir = false;
        }
        if (collision.gameObject.transform == points[0])
        {
            dir = true;
        }
    }*/
}
