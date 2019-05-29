using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour {

    public GameObject neon;
    public GameObject robot;

    bool collided;
    float timeSinceCollision;

	// Use this for initialization
	void Start () {
        collided = false;
        timeSinceCollision = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (collided && timeSinceCollision > 3f)
        {
            Destroy(gameObject);
        }
        else if (collided)
        {
            timeSinceCollision += Time.deltaTime;
        }
        else
        {
            transform.position = transform.position - new Vector3(0, 0, 1) * 15 * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Energy Flare")
        {
            print("here");
            collided = true;
            this.GetComponent<Rigidbody>().AddForce(transform.forward);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Energy Flare")
        {
            collided = true;
            this.GetComponent<Rigidbody>().AddForce(-transform.forward * 4000 + transform.up * 1000);
        }
    }
}
