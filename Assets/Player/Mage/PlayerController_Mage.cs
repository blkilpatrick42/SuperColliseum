using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Mage : MonoBehaviour {
    private Animator animator;  //this objects animation controller
    private Rigidbody2D body;   //the rigidBody assosciated with this object
    private PlayerController_Base pcBase;       //pointer used to reference PlayerController_Base
    private bool attacking; //is the player currently attacking? if so, true
    private float attacktimer; //timer used to measure how long until a player's attack is finished
    private float specialtimer;
    private float firewallTimer;
    private bool usingSpecial;
    private bool charging;
    private bool charged;
    private float chargetimer;
    private float chargetimerGoal;

    public AudioClip charge;
    private AudioSource source;

    public GameObject fireball;
    public GameObject chargeFireball;
    public GameObject firewall;
    int numOfFirewalls = 5;

    // Use this for initialization
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        //assosciate the animator and body wth this object's animation controller, rigidbody, and pcbase
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        pcBase = this.GetComponent<PlayerController_Base>();
        attacking = false;
        charging = false;
        chargetimer = 0;
        chargetimerGoal = 0.6f;
        specialtimer = 0;
        firewallTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Exit"))
        {
            Application.Quit();
        }
        UpdateAnimator();
        if (Input.GetButtonDown("Attack") && !attacking && !usingSpecial && !pcBase.dead)
        {
            attack();
        }

        if (Input.GetButton("Special") && !attacking && specialtimer > 0.2 && numOfFirewalls > 0 && !pcBase.dead)
        {
            specialtimer = 0;
            numOfFirewalls = numOfFirewalls - 1;
            Instantiate(firewall, body.position, new Quaternion());
        }

    }

    void FixedUpdate()
    {
        attacktimer += Time.deltaTime;
        specialtimer += Time.deltaTime;
        if (numOfFirewalls < 5)
        {
            firewallTimer += Time.deltaTime;
        }

        if(numOfFirewalls < 5 && firewallTimer > 2.6)
        {
            firewallTimer = 0;
            numOfFirewalls = numOfFirewalls + 1;
        }

        if (charging)
        {
            chargetimer += Time.deltaTime;
        }

        if (chargetimer >= chargetimerGoal && !charged)
        {
            source.PlayOneShot(charge, 0.5f);
            charged = true;
        }

        //if the attack is over, say we are no longer attacking and we can move again
        if (attacking && attacktimer > 0.25f)
        {
            attacking = false;
            if (!Input.GetButton("Attack"))
            {
                pcBase.setCanMove(true);
            }
            else
            {
                charging = true;
            }
        }

        //exit out of charging if attack is released before it's finished
        if (charging && !Input.GetButton("Attack") && !charged)
        {
            charging = false;
            chargetimer = 0;
            pcBase.setCanMove(true);
        }
        else if (charging && !Input.GetButton("Attack") && charged) //preform charged attack
        {
            charged = false;
            charging = false;
            //attack();
            chargetimer = 0;
            pcBase.setCanMove(true);

            if (pcBase.facingDirection == 0)
            {
                Vector2 shootPosition = body.position;
                shootPosition.y += 0.7f;
                GameObject clone = (GameObject)Instantiate(chargeFireball, shootPosition, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 90;
            }
            if (pcBase.facingDirection == 1)
            {
                Vector2 shootPosition = body.position;
                shootPosition.x += 0.7f;
                GameObject clone = (GameObject)Instantiate(chargeFireball, shootPosition, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 0;
            }
            if (pcBase.facingDirection == 2)
            {
                Vector2 shootPosition = body.position;
                shootPosition.y -= 0.7f;
                GameObject clone = (GameObject)Instantiate(chargeFireball, shootPosition, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 270;
            }
            if (pcBase.facingDirection == 3)
            {
                Vector2 shootPosition = body.position;
                shootPosition.x -= 0.7f;
                GameObject clone = (GameObject)Instantiate(chargeFireball, shootPosition, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 180;
            }
        }
    }

    void attack()
    {
        attacking = true;
        pcBase.setCanMove(false);
        attacktimer = 0;
        if (pcBase.facingDirection == 0)
        {
            Vector2 shootPosition = body.position;
            shootPosition.y += 0.7f;
            GameObject clone = (GameObject)Instantiate(fireball, shootPosition, new Quaternion());
            Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
            cloneBody.rotation = 90;
        }
        if (pcBase.facingDirection == 1)
        {
            Vector2 shootPosition = body.position;
            shootPosition.x += 0.7f;
            GameObject clone = (GameObject)Instantiate(fireball, shootPosition, new Quaternion());
            Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
            cloneBody.rotation = 0;
        }
        if (pcBase.facingDirection == 2)
        {
            Vector2 shootPosition = body.position;
            shootPosition.y -= 0.7f;
            GameObject clone = (GameObject)Instantiate(fireball, shootPosition, new Quaternion());
            Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
            cloneBody.rotation = 270;
        }
        if (pcBase.facingDirection == 3)
        {
            Vector2 shootPosition = body.position;
            shootPosition.x -= 0.7f;
            GameObject clone = (GameObject)Instantiate(fireball, shootPosition, new Quaternion());
            Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
            cloneBody.rotation = 180;
        }
    }

    //send the bool still and int facingDirection as messages to update the animator
    void UpdateAnimator()
    {
        animator.SetBool("Attacking", attacking);
        animator.SetBool("Charging", charging);
        animator.SetBool("Charged", charged);
        animator.SetBool("Special", usingSpecial);
    }
}
