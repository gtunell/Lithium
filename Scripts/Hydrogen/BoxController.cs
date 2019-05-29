using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

    GameObject box, emptybox;
    public GameObject atom;
    string[] layouts = { "*****  ", "****  *", "***  **", "**  ***", "*  ****", "***    ", "    ***", "****   ", "   ****"};
    int current_boxes;
    GameObject[] boxes_to_destroy, placeholder0, placeholder1;

    Queue<int> holeLocations;

    // Use this for initialization
    void Start () {
        box = Resources.Load("Prefabs/Box") as GameObject;
        emptybox = Resources.Load("Prefabs/EmptyBox") as GameObject;
        boxes_to_destroy = new GameObject[7];
        placeholder0 = new GameObject[7];
        placeholder1 = new GameObject[7];
        holeLocations = new Queue<int>();
        InvokeRepeating("MakeBarrier", 0.0f, 3.0f); 
	}

    public int Peek()
    {
        return holeLocations.Peek();
    }

    public int Dequeue() {
        return holeLocations.Dequeue();
    }

    public void StartSpawn()
    {
        for (int i = 0; i < 7; i++)
        {
            Destroy(boxes_to_destroy[i]);
            Destroy(placeholder0[i]);
            Destroy(placeholder1[i]);
        }

        boxes_to_destroy = new GameObject[7];
        placeholder0 = new GameObject[7];
        placeholder1 = new GameObject[7];
        holeLocations = new Queue<int>();
        InvokeRepeating("MakeBarrier", 0.0f, 3.0f);
    }

    public void StopSpawn()
    {
        CancelInvoke();
    }

    void MakeBarrier() {
        
        for (int i = 0; i < 7; i++) {
            Destroy(boxes_to_destroy[i]);
            boxes_to_destroy[i] = placeholder0[i];
            placeholder0[i] = placeholder1[i];
        }
        

        float rand = Random.value * 8;
        int index = (int)Mathf.Round(rand);
        current_boxes = index;
        holeLocations.Enqueue(index);
        float height = 1;

        for (int i = 0; i < 7; i++)
        {
            if (layouts[current_boxes][i] == '*')
            {
                placeholder1[i] = Instantiate(box, new Vector3(atom.transform.position.x + 20, height, 0), Quaternion.identity);

            }
            else
            {
                placeholder1[i] = Instantiate(emptybox, new Vector3(atom.transform.position.x + 20, height, 0), Quaternion.identity);
            }

            height = height + 1.5f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
