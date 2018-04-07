using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_controller : MonoBehaviour {
    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name.Contains("fodderA")){
            other.gameObject.GetComponent<fodder_AI>().damage(gameObject.transform.position);
        //}
    }
}
