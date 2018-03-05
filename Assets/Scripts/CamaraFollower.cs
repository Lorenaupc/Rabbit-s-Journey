using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollower : MonoBehaviour {

    public Transform rabbit;

    private float speed;
    Control con;
    public Vector3 cam;

    int i = 0;

    void Start()
    {
        cam = transform.position - rabbit.transform.position;
        speed = 2.5f;
        con = rabbit.GetComponent <Control>();
    }

	void Update () {
                
        if (Input.GetKey(KeyCode.UpArrow) && con.estaEnSuelo)
        {
            while (i == 0)
            {
                transform.position += (speed * transform.up);
                i = 1;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) && con.estaEnSuelo)
        {
            while (i == 0)
            {
                transform.position -= (speed * transform.up);
                i = 1;
            }
        }
        else
        {
            i = 0;
            transform.position = rabbit.position + cam;
        }
    }
}
