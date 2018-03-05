using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

    private Animator animator;
    public GameObject Melt;
    public GameObject die;
    public GameObject snowParticle;
    public GameObject particleCarrot;

    public AudioSource first;
    public AudioSource hit;
    public AudioSource impact;
    public AudioSource carrot;

    private Rigidbody2D rigidbody2d;

    float alturaSalto = 12;
    float velocidadLineal = 6;
    int jumping = 10;

    public bool estaEnSuelo;
    bool puedoMover;
    public Transform marcadorSuelo;
    private float radioMarcadorSuelo;
    public LayerMask layerSuelo;

    bool mirarDerecha;

    public event triggerDelegate eliminado;
    public event triggerDelegate finalNivel;
    public event triggerDelegate contadorSuma;
    public delegate void triggerDelegate();

    //Funciones que controlan comportamientos de colisión o trigger con diferentes Tags
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Meta"))
        {
            finalNivel();
        }
        else if (other.gameObject.tag.Equals("Carrot"))
        {
            carrot.Play();
            GameObject ps = (GameObject)Instantiate(particleCarrot, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(ps, 0.2f);
            contadorSuma();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Finish"))
        {
            eliminado();
        }
        else if (col.gameObject.tag.Equals("Enemy") || col.gameObject.tag.Equals("EnemyMine"))
        {
            if (!estaEnSuelo)
            {
                Vector2 v = col.contacts[0].point - (Vector2)transform.position;
                if (Mathf.Abs(Vector2.Angle(v, Vector3.up)) > 45.0)
                {
                    rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumping);

                    if (col.gameObject.tag.Equals("Enemy"))
                    {
                        GameObject ps = (GameObject)Instantiate(snowParticle, transform.position, transform.rotation);
                        hit.Play();
                        Destroy(col.gameObject);
                        Destroy(ps, 0.2f);
                        Instantiate(Melt, col.transform.position, col.transform.rotation);
                    }
                    else
                    {
                        GameObject ps = (GameObject)Instantiate(snowParticle, transform.position, transform.rotation);
                        hit.Play();
                        Destroy(col.gameObject);
                        Destroy(ps, 0.2f);
                        Instantiate(die, col.transform.position, col.transform.rotation);
                    }
                }
                else eliminado();
            }
            else eliminado();
        }
        else if (col.gameObject.tag.Equals("Snowball") ||col.gameObject.tag.Equals("Rock") || col.gameObject.tag.Equals("Special"))
        {
            impact.Play();
            Destroy(col.gameObject);     
            eliminado();
        }
        else if (col.gameObject.tag.Equals("Defeat"))
        {
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
        }
    }


    //Funciones de Start, Update y FixedUpdate
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        radioMarcadorSuelo = 0;
        animator = GetComponent<Animator>();
        mirarDerecha = true;
        puedoMover = true;
    }

    void Update()
    {        
        Invoke("Movement", 0);
        if (Input.GetKey(KeyCode.LeftArrow) && puedoMover)
        {
            if (mirarDerecha) {
                mirarDerecha = false;
                Vector3 Escala = transform.localScale;
                Escala.x *= -1;
                transform.localScale = Escala;
            }
            
            if (rigidbody2d.velocity.x * -1 < velocidadLineal / 2)
            {
                rigidbody2d.velocity += new Vector2(transform.right.x * velocidadLineal*1.5f * -1, transform.right.y * velocidadLineal)*1.5f * Time.deltaTime;
            }
            else if (rigidbody2d.velocity.x * -1 > velocidadLineal/2)
            {
                rigidbody2d.velocity = new Vector2(-velocidadLineal, rigidbody2d.velocity.y);
            }

        }
        if (Input.GetKey(KeyCode.RightArrow) && puedoMover)
        {
            
            if (rigidbody2d.velocity.x < velocidadLineal / 2)
            {
                rigidbody2d.velocity += new Vector2(transform.right.x * velocidadLineal*1.5f, transform.right.y * velocidadLineal)*1.5f * Time.deltaTime;
            }
            else if (rigidbody2d.velocity.x > velocidadLineal/2)
            {
                rigidbody2d.velocity = new Vector2(velocidadLineal, rigidbody2d.velocity.y);
            }
            
            if (!mirarDerecha) {
                mirarDerecha = true;
                Vector3 Escala = transform.localScale;
                Escala.x *= -1;
                transform.localScale = Escala;
            }          
        }
        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, alturaSalto);
            Invoke("Salto", 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) && estaEnSuelo)
        {
            puedoMover = false;
            Invoke("Mirarabajo", 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) && estaEnSuelo)
        {
            puedoMover = false;
            Invoke("Mirarriba", 0);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            puedoMover = true;
        }
        animator.SetBool("abajo", false);
        animator.SetBool("arriba", false);
    }

    void FixedUpdate()
    {
        estaEnSuelo = Physics2D.OverlapCircle(marcadorSuelo.position, radioMarcadorSuelo, layerSuelo);
        Invoke("Salto", 0);
    }


    private void Walking()
    {
        first.Play();
    }

    public void setVelocity()
    {
        rigidbody2d.velocity = new Vector2(0,0);
    }


    //Invokes para animaciones; setea floats o boleanos
    private void Movement()
    {
        if (rigidbody2d.velocity.x.Equals(0))
        {
            animator.SetBool("velocity", false);
        }
        else
        {
            animator.SetBool("velocity", true);
        }
    }

    private void Salto()
    {
        animator.SetBool("suelo", estaEnSuelo);
    }

    private void Mirarabajo()
    {
        animator.SetBool("abajo", true);
    }

    private void Mirarriba()
    {
        animator.SetBool("arriba", true);
    }
}
