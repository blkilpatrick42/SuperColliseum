using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {
    private Rigidbody2D body;
    private BoxCollider2D coll;
    public int collisions;
    // Use this for initialization
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        coll = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float bulletSpeed = 4;
        body.velocity = transform.right * bulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D co)
    {
        if (co.gameObject.tag == "Solid" || co.gameObject.tag == "2Damage")
        {
            Destroy(gameObject);
        }
        collisions = collisions - 1;
        if (collisions <= 0 && co.gameObject.tag != "P")
        {
            Destroy(gameObject);
        }
    }
}
