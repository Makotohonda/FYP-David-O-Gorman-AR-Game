using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToPlaye : MonoBehaviour {

    public GameObject gameObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, gameObject.transform.position, 0.3f);
    }
}
