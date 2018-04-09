using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete_after_Seconds : MonoBehaviour {
    public bool blinks;
    public float deleteAfter;
    public float blinkAfter;
    private float timer;
    private SpriteRenderer myRenderer;
	// Use this for initialization
	void Start () {
        myRenderer = this.GetComponent<SpriteRenderer>();
        timer = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer += Time.deltaTime;
		if(timer > deleteAfter)
        {
            Destroy(gameObject);
        }
        if(timer > blinkAfter & blinks)
        {
            if(myRenderer.enabled == true)
            {
                myRenderer.enabled = false;
            }
            else
            {
                myRenderer.enabled = true;
            }
        }
	}
}
