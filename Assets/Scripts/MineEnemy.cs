using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineEnemy : MonoBehaviour {

    float velocidad = 1.0f;      
    float limiteIzquierdo = 0.0f;       
    float limiteDerecho = 5.0f;      
    float direccion = 1.0f;
    Vector2 caminar;

    void Start()
    {
        limiteIzquierdo = transform.position.x - 2.5f;
        limiteDerecho = transform.position.x + 2.5f;
    }
    

    void Update()
    {
        caminar.x = direccion * velocidad * Time.deltaTime;
        if (direccion > 0.0f && transform.position.x >= limiteDerecho)
        {
            direccion = -1.0f;
            changeDirection();
        }
        else if (direccion < 0.0f && transform.position.x <= limiteIzquierdo)
        {
            direccion = 1.0f;
            changeDirection();
        }
        transform.Translate(caminar);
    }

    //Si se encuentra con los tags Pipe, Kill o Suelo, cambia de dirección
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Rock"))
        {
            direccion *= -1.0f;
            changeDirection();
            caminar.x = direccion * velocidad * Time.deltaTime;
            transform.Translate(caminar);
            Destroy(other.gameObject);
        }
    }

    void changeDirection()
    {
        Vector3 Escala = transform.localScale;
        Escala.x *= -1;
        transform.localScale = Escala;
    }
}
