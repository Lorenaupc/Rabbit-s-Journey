using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject snowball;
    public Transform rabbit;

    GameObject lanzado;   
    bool puedeLanzar;

    void Start()
    {
        puedeLanzar = true;
        rabbit = GetComponent <Transform>();
    }

    void Update()
    {
        if (puedeLanzar)
        {
            Vector3 player = (rabbit.transform.position - transform.position).normalized;
            Vector3 ball = new Vector3(-0.75f,0,0);
            
            lanzado = (GameObject)Instantiate(snowball, transform.position + ball, transform.rotation);
            
            Rigidbody2D snow = lanzado.GetComponent<Rigidbody2D>();
            snow.velocity = new Vector2(1500f, 0);
            StartCoroutine(Lanzar(lanzado));
        }
    }
    
    IEnumerator Lanzar(GameObject lanzado)
    {
        puedeLanzar = false;
        if (lanzado != null)
        {
            yield return new WaitForSeconds(3f);
            Destroy(lanzado);
            yield return new WaitForSeconds(2f);
        }
        puedeLanzar = true;
    }

    void OnDestroy()
    {
        if (lanzado != null) { 
            Destroy(lanzado);
        }
    }
}
