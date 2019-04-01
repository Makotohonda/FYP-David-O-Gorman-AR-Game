using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HoloToolkit.Unity.InputModule
{

    public class PlayerCubeCollision : MonoBehaviour
    {
        GameObject manager;
        // Use this for initialization
        void Start()
        {
            manager = GameObject.FindWithTag("GameManager");
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Cube")
            {
                manager.GetComponent<GameManager>().DecreaseHealth(10);
            }

        }
    }
}
