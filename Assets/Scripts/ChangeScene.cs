using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Dropdown drop;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Change(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Change("LVL select");
        }
    }

    public void Change(string next)
    {
        SceneManager.LoadScene(next);
    }

    public void Next()
    {
        var now = SceneManager.GetActiveScene().buildIndex;
        if (now > 4 && now < 29)
        {
            SceneManager.LoadScene(now + 1);
        }
        if (now <= 4 || now > 29)
        {
            SceneManager.LoadScene("LVL select");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DropChange()
    {
        SceneManager.LoadScene(drop.value.ToString());
    }
}
