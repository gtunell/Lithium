using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour {
    
    GameObject tile;
    GameObject[] tiles;
    GameObject tile1, tile2, tile3, tile4;

    float speed;

	// Use this for initialization
	void Start () {
        tile = Resources.Load("Prefabs/Tile") as GameObject;
        // emptyTile = Resources.Load("Prefabs/EmptyTile") as GameObject;
        // GameObject obj = Instantiate(emptyTile, new Vector3(-8, -1, 0), Quaternion.identity);
        // obj.transform.parent = this.transform;
        tiles = new GameObject[9];

        int x = 0;
        for (int i = 0; i < 9; i++)
        {
            tiles[i] = Instantiate(tile, new Vector3(x, -1, 0), Quaternion.identity);
            x -= 4;
        }
        
        speed = 6;

    }

	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tile")
        {
            other.gameObject.transform.position = new Vector3(-20, -1, 0);
            float rand;
            rand = Random.value * 2;

            if (rand > 0.8)
            {
                other.gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
                other.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().enabled = true;
                other.gameObject.transform.GetChild(1).GetComponent<Renderer>().enabled = true;

                other.gameObject.transform.GetComponent<Collider>().enabled = true;
                other.gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = true;
                other.gameObject.transform.GetChild(1).GetComponent<Collider>().enabled = true;
            }
            else
            {
                other.gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                other.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                other.gameObject.transform.GetChild(1).GetComponent<Renderer>().enabled = false;

               // other.gameObject.transform.GetComponent<Collider>().enabled = false;
                other.gameObject.transform.GetChild(0).GetComponent<Collider>().enabled = false;
                other.gameObject.transform.GetChild(1).GetComponent<Collider>().enabled = false;
            }
        }
    }
}
