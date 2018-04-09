using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour {
    GameObject player;
    private PlayerController_Base pBase;

    public GameObject lostHeart1;
    public GameObject lostHeart2;
    public GameObject lostHeart3;
    public GameObject lostHeart4;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject armor1;
    public GameObject armor2;
    public GameObject armor3;
    public GameObject armor4;

    public List<Image> lostHearts;
    public List<Image> Hearts;
    public List<Image> Armor;

    void Start () {
        player = GameObject.FindGameObjectWithTag("P");    
        pBase = player.GetComponent<PlayerController_Base>();
        lostHearts.Add(lostHeart1.GetComponent<Image>());
        lostHearts.Add(lostHeart2.GetComponent<Image>());
        lostHearts.Add(lostHeart3.GetComponent<Image>());
        lostHearts.Add(lostHeart4.GetComponent<Image>());

        Hearts.Add(heart1.GetComponent<Image>());
        Hearts.Add(heart2.GetComponent<Image>());
        Hearts.Add(heart3.GetComponent<Image>());
        Hearts.Add(heart4.GetComponent<Image>());

        Armor.Add(armor1.GetComponent<Image>());
        Armor.Add(armor2.GetComponent<Image>());
        Armor.Add(armor3.GetComponent<Image>());
        Armor.Add(armor4.GetComponent<Image>());

        //armor4.GetComponent<Image>().enabled = false;
        for (int i = 0; i < 4; i++) {
            if (i + 1 > pBase.MaxHealthPoints)
            {
                lostHearts[i].enabled = false;
                Hearts[i].enabled = false;
                Armor[i].enabled = false;
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < pBase.MaxHealthPoints; i++)
        {
            if (i + 1 > pBase.CurrentHealthPoints)
            {
                lostHearts[i].enabled = true;
                Hearts[i].enabled = false;
            }
            else
            {
                lostHearts[i].enabled = false;
                Hearts[i].enabled = true;
            }
            if (i + 1 <= pBase.CurrentArmorPoints)
            {
                Armor[i].enabled = true;
                lostHearts[i].enabled = false;
                Hearts[i].enabled = false;
            }
            else
            {
                Armor[i].enabled = false;
            }
        }
    }
}

