using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is used to check the distance of the Boss to the playes location
public class BBDistance : MonoBehaviour {

    Animator animator;
    public GameObject player;

	//Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindWithTag("PlayerBox");
    }
	
	//Update is called once per frame
	void Update () {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        animator.SetFloat("DistanceFromPlayer", dist);
    }
}
