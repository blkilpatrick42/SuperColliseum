using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
    public float timeBetweenSpawns;
    public int maxSpawns;
    private float timer;

    public List<GameObject> spawnBoys;

    public GameObject spawnObject;
	// Use this for initialization
	void Start () {
        spawnBoys.Add(Instantiate(spawnObject, transform.position, new Quaternion()));
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        timer = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (spawnBoys.Count > 0)
        {
            for (int i = 0; i < spawnBoys.Count; i++)
            {
                if (spawnBoys[i] == null)
                {
                    spawnBoys.RemoveAt(i);
                }
            }
        }
        timer += Time.deltaTime;
        if(timer > timeBetweenSpawns)
        {
            if (spawnBoys.Count < maxSpawns)
            {
                spawnBoys.Add(Instantiate(spawnObject, transform.position, new Quaternion()));
            }
            timer = 0;
        }
	}
}
