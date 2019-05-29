using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderControl : MonoBehaviour {
    
    GameObject quad;
    

    // Use this for initialization
    void Start () {
        quad = Resources.Load("Prefabs/Quad") as GameObject;

        Instantiate(quad, new Vector3(0, 10, 30), Quaternion.Euler(0, 0, 180));
        Instantiate(quad, new Vector3(-100, 10, 30), Quaternion.Euler(0, 0, 180));

    }
	
	// Update is called once per frame
	void Update () {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Quad")
        {
            other.transform.position = new Vector3(-100, 10, 30);
        }
    }
}
