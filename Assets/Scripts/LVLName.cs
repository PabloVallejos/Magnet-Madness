using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LVLName : MonoBehaviour
{
    public Text texto;
    private string str;

    void FixedUpdate()
    {
        str = SceneManager.GetActiveScene().name;

        texto.text = str;
    }
}
