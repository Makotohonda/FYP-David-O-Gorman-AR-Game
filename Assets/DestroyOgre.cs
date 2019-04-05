using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOgre : MonoBehaviour {

    bool startTime = false;
    float count = 0;

    public void ActivateTimer()
    {
        startTime = true;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(startTime)
        {
            //timer
            count += Time.deltaTime;

            if (count >= 3)
            {
                Destroy(gameObject);
                count = 0;
                
            }
        }
	}
}
