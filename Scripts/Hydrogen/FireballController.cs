using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour {
    
    public GameObject atom;
    GameObject fireball;
    GameObject fireballHolder;

    float time;
    bool gameover;
    

	// Use this for initialization
	void Start () {
        time = 0;
        gameover = false;
        
        fireball = Resources.Load("Prefabs/Fireball", typeof(GameObject)) as GameObject;
        fireballHolder = GameObject.Find("FireballHolder");
        InvokeRepeating("ShootFireball", 3.0f, 3.0f);
	}

    public void KillShot() {
        float x = atom.transform.position.x;
        Instantiate(fireball, new Vector3(x + 20f, 1.0f, 0.0f), Quaternion.Euler(0, -90, 0));
        Instantiate(fireball, new Vector3(x + 20f, 3.0f, 0.0f), Quaternion.Euler(0, -90, 0));
        Instantiate(fireball, new Vector3(x + 20f, 5.0f, 0.0f), Quaternion.Euler(0, -90, 0));
        Instantiate(fireball, new Vector3(x + 20f, 7.0f, 0.0f), Quaternion.Euler(0, -90, 0));
        Instantiate(fireball, new Vector3(x + 20f, 9.0f, 0.0f), Quaternion.Euler(0, -90, 0));
        Instantiate(fireball, new Vector3(x + 20f, 11.0f, 0.0f), Quaternion.Euler(0, -90, 0));
    }

    void ShootFireball()
    {
        GameObject new_fireball =
                Instantiate(fireball, atom.transform.position + new Vector3(20.0f, 0.0f, 0.0f), Quaternion.Euler(0,-90,0));

        new_fireball.transform.parent = fireballHolder.transform;

    }

    public void StartSpawn()
    {
        gameover = false;
        InvokeRepeating("ShootFireball", 3.0f, 3.0f);
    }

    public void StopSpawn()
    {
        gameover = true;
        time = 0;

        foreach (Transform t in fireballHolder.transform)
        {
            Destroy(t.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (gameover)
        {
            CancelInvoke();
        }

        if (gameover && time < 0.9f)
        {
            time += Time.deltaTime;
        }
        else if (gameover)
        {
            gameover = false;

            GameObject[] fballs = GameObject.FindGameObjectsWithTag("Fireball");

            foreach (GameObject fball in fballs)
            {
                Destroy(fball);
            }

            time = 0;
        }
        

	}
}
