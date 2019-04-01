using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCL : MonoBehaviour {
    GameObject other;
    public GameObject particle;
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        var x = Time.deltaTime * 2.5f;
        transform.Translate(-x, 0,0 );
      //  Debug.Log(x);
    }


    public void OnCollisionEnter(Collision collision)
    {
        Instantiate(particle, transform.position, transform.rotation);
        Destroy(collision.gameObject);
    }
}
