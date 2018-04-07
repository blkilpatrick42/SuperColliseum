using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete_after_Seconds : MonoBehaviour {
    public float deleteAfter;
    private float timer;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer += Time.deltaTime;
		if(timer > deleteAfter)
        {
            Destroy(gameObject);
        }
	}
}
