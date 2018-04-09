using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour {
    public int type; //0 = start aim at player 1 = homing 2 = use givenDirection
    public int homingChecks;
    public float homingFactor;
    float homingTimer;
    private Vector2 toPlayer;
    public Vector2 givenDirection;
    private Rigidbody2D body;
    public float projectileSpeed;

    public GameObject explosion;
    public GameObject hitExplosion;

    public GameObject trail;
    float timer;
    public float timerGoal;
    // Use this for initialization
    void Start () {
        body = this.GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("P");
	    toPlayer = new Vector2(player.transform.position.x - body.position.x, player.transform.position.y - body.position.y);
        timer = 0;
        homingTimer = 0;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        homingTimer += Time.deltaTime;
        if(timer >= timerGoal)
        {
            Vector3 pos = body.position;
            pos.z -= 1;
            //Instantiate(trail, pos, new Quaternion());
            timer = 0;
        }
        if (homingTimer > homingFactor && homingChecks > 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("P");
            toPlayer = new Vector2(player.transform.position.x - body.position.x, player.transform.position.y - body.position.y);
            homingTimer = 0;
            homingChecks--;
        }

	}

    void FixedUpdate()
    {
        if (type == 0 || type == 1)
        {
            Vector2 toP = toPlayer.normalized;
            var y = toP.y * Time.deltaTime * projectileSpeed;
            var x = toP.x * Time.deltaTime * projectileSpeed;
            Vector2 dir = new Vector2(x, y);
            body.MovePosition(body.position + dir);
        }
        else if (type == 2)
        {
            Vector2 toP = givenDirection.normalized;
            var y = toP.y * Time.deltaTime * projectileSpeed;
            var x = toP.x * Time.deltaTime * projectileSpeed;
            Vector2 dir = new Vector2(x, y);
            body.MovePosition(body.position + dir);
        }
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if( co.gameObject.tag == "Solid" || co.gameObject.tag == "Sword" || co.gameObject.tag == "P" || co.gameObject.tag == "FireWall")
        {
            Destroy(gameObject);
            Instantiate(hitExplosion, body.position, new Quaternion());
        }
        if (co.gameObject.tag == "Shield")
        {
            Instantiate(explosion, body.position, new Quaternion());
            Destroy(gameObject);
        }
    }
}
