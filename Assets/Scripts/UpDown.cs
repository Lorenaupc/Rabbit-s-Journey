using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{

    float velocidad = 3.0f;
    float limiteArriba = 8.5f;
    float limiteAbajo = 8.5f;
    float direccion = 1.0f;

    void Start()
    {
        limiteArriba = transform.position.y - 8.5f;
        limiteAbajo = transform.position.y + 8.5f;
    }


    void Update()
    {
        if (transform.position.y >= limiteAbajo)
        {
            direccion = -1.0f;
        }
        else if (transform.position.y <= limiteArriba)
        {
            direccion = 1.0f;
        }
        GetComponent<Rigidbody2D>().MovePosition(transform.position + (velocidad * direccion * transform.up * Time.deltaTime));
    }
}
