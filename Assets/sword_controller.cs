using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_controller : MonoBehaviour {
    public AudioClip swordSlash;
    private AudioSource source;
    void start()
    {
        source = gameObject.GetComponent<AudioSource>();
        source.PlayOneShot(swordSlash, 1f);
    }
    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name.Contains("fodderA")){
            other.gameObject.GetComponent<fodder_AI>().damage(gameObject.transform.position);
        //}
    }
}
