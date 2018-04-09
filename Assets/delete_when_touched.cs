using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete_when_touched : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D co)
    {
        if(co.gameObject.tag == "P")
        {
            Destroy(gameObject);
        }
    }
}
