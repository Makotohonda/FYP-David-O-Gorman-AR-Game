using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//swarm effect was the original form before switching to a more direct and wave like gameplay feel
//It was also more ideal to have the enemies to constantly move for the player
public class swarm : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var z = Time.deltaTime * 2.5f;
        transform.Translate(0, 0, -z);
        Debug.Log(z);
    }
}
