﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to make the Hammer constantly rotate as it is thrown
public class rotateHammer : MonoBehaviour {

    int speed = 1000;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate( Vector3.right, speed * Time.deltaTime);
    }
}
