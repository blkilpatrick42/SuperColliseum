using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour {


    private Animator animator;
    private Rigidbody2D rb2;
    public GameObject knight;
    public GameObject archer;
    public GameObject mage;

    public GameObject baseKnight;
    public GameObject baseRanger;
    public GameObject baseMage;

    public GameObject knightCard;
    public GameObject mageCard;
    public GameObject rangerCard;
    public GameObject camera;

    public bool selected;
    public bool flag;
    public bool flag2;
    public Vector2 kPosition;
    public int count;

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        animator = this.GetComponent<Animator>();
        flag = false;
        flag2 = false;
        baseMage.SetActive(false);
        baseRanger.SetActive(false);
        heart1.SetActive(false);
        heart2.SetActive(false);
        heart3.SetActive(false);
        heart4.SetActive(false);
        //DoubleCheck.SetActive(false);
        //DoubleCheckObj.SetActive(false);
        count = 0;
    }
    void Update()
    {
        if (Input.GetButtonDown("Exit"))
        {
            Application.Quit();
        }
        animator.SetBool("moving", selected);
        var Horizontal = Input.GetAxis("Horizontal");

        if (this.gameObject.tag == "Knight" && selected)
        {

            mageCard.GetComponent<SpriteRenderer>().enabled = false;
            knightCard.GetComponent<SpriteRenderer>().enabled = true;
            rangerCard.GetComponent<SpriteRenderer>().enabled = false;

            baseKnight.SetActive(true);
            baseMage.SetActive(false);
            baseRanger.SetActive(false);


            if (Horizontal > 0 && !flag&& !flag2)
            {
                archer.GetComponent<SelectionScript>().flag = true;
                mage.GetComponent<SelectionScript>().flag = true;
                knight.GetComponent<SelectionScript>().flag = true;
                mage.GetComponent<SelectionScript>().selected = true;
                selected = false;
            }
            if (Input.GetButtonDown("Attack"))
            {
                if (camera.GetComponent<startScreenCamera>().isAtScreen)
                {

                    SceneManager.LoadScene("KnightTest");

                }

                count = 1;
            }
            else if(Input.GetButtonDown("Cancel"))
            {
                DoubleCheck.enabled = false;
                buttonA.enabled = false;
                buttonB.enabled = false;
                HeroText.enabled = true;
                basicText.enabled = true;
                Stats.enabled = true;
                flag2 = false;
                count = 0;
            }

        }

        else if (this.gameObject.tag == "Mage" && selected)
        {

            mageCard.GetComponent<SpriteRenderer>().enabled = true;
            knightCard.GetComponent<SpriteRenderer>().enabled = false;
            rangerCard.GetComponent<SpriteRenderer>().enabled = false;


            baseMage.SetActive(true);
            baseKnight.SetActive(false);
            baseRanger.SetActive(false);

            
            if (Horizontal > 0 && !flag&& !flag2)
            {
                archer.GetComponent<SelectionScript>().flag = true;
                mage.GetComponent<SelectionScript>().flag = true;
                knight.GetComponent<SelectionScript>().flag = true;
                archer.GetComponent<SelectionScript>().selected = true;
                selected = false;

            }
            else if (Horizontal < 0 && !flag&& !flag2)
            {
                archer.GetComponent<SelectionScript>().flag = true;
                mage.GetComponent<SelectionScript>().flag = true;
                knight.GetComponent<SelectionScript>().flag = true;
                knight.GetComponent<SelectionScript>().selected = true;
                selected = false;
            }
            if (Input.GetButtonDown("Attack"))
            {
                if (camera.GetComponent<startScreenCamera>().isAtScreen)
                {
                    SceneManager.LoadScene("MageTest");
                }


            }
        }
        else if (this.gameObject.tag == "Ranger" && selected)
        {

            mageCard.GetComponent<SpriteRenderer>().enabled = false;
            knightCard.GetComponent<SpriteRenderer>().enabled = false;
            rangerCard.GetComponent<SpriteRenderer>().enabled = true;

            baseRanger.SetActive(true);
            baseMage.SetActive(false);
            baseKnight.SetActive(false);


            if (Horizontal < 0 && !flag&& !flag2)
            {
                archer.GetComponent<SelectionScript>().flag = true;
                mage.GetComponent<SelectionScript>().flag = true;
                knight.GetComponent<SelectionScript>().flag = true;
                mage.GetComponent<SelectionScript>().selected = true;
                selected = false;
            }
            if (Input.GetButtonDown("Attack"))
            {
                if (camera.GetComponent<startScreenCamera>().isAtScreen)
                {
                    SceneManager.LoadScene("RangerTest");
                }


            }
        }
        if (Horizontal == 0)
        {
            archer.GetComponent<SelectionScript>().flag = false;
            mage.GetComponent<SelectionScript>().flag = false;
            knight.GetComponent<SelectionScript>().flag = false;
            flag = false;
        }

    }
    /*
     public Text HeroText;
    public Text basicText;
    public Text Stats;
    private Rigidbody2D rb2;
    public GameObject knight;
    public GameObject archer;
    public GameObject mage;
    public GameObject textbox;
    public bool selected;
    public bool flag;
    public Vector2 kPosition;
	// Use this for initialization
    void Start()
    {
        HeroText.text = "";
        basicText.text = "";
        Stats.text = "";
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        var Horizontal = Input.GetAxis("Horizontal");
        if (this.gameObject.tag == "Knight" && selected)
        {
            HeroText.enabled = true;
            HeroText.text = "Knight: ";
            basicText.text = "Health: ";
            Stats.text = "Close Range\n Basic Attack: Sweeping Attack\n Charged Attack: Mid Range Wide Attack\n Special: Blocks and Reflects enemies attacks,\n" +
                "    " + " But does not reflect boss attacks";
            if (Horizontal > 0 && !flag)
            {
                archer.GetComponent<Knight>().flag = true;
                mage.GetComponent<Knight>().flag = true;
                knight.GetComponent<Knight>().flag = true;
                archer.GetComponent<Knight>().selected = true;
                selected = false;
            }
        }

        else if (this.gameObject.tag == "Mage" && selected)
        {
            HeroText.enabled = true;
            HeroText.text = "Mage: ";
            basicText.text = "Health: ";
            Stats.text = "Mid Range\n Basic Attack: Fireball Shot\n Charged Attack: Fireball Shot that explodes \n" +
                "    " + "sending fireballs in all directions\n Special: Leaves a trail of Fire for a few seconds\n";
            if (Horizontal < 0 && !flag)
            {
                archer.GetComponent<Knight>().flag = true;
                mage.GetComponent<Knight>().flag = true;
                knight.GetComponent<Knight>().flag = true;
                archer.GetComponent<Knight>().selected = true;
                selected = false;
            }
        }
        else if (this.gameObject.tag == "Archer" && selected)
        {
            HeroText.enabled = true;
            //textbox.SetActive(true);
            HeroText.text = "Archer: ";
            basicText.text = "Health: ";
            Stats.text = "Long Range\n Basic Attack: Arrow Shot (arrow will go until\n" +
                "    " + " it hits a wall or hits 3 enemies)\n Charged Attack: Three Arrow Spread\n Special: Movement Increased by a factor of 2";
            if (Horizontal > 0 && !flag)
            {
                archer.GetComponent<Knight>().flag = true;
                mage.GetComponent<Knight>().flag = true;
                knight.GetComponent<Knight>().flag = true;
                mage.GetComponent<Knight>().selected = true;
                selected = false;

            }
            else if (Horizontal < 0 && !flag)
            {
                archer.GetComponent<Knight>().flag = true;
                mage.GetComponent<Knight>().flag = true;
                knight.GetComponent<Knight>().flag = true;
                knight.GetComponent<Knight>().selected = true;
                selected = false;
            }
        }
        if (Horizontal == 0)
        {
            archer.GetComponent<Knight>().flag = false;
            mage.GetComponent<Knight>().flag = false;
            knight.GetComponent<Knight>().flag = false;
            flag = false;
        }
        
    }
    */
}
