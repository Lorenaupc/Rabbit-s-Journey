using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Finish") || col.gameObject.tag.Equals("Wall") || col.gameObject.tag.Equals("EnemyMine"))
        {
            Destroy(this.gameObject);
        }
    }
}
