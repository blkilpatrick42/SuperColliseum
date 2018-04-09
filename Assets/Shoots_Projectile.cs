using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoots_Projectile : MonoBehaviour {
    public float minDistance;
    public int numProjectiles;
    public GameObject projectile;
    public GameObject projectile1;
    public GameObject projectile2;
    public GameObject projectile3;
    GameObject player;
    Rigidbody2D body;
    public LayerMask myMask;
    public float timer;
    public float timerGoal;

    public List<GameObject> projectiles;
	// Use this for initialization
	void Start () {
        if (projectile != null)
        {
            projectiles.Add(projectile);
        }
        if (projectile1 != null)
        {
            projectiles.Add(projectile1);
        }
        if (projectile2 != null)
        {
            projectiles.Add(projectile2);
        }
        if (projectile3 != null)
        {
            projectiles.Add(projectile3);
        }
        player = GameObject.FindGameObjectWithTag("P");
        body = this.GetComponent<Rigidbody2D>();
        timer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 toPlayer = new Vector2(player.transform.position.x - body.position.x, player.transform.position.y - body.position.y);
        RaycastHit2D hit = Physics2D.Raycast(body.position, toPlayer.normalized, Vector2.Distance(body.position, player.transform.position), myMask);
        if (hit.collider == null && timer>=timerGoal && toPlayer.magnitude < minDistance)
        {
            for (int i = 0; i < numProjectiles; i++)
            {
                Instantiate(projectiles[i], body.position, new Quaternion());
            }
            timer = 0;
        }
    }
    void FixedUpdate()
    {
        timer += Time.deltaTime;
    }
}
