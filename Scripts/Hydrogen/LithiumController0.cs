using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LithiumController0 : MonoBehaviour {

    Rigidbody rb;
    bool pickedup;
    
    HydrogenGameController hydrogenGameController;

    GameObject atom;
    float time;
    
    // Use this for initialization
    void Start ()
    {

        hydrogenGameController = GameObject.Find("HydrogenGameController").GetComponent<HydrogenGameController>();
        atom = GameObject.Find("Hydrogen");
        rb = GetComponent<Rigidbody>();
        time = 0;
        
        StartCoroutine(ScaleUp(1f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * 300);
        time += Time.deltaTime;
        if (time > 5)
            Destroy(gameObject);
    }

    IEnumerator ScaleUp(float time)
    {
        Vector3 originalScale = transform.localScale * 0.0f;
        Vector3 destinationScale = new Vector3(1.0f, 1.0f, 1.0f);

        float currentTime = 0.0f;

        do
        {
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        } while (currentTime <= time);

        
    }

    IEnumerator ScaleDown(float time)
    {
        Vector3 originalScale = transform.localScale * 1.2f;
        Vector3 destinationScale = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 originalPos = transform.position;

        float currentTime = 0.0f;

        do
        {
            transform.position = Vector3.Lerp(originalPos, atom.transform.position, currentTime / time);
            transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
            currentTime += Time.deltaTime;
            
            yield return null;
        } while (currentTime <= time);

        hydrogenGameController.Score();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hydrogenGameController.gameover && other.tag == "Hydrogen")
        {
            rb.useGravity = false;
            StartCoroutine(ScaleDown(1f));
        }
        else if (other.tag == "Box")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.localScale = new Vector3(1,1,1);
    }

}
