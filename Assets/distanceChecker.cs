using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceChecker : MonoBehaviour {

    Animator animator;
    public GameObject player;
	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindWithTag("PlayerBox");
    }
	
	// Update is called once per frame
	void Update () {

        float dist = Vector3.Distance(player.transform.position, transform.position);
       // Debug.Log("Distance" + dist);
        animator.SetFloat("DistanceFromPlayer",dist);
	}
}
