using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadControllers : MonoBehaviour {
    
    GameObject quad, emptyQuad;

    float speed = 20;

	// Use this for initialization
	void Start () {
        quad = Resources.Load("Prefabs/Quad") as GameObject;
        emptyQuad = Resources.Load("Prefabs/EmptyQuad") as GameObject;
        //GameObject obj = Instantiate(emptyQuad, new Vector3(0, -1, 0), Quaternion.identity);
        //obj.transform.parent = this.transform;



        GameObject obj = Instantiate(quad, new Vector3(0, 10, 30), Quaternion.Euler(0, 0, 180));
        obj.transform.parent = this.transform;

        obj = Instantiate(quad, new Vector3(-100, 10, 30), Quaternion.Euler(0, 0, 180));
        obj.transform.parent = this.transform;
    }
    
    public void Reset()
    {

        foreach (Transform obj in this.transform)
        {
            Destroy(obj.gameObject);
        }
        
        GameObject start_obj = Instantiate(emptyQuad, new Vector3(0, -1, 0), Quaternion.identity);
        start_obj.transform.parent = this.transform;
    }

    public void SpawnQuad (int x)
    {/*
        GameObject obj = Instantiate(emptyQuad, new Vector3(x - 100, -1, 0), Quaternion.identity);
        obj.transform.parent = this.transform;

        obj = Instantiate(quad, new Vector3(x - 100, 10, 20), Quaternion.Euler(0,0,180));
        obj.transform.parent = this.transform;


        obj = Instantiate(quad, new Vector3(x - 150, 10, 20), Quaternion.Euler(0, 0, 180));
        obj.transform.parent = this.transform;
        */
    }
	
	// Update is called once per frame
	void Update () {
        // transform.position = transform.position + new Vector3(1, 0, 0) * speed * Time.deltaTime;
    }
}
