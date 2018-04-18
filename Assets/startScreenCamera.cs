using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startScreenCamera : MonoBehaviour {
    public bool onStart;
    public bool isAtScreen;
    double distanceTraveled;

    float pauseTimer;
    float pauseTimerGoal;

    public AudioClip selectSound;
    AudioSource source;

   public GameObject selectFlash;
	// Use this for initialization
	void Start () {
        source = this.GetComponent<AudioSource>();
        onStart = true;
        isAtScreen = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Attack"))
        {
            pauseTimer = 0;
            Vector3 position = transform.position;
            position.z += 8;
            source.PlayOneShot(selectSound, 0.5f);
            Instantiate(selectFlash, position, new Quaternion());
            onStart = false;
            pauseTimerGoal = 0.5f;
        }

    }

    void FixedUpdate()
    {
        if(!onStart && !isAtScreen)
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer > pauseTimerGoal)
            {
                this.transform.Translate(new Vector3(0, -3 * Time.deltaTime, 0));
                if (this.transform.position.y <= 0)
                {
                    isAtScreen = true;
                }
            }
        }
    }
}
