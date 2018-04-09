using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_script : MonoBehaviour {
    itemManager manager;
    public AudioClip pickup;
    private AudioSource source;
    public string itemName;
    void Start()
    {
        source = GameObject.FindGameObjectWithTag("itemManager").GetComponent<AudioSource>();
        manager = GameObject.FindGameObjectWithTag("itemManager").GetComponent<itemManager>();
    }
        void OnCollisionEnter2D(Collision2D co)
    {
        if (co.gameObject.tag == "P" && manager.numItems < manager.maxItems)
        {
            source.PlayOneShot(pickup, 0.5f);
            manager.acquireItem(itemName);
            Destroy(gameObject);
        }
    }
}
