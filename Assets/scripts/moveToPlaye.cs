using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to move the ogres towards the players current position, The script is constantly  updated so 
//the enemies will always move towards the player 
public class moveToPlaye : MonoBehaviour {

    public GameObject gameObject;
    public Animator anim;
    public bool movement = true;
	//Use this for initialization
	void Start ()
    {
		
	}
	
	//Update is called once per frame
	void Update () {
        if (movement == true)
        {
          transform.position = Vector3.MoveTowards(transform.position, gameObject.transform.position, 0.3f);
        }
    }
}
