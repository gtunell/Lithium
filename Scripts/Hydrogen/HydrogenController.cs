using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydrogenController : MonoBehaviour {

    Vector3 cur_pos;
    public float horizontalSpeed = 0, verticalSpeed = 0, maxSpeed = 4;
    public float burntSpeed = 0;
    public float rotate = 0;

    bool jumping;
    Rigidbody rb;

    Material neutronMaterial;
    Material protonMaterial;
    Material electronMaterial;
    Material burntMaterial;
    public GameObject neutron;
    public GameObject proton;
    public GameObject electron;
    MeshRenderer rend1;
    MeshRenderer rend2;
    MeshRenderer rend3;

    HydrogenGameController gameController;
    FireballController fireballController;
    BoxController boxController;

    bool tooSlow;
    bool gameover;
    bool blocked;
    bool gap_found;
    float start;
    float blockedTime;

    // Use this for initialization
    void Start() {
        tooSlow = false;
        gameover = false;
        blocked = false;
        gap_found = false;
        start = 0;

        jumping = false;
        rb = GetComponent<Rigidbody>();


        neutronMaterial = Resources.Load("Materials/neutronColor", typeof(Material)) as Material;
        protonMaterial = Resources.Load("Materials/protonColor", typeof(Material)) as Material;
        electronMaterial = Resources.Load("Materials/electronColor", typeof(Material)) as Material;
        burntMaterial = Resources.Load("Materials/burnt", typeof(Material)) as Material;


        if (neutron && proton && electron)
        {
            rend1 = neutron.GetComponent<MeshRenderer>();
            rend2 = proton.GetComponent<MeshRenderer>();
            rend3 = electron.GetComponent<MeshRenderer>();
        }

        gameController = GameObject.Find("HydrogenGameController").GetComponent<HydrogenGameController>();
        fireballController = GameObject.Find("FireballController").GetComponent<FireballController>();
        boxController = GameObject.Find("BoxController").GetComponent<BoxController>();
    }

    public void Restart() {
        gameover = false;
        blocked = false;
        gap_found = false;
        start = 0;
        UnBurnt();

        horizontalSpeed = verticalSpeed = maxSpeed;
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

    public void Burnt()
    {
        rend1.material = burntMaterial;
        rend2.material = burntMaterial;
        rend3.material = burntMaterial;
        gameover = true;
        horizontalSpeed = verticalSpeed = burntSpeed;
    }

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
    void Update () {

        if (!gameover)
        {
            if (blocked)
            {
                int curr_boxes = boxController.Peek();
                //int curr_boxes = boxController.HoleLocation();
                float max = HoleMaxHeight(curr_boxes);
                float min = HoleMinHeight(curr_boxes);

                if (gap_found == false && transform.position.y + 1.2f < max && transform.position.y - 1.2f > min)
                {
                    gap_found = true;
                    horizontalSpeed = maxSpeed;
                    if (jumping && transform.position.y + 1.2 > max)
                    {
                        this.StopJump();
                        verticalSpeed = 0;
                    }
                    else if (transform.position.y - 1.2 < min)
                    {
                        this.Jump();
                        verticalSpeed = maxSpeed;
                    }
                    else if (!jumping)
                    {
                        this.StopJump();
                        verticalSpeed = 0;
                    }
                    else
                    {
                        this.Jump();
                        verticalSpeed = maxSpeed;
                    }

                    start = this.transform.position.x;

                }/*
            else if (gap_found == true && this.transform.position.x - start > 3.0f)
            {
                StartCoroutine(DequeMe(holeLocations));
                gap_found = false;
                blocked = false;
                horizontalSpeed = maxSpeed;
                verticalSpeed = maxSpeed;
                this.StopJump();
            }*/
                else if (gap_found == true)
                {
                    gap_found = true;
                    horizontalSpeed = maxSpeed;
                    if (jumping && transform.position.y + 1.2 > max)
                    {
                        this.StopJump();
                        verticalSpeed = 0;
                    }
                    else if (!jumping && transform.position.y - 1.2 < min)
                    {
                        this.Jump();
                        //verticalSpeed = 0;
                    }
                    else if (!jumping)
                    {
                        verticalSpeed = 0;
                    }
                    else
                    {
                        this.Jump();
                        verticalSpeed = maxSpeed;
                    }

                    if (this.transform.position.x - start > 3.5f)
                    {
                        if (jumping && !Input.GetKey(KeyCode.Space)) StopJump();
                        gap_found = false;
                        blocked = false;
                        horizontalSpeed = maxSpeed;
                        verticalSpeed = maxSpeed;
                        boxController.Dequeue();
                    }
                }
                else
                {
                    gap_found = false;
                    blocked = true;
                    horizontalSpeed = 0;
                    verticalSpeed = maxSpeed;

                    blockedTime += Time.deltaTime;
                    if (blockedTime > 2)
                    {
                        tooSlow = true;
                        blockedTime = -1000;
                        fireballController.StopSpawn();
                        fireballController.KillShot();
                    }
                }
            }

            if (!gameover && transform.position.y > 10)
            {
                gameController.SignalFire();
            }

            if (jumping == true)
            {
                transform.position = transform.position + new Vector3(1, 0, 0) * horizontalSpeed * Time.deltaTime
                                                        + new Vector3(0, 1, 0) * verticalSpeed * Time.deltaTime;
            }
            else
            {
                transform.position = transform.position + new Vector3(1, 0, 0) * horizontalSpeed * Time.deltaTime;
            }

            transform.Rotate(Vector3.right * Time.deltaTime * rotate);
        }

        else
        {
            transform.Rotate(Vector3.right * Time.deltaTime * rotate * 0.3f);
        }
            
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box" && gap_found == false && blocked == false) {     
            blocked = true;
            gap_found = false;
            blockedTime = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "FireballCollider")
        {
            StopJump();
            gameover = true;
            gameController.SignalFireball(tooSlow);
            tooSlow = false;
        }
        
    }
}
