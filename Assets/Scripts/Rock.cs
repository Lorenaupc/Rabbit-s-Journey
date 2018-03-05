using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

    GameObject lanzado;
    public GameObject rock;
    bool puedeLanzar;

    void Start()
    {
        puedeLanzar = true;
    }

    void Update()
    {
        if (puedeLanzar)
        {
            lanzado = (GameObject)Instantiate(rock, transform.position, transform.rotation);
            StartCoroutine(Lanzar(lanzado));
        }
    }

    IEnumerator Lanzar(GameObject lanzado)
    {
        puedeLanzar = false;

        if (this.gameObject.tag.Equals("Special")) {
            if (lanzado != null)
            {
                yield return new WaitForSeconds(5f);
                Destroy(lanzado);
                yield return new WaitForSeconds(2f);
            }
            puedeLanzar = true;
        }
        else if (this.gameObject.tag.Equals("Rock"))
        {
            if (lanzado != null)
            {
                yield return new WaitForSeconds(1.5f);
                Destroy(lanzado);
                yield return new WaitForSeconds(0.5f);
            }
            puedeLanzar = true;
        }
    }
}
