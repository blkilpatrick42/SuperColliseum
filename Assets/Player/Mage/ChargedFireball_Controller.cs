using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedFireball_Controller : MonoBehaviour {
    private Animator animator;  //this objects animation controller
    private Rigidbody2D body;
    private BoxCollider2D coll;
    private float bulletSpeed = 3.5f;
    float timer;
    public float timerGoal;
    float timerGoal2;
    bool breaking;

    public GameObject tinyFireball;
    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        coll = this.GetComponent<BoxCollider2D>();
        breaking = false;
        body = this.GetComponent<Rigidbody2D>();
        timer = 0;
        timerGoal2 = 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = 0.2f;
        animator.SetBool("Break", breaking);

        body.velocity = transform.right * bulletSpeed;
        timer += Time.deltaTime;
        if (timer > timerGoal2 && breaking)
        {
            Vector2 shootpos = body.position;
            shootpos.x = shootpos.x + dist;
            GameObject clone = (GameObject)Instantiate(tinyFireball, shootpos, new Quaternion());
            Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
            cloneBody.rotation = 0;
            shootpos.y = shootpos.y + dist;
            clone = (GameObject)Instantiate(tinyFireball, shootpos, new Quaternion());
            cloneBody = clone.GetComponent<Rigidbody2D>();
            cloneBody.rotation = 45;
            shootpos.x = shootpos.x - dist;
            clone = (GameObject)Instantiate(tinyFireball, shootpos, new Quaternion());
            cloneBody = clone.GetComponent<Rigidbody2D>();
            cloneBody.rotation = 90;
            shootpos.x = shootpos.x - dist;
            clone = (GameObject)Instantiate(tinyFireball, shootpos, new Quaternion());
            cloneBody = clone.GetComponent<Rigidbody2D>();
            cloneBody.rotation = 135;
            shootpos.y = shootpos.y - dist;
            clone = (GameObject)Instantiate(tinyFireball, shootpos, new Quaternion());
            cloneBody = clone.GetComponent<Rigidbody2D>();
            cloneBody.rotation = 180;
            shootpos.y = shootpos.y - dist;
            clone = (GameObject)Instantiate(tinyFireball, shootpos, new Quaternion());
            cloneBody = clone.GetComponent<Rigidbody2D>();
            cloneBody.rotation = 225;
            shootpos.x = shootpos.x + dist;
            clone = (GameObject)Instantiate(tinyFireball, shootpos, new Quaternion());
            cloneBody = clone.GetComponent<Rigidbody2D>();
            cloneBody.rotation = 270;
            shootpos.x = shootpos.x + dist;
            clone = (GameObject)Instantiate(tinyFireball, shootpos, new Quaternion());
            cloneBody = clone.GetComponent<Rigidbody2D>();
            cloneBody.rotation = 315;
            Destroy(gameObject);
        }
        else if (timer > timerGoal && !breaking)
        {
            coll.enabled = false;
            bulletSpeed = 0;
            breaking = true;
            timer = 0;
        }
    }

}
