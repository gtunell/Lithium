 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LithiumController1 : MonoBehaviour {

    Rigidbody rb;
    bool pickedup;
    HeliumGameController gameController;
    GameObject atom;
    float time;

    bool scaling;
    
    // Use this for initialization
    void Start ()
    {
        scaling = false;

        gameController = GameObject.Find("HeliumGameController").GetComponent<HeliumGameController>();
        atom = GameObject.Find("Helium");
        rb = GetComponent<Rigidbody>();
        time = 0;
        
        StartCoroutine(ScaleUp(1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!scaling)
            transform.position = transform.position + new Vector3(6 * Time.deltaTime, 0, 0);

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
        scaling = true;

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

        gameController.Score();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameController.gameover && other.tag == "Helium")
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
