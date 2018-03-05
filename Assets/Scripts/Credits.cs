using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if ((SceneManager.GetActiveScene()).name.Equals("Creditos") || (SceneManager.GetActiveScene()).name.Equals("Controles"))
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
}
