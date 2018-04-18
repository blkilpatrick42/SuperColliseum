using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_Knight : MonoBehaviour {
    private Animator animator;  //this objects animation controller
    private Rigidbody2D body;   //the rigidBody assosciated with this object
    private PlayerController_Base pcBase;       //pointer used to reference PlayerController_Base
    private bool attacking; //is the player currently attacking? if so, true
    private float attacktimer; //timer used to measure how long until a player's attack is finished
    private bool usingSpecial;
    private bool charging;
    private bool charged;
    private float chargetimer;
    private float chargetimerGoal;

    public GameObject swordLeft;
    public GameObject swordRight;
    public GameObject swordUp;
    public GameObject swordDown;
    private GameObject currentSword;

    public AudioClip charge;
    private AudioSource source;

    public GameObject shieldLeft;
    public GameObject shieldRight;
    public GameObject shieldUp;
    public GameObject shieldDown;
    private GameObject currentShield;

    public GameObject slash;

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
        usingSpecial = false;
        chargetimer = 0;
        chargetimerGoal = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Exit"))
        {
            Application.Quit();
        }
        UpdateAnimator();
        if (Input.GetButtonDown("Attack") && !attacking && !usingSpecial && !pcBase.dead){
            attack();
        }

        if (Input.GetButtonDown("Special") && !usingSpecial && !attacking && !pcBase.dead)
        {
            usingSpecial = true;
            pcBase.setCanMove(false);
            if (pcBase.facingDirection == 0)
            {
                currentShield = Instantiate(shieldUp, body.position, new Quaternion());
            }
            if (pcBase.facingDirection == 1)
            {
                currentShield = Instantiate(shieldRight, body.position, new Quaternion());
            }
            if (pcBase.facingDirection == 2)
            {
                currentShield = Instantiate(shieldDown, body.position, new Quaternion());
            }
            if (pcBase.facingDirection == 3)
            {
                currentShield = Instantiate(shieldLeft, body.position, new Quaternion());
            }
        }

        if(usingSpecial && Input.GetButtonUp("Special"))
        {
            usingSpecial = false;
            pcBase.setCanMove(true);
            Destroy(currentShield);
        }


        

        
    }

    void FixedUpdate()
    {
        attacktimer += Time.deltaTime;
        if (charging)
        {
            chargetimer += Time.deltaTime;
        }

        if(chargetimer >= chargetimerGoal && !charged)
        {
            source.PlayOneShot(charge, 0.5f);
            charged = true;
        }

        //if the attack is over, say we are no longer attacking and we can move again
        if (attacking && attacktimer > 0.25f)
        {
            attacking = false;
            Destroy(currentSword);

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
            attack();
            chargetimer = 0;

            if (pcBase.facingDirection == 0)
            {
                GameObject clone = (GameObject)Instantiate(slash, body.position, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 90;
            }
            if (pcBase.facingDirection == 1)
            {
                GameObject clone = (GameObject)Instantiate(slash, body.position, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 0;
            }
            if (pcBase.facingDirection == 2)
            {
                GameObject clone = (GameObject)Instantiate(slash, body.position, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 270;
            }
            if (pcBase.facingDirection == 3)
            {
                GameObject clone = (GameObject)Instantiate(slash, body.position, new Quaternion());
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
            currentSword = Instantiate(swordUp, body.position, new Quaternion());
        }
        if (pcBase.facingDirection == 1)
        {
            currentSword = Instantiate(swordRight, body.position, new Quaternion());
        }
        if (pcBase.facingDirection == 2)
        {
            currentSword = Instantiate(swordDown, body.position, new Quaternion());
        }
        if (pcBase.facingDirection == 3)
        {
            currentSword = Instantiate(swordLeft, body.position, new Quaternion());
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
