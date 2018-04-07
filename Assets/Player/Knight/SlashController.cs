using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour {
    private Rigidbody2D body;
    float timer;
    float timerGoal;
    // Use this for initialization
    void Start () {
        body = this.GetComponent<Rigidbody2D>();
        timer = 0;
        timerGoal = 0.5f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float bulletSpeed = 3;
        body.velocity = transform.right * bulletSpeed;
        timer += Time.deltaTime;
        if(timer > timerGoal)
        {
            Destroy(gameObject);
        }
    }

  

}
