using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet2 : MonoBehaviour
{
    List<MagnetizedObject> mo;
    public string pole;
    public float range;
    public float strength;

    public void Start()
    {
        mo = new List<MagnetizedObject>();
        gameObject.GetComponent<SphereCollider>().radius = range;
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < mo.Count; i++)
        {
            ApplyMagneticForce(mo[i]);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(pole))
        {
            MagnetizedObject newMag = new MagnetizedObject();
            newMag.col = other;
            newMag.rb = other.GetComponent<Rigidbody>();
            newMag.t = other.transform;
            newMag.polarity = 1;
            mo.Add(newMag);
        }
        else if (other.gameObject.CompareTag(this.gameObject.tag))
        {
            MagnetizedObject newMag = new MagnetizedObject();
            newMag.col = other;
            newMag.rb = other.GetComponent<Rigidbody>();
            newMag.t = other.transform;
            newMag.polarity = -1;
            mo.Add(newMag);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Attract") || other.CompareTag("Repel"))
        {
            for (int i = 0; i < mo.Count; i++)
            {
                if (mo[i].col == other)
                {
                    mo.RemoveAt(i);
                    break;
                }
            }
        }
    }

    private void ApplyMagneticForce(MagnetizedObject obj)
    {
        Vector3 rawDirection = transform.position - obj.t.position;

        float distance = rawDirection.magnitude;
        float distanceScale = Mathf.InverseLerp(range, 0f, distance);
        float attractionStrength = Mathf.Lerp(0f, strength, distanceScale);

        obj.rb.AddForce(rawDirection.normalized * attractionStrength * obj.polarity, ForceMode.Force);
    }
}

public class MagnetizedObject
{
    public Collider col;
    public Rigidbody rb;
    public Transform t;
    public int polarity;
}

