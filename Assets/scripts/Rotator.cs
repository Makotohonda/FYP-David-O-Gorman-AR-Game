﻿using UnityEngine;

using System.Collections;



/// <summary>

/// </summary>



public class Rotator : MonoBehaviour
{



    public float speed;



    void Update()

    {

        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * speed);

    }

}