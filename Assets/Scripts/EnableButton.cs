using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableButton : MonoBehaviour
{
    public Button boton;
    public DragNDrop DND;
    public DND2nd DND2;
    public Switch[] s;
    private bool done;

    private void Start()
    {
        boton.enabled = false;
        boton.image.enabled = false;
        DND = FindObjectOfType<DragNDrop>();
        DND2 = FindObjectOfType<DND2nd>();
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < s.Length; i++)
        {
            if(s[i].on == false) 
            { 
                done = false;
            }
            else 
            { 
                if (s[i].on == true)
                {
                    done = true;
                }
            }
        }
        if (done == true)
        {
            boton.enabled = true;
            boton.image.enabled = true;
            DND2.enabled = false;
            DND.enabled = false;
        }
        if (done == false)
        {
            boton.enabled = false;
            boton.image.enabled = false;
            DND2.enabled = true;
            DND.enabled = true;
        }
    }
}
