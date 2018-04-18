using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fodder_AI : MonoBehaviour {
    public bool approachesPlayer;
    public float approachDistance;
    public LayerMask mask;
    private Animator animator;
    private Rigidbody2D body;
    private BoxCollider2D coll;
    public float movementSpeed;

    private bool rightFacing;
    private bool damaged;

    public int healthPoints;

    public GameObject explosion;
    public GameObject player;

    float damageTimer;
    public float damageTimerGoal;
    public float wanderTimer;
    public float wandertimerGoal;
    Vector2 wanderDir;

    float checkRayCastTimer;
    float checkRayCastTimerGoal;

    public GameObject drop;
    public int dropRate;
	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        body = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<BoxCollider2D>();
        var playerList = GameObject.FindGameObjectsWithTag("P");
        player = playerList[0];
        damaged = false;
        rightFacing = true;
        checkRayCastTimerGoal = 0.1f;
        //wanderTimer = 2;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateAnimator();
	}

    void FixedUpdate()
    {
        checkRayCastTimer += Time.deltaTime;
        if (player != null)
        {
            Vector2 toPlayer = new Vector2(player.transform.position.x - body.position.x, player.transform.position.y - body.position.y);
            if (toPlayer.magnitude < approachDistance && checkRayCastTimer > checkRayCastTimerGoal)
            {
                checkRayCastTimer = 0;
                RaycastHit2D hit = Physics2D.Raycast(body.position, toPlayer.normalized, Vector2.Distance(body.position, player.transform.position), mask);
                if (hit.collider == null && !damaged && approachesPlayer)
                {
                    //Debug.DrawLine(body.position, body.position + toPlayer.normalized* Vector2.Distance(body.position, player.transform.position), Color.green, 0f);
                    moveInDirection(toPlayer.normalized);
                }
                else if (!damaged)
                {
                    wanderTimer += Time.deltaTime;
                    if (wanderTimer > wandertimerGoal)
                    {
                        wanderTimer = 0;
                        wanderDir = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)).normalized;
                        //Debug.DrawLine(body.position, body.position + wanderDir, Color.green, 0f);
                    }
                    moveInDirection(wanderDir);
                    //Debug.DrawLine(body.position, body.position + toPlayer.normalized * Vector2.Distance(body.position, player.transform.position), Color.red, 0f);
                }
            }
            else if (!damaged)
            {
                wanderTimer += Time.deltaTime;
                if (wanderTimer > wandertimerGoal)
                {
                    wanderTimer = 0;
                    wanderDir = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)).normalized;
                    //Debug.DrawLine(body.position, body.position + wanderDir, Color.green, 0f);
                }
                moveInDirection(wanderDir);
                //Debug.DrawLine(body.position, body.position + toPlayer.normalized * Vector2.Distance(body.position, player.transform.position), Color.red, 0f);
            }
        }
        damageTimer += Time.deltaTime;
        if(damageTimer > damageTimerGoal)
        {
            coll.enabled = true;
            damaged = false;
            if (healthPoints <= 0)
            {
                Instantiate(explosion, body.position, new Quaternion());
                if(Random.Range(1,dropRate) == 1)
                {
                    Instantiate(drop, body.position, new Quaternion());
                }
                player.GetComponent<PlayerController_Base>().score++;
                Destroy(gameObject);
            }
        }       
    }

    void moveInDirection(Vector2 direction)
    {
        wanderDir = direction;
        var y = direction.y * Time.deltaTime * movementSpeed;
        var x = direction.x * Time.deltaTime * movementSpeed;
        if(x > 0)
        {
            rightFacing = true;
        }
        else
        {
            rightFacing = false;
        }
        Vector2 dir = new Vector2(x, y);
        body.MovePosition(body.position + dir);
    }

    void OnCollisionEnter2D(Collision2D co)
    {
        
        if(co.gameObject.tag == "Sword" || co.gameObject.tag == "Arrow" || co.gameObject.tag == "Fireball" || co.gameObject.tag == "FireWall")
        {
            Vector2 toCo = new Vector2(co.gameObject.transform.position.x - body.position.x, co.gameObject.transform.position.y - body.position.y);
            if (!damaged)
            {
                damageTimer = 0;
                damaged = true;
                coll.enabled = false;
                healthPoints -= 1;
                toCo = -toCo.normalized;
                body.AddForce(toCo*500);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D co)
    {       
        if (co.gameObject.tag == "Sword" || co.gameObject.tag == "FireWall")
        {
            Vector2 toCo = new Vector2(co.gameObject.transform.position.x - body.position.x, co.gameObject.transform.position.y - body.position.y);
           //Vector2 toCo = new Vector2(player.transform.position.x - body.position.x, player.transform.position.y - body.position.y);
            damageTimer = 0;
            damaged = true;
            coll.enabled = false;
            healthPoints -= 1;
            toCo = -toCo.normalized;
            body.AddForce(toCo * 500);
        }
    }

    public void damage(Vector2 coposition)
    {
        Vector2 toCo = new Vector2(coposition.x - body.position.x, coposition.y - body.position.y);
        if (!damaged)
        {
            damageTimer = 0;
            damaged = true;
            //coll.enabled = false;
            healthPoints -= 1;
            toCo = -toCo.normalized;
            body.AddForce(toCo * 500);
        }
    }

    void UpdateAnimator()
    {
        animator.SetBool("rightfacing", rightFacing);
        animator.SetBool("damaged", damaged);
    }
}
