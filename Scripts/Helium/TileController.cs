﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    float speed;

	// Use this for initialization
	void Start () {
        speed = 6;
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position = this.transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
