using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
    public float timeBetweenSpawns;
    private float timer;

    public GameObject spawnObject;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        timer = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer += Time.deltaTime;
        if(timer > timeBetweenSpawns)
        {
            Instantiate(spawnObject, transform.position, new Quaternion());
            timer = 0;
        }
	}
}
