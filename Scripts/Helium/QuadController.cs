using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadController : MonoBehaviour {

    float speed;

	// Use this for initialization
	void Start () {
        speed = 2;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + new Vector3(1, 0, 0) * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Border")
        {
            // this.transform.position = new Vector3(-100, 10, 30);
        }

    }
}
