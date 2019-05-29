using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellicopterControllers : MonoBehaviour
{

    public GameObject atom;
    GameObject hellicopter;
    

    // Use this for initialization
    void Start()
    {
        hellicopter = Resources.Load("Prefabs/Hellicopter", typeof(GameObject)) as GameObject;
        InvokeRepeating("SpawnHellicopter", 2.0f, 2f);
    }

    public void StartSpawn()
    {
        InvokeRepeating("SpawnHellicopter", 2.0f, 2f);
    }

    public void StopSpawn()
    {
        CancelInvoke();
    }

    void SpawnHellicopter()
    {
        double height = (Random.value * 4) + 4;
        Instantiate(hellicopter, new Vector3(atom.transform.position.x - 25, (float) height, 0), Quaternion.Euler(180, 180, 0));
    }


    // Update is called once per frame
    void Update()
    {
    }
}
