using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {
    public float timeBetweenSpawns;
    float turnoffDistance;
    public int maxSpawns;
    private float timer;

    public List<GameObject> spawnBoys;
    GameObject player;

    public GameObject spawnObject;
	// Use this for initialization
	void Start () {
        spawnBoys.Add(Instantiate(spawnObject, transform.position, new Quaternion()));
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        player = GameObject.FindGameObjectWithTag("P");
        timer = 0;
        turnoffDistance = 5;
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
        Vector2 toPlayer = (Vector2)player.transform.position - (Vector2)this.transform.position;
        if(timer > timeBetweenSpawns && toPlayer.magnitude > turnoffDistance)
        {
            if (spawnBoys.Count < maxSpawns)
            {
                spawnBoys.Add(Instantiate(spawnObject, transform.position, new Quaternion()));
            }
            timer = 0;
        }
	}
}
