using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {
    private Animator animator;  //this objects animation controller
    private Rigidbody2D body;
    private BoxCollider2D coll;
    public float bulletSpeed = 3.5f;
    float timer;
    public float timerGoal;
    float timerGoal2;
    bool breaking;
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
        animator.SetBool("Break", breaking);
        
        body.velocity = transform.right * bulletSpeed;
        timer += Time.deltaTime;
        if (timer > timerGoal2 && breaking)
        {
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
