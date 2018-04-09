using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemManager : MonoBehaviour {
    public List<string> itemList;
    public int maxItems;
    public int selectedItem;
    public int numItems;

    public GameObject bomb;
    public GameObject health;
    public GameObject armor;
    public GameObject textBox;

    public GameObject player;
    public PlayerController_Base pBase;

    public AudioClip use;
    public AudioClip empty;
    private AudioSource source;

    public GameObject explosion;

    // Use this for initialization
    void Start () {
        source = this.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("P");
        pBase = player.GetComponent<PlayerController_Base>();
        textBox = GameObject.FindGameObjectWithTag("boxNumber");
        maxItems = 5;
        selectedItem = 0;
        numItems = 0;
        for(int i = 0; i < maxItems; i++)
        {
            itemList.Add("empty");
        }
	}
	
	// Update is called once per frame
	void Update () {
        textBox.GetComponent<UnityEngine.UI.Text>().text = "" + (selectedItem + 1);
        if (Input.GetButtonDown("ItemRight"))
        {
            selectedItem = selectedItem + 1;
            if(selectedItem >= maxItems)
            {
                selectedItem = 0;
            }
        }
        if (Input.GetButtonDown("ItemLeft"))
        {
            selectedItem = selectedItem - 1 ;
            if (selectedItem < 0)
            {
                selectedItem = 4;
            }
        }
        
        //what item is it?
        if(itemList[selectedItem] == "empty")
        {
            bomb.GetComponent<Image>().enabled = false;
            health.GetComponent<Image>().enabled = false;
            armor.GetComponent<Image>().enabled = false;
        }
        else if (itemList[selectedItem] == "bomb")
        {
            bomb.GetComponent<Image>().enabled = true;
            health.GetComponent<Image>().enabled = false;
            armor.GetComponent<Image>().enabled = false;
        }
        else if(itemList[selectedItem] == "health")
        {
            bomb.GetComponent<Image>().enabled = false;
            health.GetComponent<Image>().enabled = true;
            armor.GetComponent<Image>().enabled = false;
        }
        else if(itemList[selectedItem] == "armor")
        {
            bomb.GetComponent<Image>().enabled = false;
            health.GetComponent<Image>().enabled = false;
            armor.GetComponent<Image>().enabled = true;
        }

        if (Input.GetButtonDown("Item"))
        {
            useItem();
        }
    }

    void useItem()
    {
        if (itemList[selectedItem] == "bomb")
        {
            source.PlayOneShot(use, 1f);
            itemList[selectedItem] = "empty";
            numItems = numItems - 1;
            Vector3 pos = player.transform.position;
            pos.z -= 1;
            GameObject clone = (GameObject)Instantiate(explosion, pos, new Quaternion());
        }
        else if (itemList[selectedItem] == "health")
        {
            source.PlayOneShot(use, 1f);
            itemList[selectedItem] = "empty";
            numItems = numItems - 1;
            if(pBase.CurrentHealthPoints < pBase.MaxHealthPoints)
            {
                pBase.CurrentHealthPoints = pBase.CurrentHealthPoints + 1;
            }
        }
        else if (itemList[selectedItem] == "armor")
        {
            source.PlayOneShot(use, 1f);
            itemList[selectedItem] = "empty";
            numItems = numItems - 1;
            if (pBase.CurrentArmorPoints < pBase.MaxHealthPoints)
            {
                pBase.CurrentArmorPoints = pBase.CurrentArmorPoints + 1;
            }
        }
        else
        {
            source.PlayOneShot(empty, 0.5f);
        }

    }

    public void acquireItem(string item)
    {
        for(int i = 0; i < maxItems; i++)
        {
            if(itemList[i] == "empty")
            {
                itemList[i] = item;
                numItems = numItems + 1;
                break;
            }
        }
    }
}
