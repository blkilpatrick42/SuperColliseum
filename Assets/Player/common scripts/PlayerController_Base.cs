using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Base : MonoBehaviour {
    public int facingDirection; //the direction the player is facing, 0 = up, 1 = right, 2 = down, 3 = left
    public bool still;          //keeps track of if the player is currently moving
    public float baseSpeed;     //the base movement speed of this player character
    public float speed;
    private Animator animator;  //this objects animation controller
    private Rigidbody2D body;   //the rigidBody assosciated with this object
    public bool canMove;        //used to determine if the player is in a state where they can move
    public bool canOnlyTurn;

    // Use this for initialization
    void Start () {
        //assosciate the animator and body wth this object's animation controller and rigidbody
        animator = this.GetComponent<Animator>();
        body = this.GetComponent<Rigidbody2D>();
        canMove = true;
        speed = baseSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateAnimator(); 
    }

    void FixedUpdate()
    {
        UpdateMovement();
        UpdateAnimator();
    }

    //controls the physical movement of the player.
    //ensures direction and still are set to the correct values
    void UpdateMovement()
    {
        if (canMove)
        {
            float movementSpeed = speed;

            //get input from the input axes
            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");

            //Multiply those directions by time and movement speed,
            //convert into the vector direction, which represents the velocity
            var y = vertical * Time.deltaTime * movementSpeed;
            var x = horizontal * Time.deltaTime * movementSpeed;
            Vector2 direction = new Vector2(x, y);

            //Debug.Log(Input.GetAxis("Vertical"));
            //Debug.Log(Input.GetAxis("Horizontal"));

            //translate the player by the velocity vector named direction.
            if (!canOnlyTurn)
            {
                body.MovePosition(body.position + direction);
            }

            //determine the facing direction
            //up
            if (Mathf.Abs(horizontal) <= 0.5 && vertical > 0)
            {
                still = false;
                facingDirection = 0;
            }
            //down
            else if (Mathf.Abs(horizontal) < 0.5 && vertical < 0)
            {
                still = false;
                facingDirection = 2;
            }
            //right
            else if (Mathf.Abs(vertical) <= 0.5 && horizontal > 0 || Mathf.Abs(vertical) >= 0.5 && horizontal > 0.5)
            {
                still = false;
                facingDirection = 1;
            }
            //left
            else if (Mathf.Abs(vertical) < 0.5 && horizontal < 0 || Mathf.Abs(vertical) >= 0.5 && horizontal < 0.5)
            {
                still = false;
                facingDirection = 3;
            }
            else if (vertical == 0 && horizontal == 0)
            {
                still = true;
            }
          
        }
    }

    //send the bool still and int facingDirection as messages to update the animator
    void UpdateAnimator()
    {
        animator.SetBool("Still", still);
        animator.SetInteger("Direction", facingDirection);
    }

    //used to toggle the players ability to move
    public void setCanMove(bool setValue)
    {
        canMove = setValue;
        if (!setValue)
        {
            body.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void setCanOnlyTurn(bool setValue)
    {
        canOnlyTurn = setValue;
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
