using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LithiumControllers1 : MonoBehaviour {

    public GameObject atom;
    GameObject lithium;

	// Use this for initialization
	void Start () {
		lithium = Resources.Load("Prefabs/Lithium1", typeof(GameObject)) as GameObject;
        InvokeRepeating("SpawnLithium", 1.0f, 2f);
    }

    public void StartSpawn()
    {
        InvokeRepeating("SpawnLithium", 1.0f, 2f);
    }

    public void StopSpawn()
    {
        CancelInvoke();
    }

    void SpawnLithium()
    {
        double height = (Random.value * 8) + 1f;
        Instantiate(lithium, new Vector3(atom.transform.position.x - 15, (float) height, 0), Quaternion.identity);
    }
    

    // Update is called once per frame
    void Update () {
		
	}
}
