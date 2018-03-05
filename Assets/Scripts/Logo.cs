using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour {

    float start;
    float final = 5;

    void Start()
    {
    }

    void Update()
    {
        start += Time.deltaTime;
        if (start >= final)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
