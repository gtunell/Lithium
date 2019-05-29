using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliumController : MonoBehaviour {

    Vector3 cur_pos;
    public float horizontalSpeed = 0, verticalSpeed = 0, maxSpeed = 4;
    public float hellicopterHitVerticalSpeed = -200;
    public float hellicopterHitHorizontalSpeed = 0.5f;
    public float rotate = 0;

    float timeElapsedEmptyTile;
    float timeElapsedEmptyQuad;
    bool secondsClearedEmptyTile;
    bool secondsClearedEmptyQuad;
    bool jumping;
    bool hellicopterHit;
    Rigidbody rb;

    Material neutronMaterial;
    Material protonMaterial;
    Material electronMaterial;
    // Material burntMaterial;
    public GameObject neutron;
    public GameObject proton;
    public GameObject electron;
    MeshRenderer rend1;
    MeshRenderer rend2;
    MeshRenderer rend3;

    HeliumGameController gameController;
    TileController tileController;
    QuadControllers quadControllers;
    
    float blockedTime;

    // Use this for initialization
    void Start() {
        timeElapsedEmptyTile = 0;
        timeElapsedEmptyQuad = 0;
        secondsClearedEmptyTile = true;
        secondsClearedEmptyQuad = true;
        jumping = false;
        hellicopterHit = false;
        rb = GetComponent<Rigidbody>();


        neutronMaterial = Resources.Load("Materials/neutronColor", typeof(Material)) as Material;
        protonMaterial = Resources.Load("Materials/protonColor", typeof(Material)) as Material;
        electronMaterial = Resources.Load("Materials/electronColor", typeof(Material)) as Material;
        // burntMaterial = Resources.Load("Materials/burnt", typeof(Material)) as Material;


        if (neutron && proton && electron)
        {
            rend1 = neutron.GetComponent<MeshRenderer>();
            rend2 = proton.GetComponent<MeshRenderer>();
            rend3 = electron.GetComponent<MeshRenderer>();
        }

        gameController = GameObject.Find("HeliumGameController").GetComponent<HeliumGameController>();
        // tileController = GameObject.Find("TileController").GetComponent<TileController>();
        // quadControllers = GameObject.Find("QuadControllers").GetComponent<QuadControllers>();


    }

    public void Restart()
    {

        hellicopterHit = false;
        timeElapsedEmptyTile = 0;
        timeElapsedEmptyQuad = 0;
        secondsClearedEmptyTile = true;
        secondsClearedEmptyQuad = true;

        // horizontalSpeed = -maxSpeed;
        horizontalSpeed = 0;
        verticalSpeed = 10;
        Jump();
        this.transform.position = new Vector3(0, 4, 0);
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        StopJump();

    }

    public void Jump()
    {
        jumping = true;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void StopJump()
    {
        jumping = false;
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    public void UnBurnt()
    {
        rend1.material = neutronMaterial;
        rend2.material = protonMaterial;
        rend3.material = electronMaterial;
    }

    /*
    public void Burnt()
    {
        rend1.material = burntMaterial;
        rend2.material = burntMaterial;
        rend3.material = burntMaterial;
        horizontalSpeed = verticalSpeed = burntSpeed;
    }
    */

    float HoleMaxHeight(int current_boxes)
    {

         // string[] layouts = { "*****  ", "****  *", "***  **", "**  ***", "*  ****", "***    ", "    ***", "****   ", "   ****" };
        if (current_boxes == 1)
        {
            return 9.25f;
        }
        else if (current_boxes == 2)
        {
            return 7.75f;
        }
        else if (current_boxes == 3 || current_boxes == 6)
        {
            return 6.25f;
        }
        else if (current_boxes == 4 || current_boxes == 8)
        {
            return 4.75f;
        }
        else
        {
            return 11;
        }
        
    }

    float HoleMinHeight(int current_boxes)
    {

         // string[] layouts = { "*****  ", "****  *", "***  **", "**  ***", "*  ****", "***    ", "    ***", "****   ", "   ****" };
        
        if (current_boxes == 0)
        {
            return 7.75f;
        }
        else if (current_boxes == 1 || current_boxes == 7)
        {
            return 6.25f;
        }
        else if (current_boxes == 2 || current_boxes == 5)
        {
            return 4.75f;
        } 
        else if (current_boxes == 3)
        {
            return 3.25f;
        }
        else if (current_boxes == 4)
        {
            return 1.75f;
        }
        else
        {
            return 0;
        }
        
    }

    IEnumerator DequeMe(Queue<int> q)
    {
        q.Dequeue();
        yield return new WaitForSeconds(1f);
    }


    // Update is called once per frame
    void Update ()
    {

        if (!secondsClearedEmptyTile && timeElapsedEmptyTile > 1)
        {
            secondsClearedEmptyTile = true;
            timeElapsedEmptyTile = 0f;
        }
        else
        {
            timeElapsedEmptyTile += Time.deltaTime;
        }

        if (!secondsClearedEmptyQuad && timeElapsedEmptyQuad > 2)
        {
            secondsClearedEmptyQuad = true;
            timeElapsedEmptyQuad = 0f;
        }
        else
        {
            timeElapsedEmptyQuad += Time.deltaTime;
        }

        if (hellicopterHit)
        {
            transform.position = transform.position + new Vector3(1, 0, 0) * hellicopterHitHorizontalSpeed * Time.deltaTime
                                                    + new Vector3(0, 1, 0) * hellicopterHitVerticalSpeed * Time.deltaTime;
            hellicopterHitVerticalSpeed += Time.deltaTime * 5;
        }
        else if (jumping == true)
        {
            transform.position = transform.position + new Vector3(1, 0, 0) * horizontalSpeed * Time.deltaTime
                                                    + new Vector3(0, 1, 0) * verticalSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position + new Vector3(1, 0, 0) * horizontalSpeed * Time.deltaTime;
        }

        if (transform.position.y > 11 || transform.position.y < 0)
        {
            gameController.SignalLoss();
        }

        transform.Rotate(Vector3.right * Time.deltaTime * rotate);

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EmptyTile" && secondsClearedEmptyTile)
        {
            secondsClearedEmptyTile = false;
            //tileController.SpawnTile((int) other.transform.position.x);
        }

        if (other.tag == "EmptyQuad" && secondsClearedEmptyQuad)
        {
            secondsClearedEmptyQuad = false;
            quadControllers.SpawnQuad((int) other.transform.position.x);
        }

        if (other.gameObject.tag == "Hellicopter")
        {
            hellicopterHit = true;
            this.Jump();
            gameController.SignalHellicopter();
            other.gameObject.GetComponent<HellicopterController>().StopCollider();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        
    }
}
