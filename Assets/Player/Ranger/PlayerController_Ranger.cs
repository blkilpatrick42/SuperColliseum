using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_Ranger : MonoBehaviour {

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
    private float specialtimer;
    private float specialtimerGoal;

    public GameObject arrow;

    // Use this for initialization
    void Start()
    {
        //assosciate the animator and body wth this object's animation controller, rigidbody, and pcbase
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        pcBase = this.GetComponent<PlayerController_Base>();
        attacking = false;
        charging = false;
        usingSpecial = false;
        specialtimer = 0;
        chargetimer = 0;
        chargetimerGoal = 0.4f;
        specialtimerGoal = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Dev"))
        {
            SceneManager.LoadScene("MageTest", LoadSceneMode.Single);
        }
        UpdateAnimator();
        if (Input.GetButtonDown("Attack") && !charging)
        {
            charging = true;
            pcBase.setCanMove(false);
        }

        if (Input.GetButtonDown("Special") && specialtimer > specialtimerGoal && !charging)
        {
            pcBase.setSpeed(6);
            specialtimer = 0;
        }

    }

    void FixedUpdate()
    {
        specialtimer += Time.deltaTime;

        if(specialtimer > 1)
        {
            pcBase.setSpeed(pcBase.baseSpeed);
        }
        if (charging)
        {
            chargetimer += Time.deltaTime;
        }

        if (chargetimer >= chargetimerGoal)
        {
            charged = true;
        }

        //exit out of charging if attack is released before it's finished
        if (charging && !Input.GetButton("Attack") && !charged && chargetimer > 0.25)
        {
            charging = false;
            chargetimer = 0;
            pcBase.setCanMove(true);

            if (pcBase.facingDirection == 0)
            {
                Vector2 shootPosition = body.position;
                shootPosition.y += 0.7f;
                GameObject clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 90;
            }
            if (pcBase.facingDirection == 1)
            {
                Vector2 shootPosition = body.position;
                shootPosition.x += 0.7f;
                GameObject clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 0;
            }
            if (pcBase.facingDirection == 2)
            {
                Vector2 shootPosition = body.position;
                shootPosition.y -= 0.8f;
                GameObject clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 270;
            }
            if (pcBase.facingDirection == 3)
            {
                Vector2 shootPosition = body.position;
                shootPosition.x -= 0.7f;
                GameObject clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 180;
            }
        }
        else if (charging && !Input.GetButton("Attack") && charged) //preform charged attack
        {
            float spread = 15;
            float spacing = 0.2f;
            charged = false;
            charging = false;
            chargetimer = 0;
            pcBase.setCanMove(true);

            if (pcBase.facingDirection == 0)
            {
                Vector2 shootPosition = body.position;
                shootPosition.y += 0.7f;
                GameObject clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 90;
                shootPosition.x += spacing;
                clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 90-spread;
                shootPosition.x -= 2*spacing;
                clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 90 + spread;
            }
            if (pcBase.facingDirection == 1)
            {
                Vector2 shootPosition = body.position;
                shootPosition.x += 0.7f;
                GameObject clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 0;
                shootPosition.y += spacing;
                clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 0 + spread;
                shootPosition.y -= 2*spacing;
                clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 0 - spread;
            }
            if (pcBase.facingDirection == 2)
            {
                Vector2 shootPosition = body.position;
                shootPosition.y -= 0.8f;
                GameObject clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 270;
                shootPosition.x += spacing;
                clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 270 + spread;
                shootPosition.x -= 2*spacing;
                clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 270 - spread;
            }
            if (pcBase.facingDirection == 3)
            {
                Vector2 shootPosition = body.position;
                shootPosition.x -= 0.7f;
                GameObject clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 180;
                shootPosition.y += spacing;
                clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 180 - spread;
                shootPosition.y -= 2*spacing;
                clone = (GameObject)Instantiate(arrow, shootPosition, new Quaternion());
                cloneBody = clone.GetComponent<Rigidbody2D>();
                cloneBody.rotation = 180 + spread;
            }
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
