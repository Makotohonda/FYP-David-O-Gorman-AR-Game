using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToPlaye : MonoBehaviour {

    public GameObject gameObject;
    public Animator anim;
    public bool movement = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (movement == true)
        {
          //  anim.SetFloat("DistanceToPlayer", Vector3.Distance(transform.position, gameObject.transform.position));
            transform.position = Vector3.MoveTowards(transform.position, gameObject.transform.position, 0.3f);
        }
    }
}
