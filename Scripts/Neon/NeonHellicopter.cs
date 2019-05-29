using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonHellicopter : MonoBehaviour {

    GameObject blade1, blade2;

    public float speed = 5;
    float frequency;
    Vector3 originalPos;
    
    float time;
    
	// Use this for initialization
	void Start () {
        time = 0;

        blade1 = this.transform.GetChild(0).gameObject;
        blade2 = this.transform.GetChild(1).gameObject;

        originalPos = transform.position;
        frequency = (Random.value * 0.25f) + 5;
        
    }

    public void StopCollider()
    {
        this.gameObject.GetComponent<Collider>().enabled = !this.gameObject.GetComponent<Collider>().enabled;
    }

    // Update is called once per frame
    void Update () {
        blade1.transform.Rotate(Vector3.up * Time.deltaTime * 500);
        blade2.transform.Rotate(Vector3.up * Time.deltaTime * 500);

        time += Time.deltaTime;

        if (time > 8)
        {

            Destroy(gameObject);
        }

        Vector3 pos = transform.position;
        float height = 1 * Mathf.Cos(Time.time * frequency);
        transform.position = new Vector3(pos.x, originalPos.y + height + 1, pos.z - Time.deltaTime * 15);

        if (this.transform.position.z < -90)
            Destroy(gameObject);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Game Trigger")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Energy Flare")
        {
            Destroy(gameObject);
        }
    }

}
