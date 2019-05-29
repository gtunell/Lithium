﻿using System.Collections;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;

    void Start ()
    {
        InvokeRepeating("Rotate", 0f, 0.15f);
    }
    void Rotate()
    {
        this.transform.localEulerAngles += new Vector3(x, y, z);
    }
    void OnDisable()
    {
        CancelInvoke();
    }
}
