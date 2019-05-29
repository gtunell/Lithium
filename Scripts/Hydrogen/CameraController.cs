using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject atom;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - atom.transform.position;
    }

    void LateUpdate() // update the camera position
    {
        Vector3 cur_pos = atom.transform.position;
        Vector3 new_pos = cur_pos + offset;
        new_pos.y = transform.position.y;

        transform.position = new_pos;
    }
}