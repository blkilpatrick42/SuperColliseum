using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireWallScript : MonoBehaviour {
    float timer;
    float timerGoal;
	// Use this for initialization
	void Start () {
        timer = 0;
        timerGoal = 4.6f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer += Time.deltaTime;
        if(timer > timerGoal)
        {
            Destroy(gameObject);
        }
	}
}
