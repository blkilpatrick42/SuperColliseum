using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreReadout : MonoBehaviour {
    PlayerController_Base pBase;
    
	// Use this for initialization
	void Start () {
        pBase = GameObject.FindGameObjectWithTag("P").GetComponent<PlayerController_Base>();
	}
	
	// Update is called once per frame
	void Update () {
        string score = "SCORE: ";
        string number = string.Format("{0:000000}" , pBase.score) ;
        this.GetComponent<Text>().text = score + number;
	}
}
