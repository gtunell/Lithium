using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronController : MonoBehaviour {

    public GameObject hydrogen;

    private float angle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        angle += 2 * Time.deltaTime;

        var offset = new Vector3(-0.67f, 
            Mathf.Cos(angle) * 1.15f, Mathf.Sin(angle));

        transform.position = hydrogen.transform.position + offset;
	}
}
