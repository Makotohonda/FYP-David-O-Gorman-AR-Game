using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class is used to detect collision between the player and the the enemeies
//If collison between enemy and player than the player will lose 10 health with each hit
//audio to indicate player was hit plays

namespace HoloToolkit.Unity.InputModule
{

    public class PlayerCubeCollision : MonoBehaviour
    {
        GameObject manager;
        AudioSource audioS;
        // Use this for initialization
        public AudioClip damage;

        void Start()
        {
            manager = GameObject.FindWithTag("GameManager");
        }

        // Update is called once per frame
        void Update()
        {

        }

        //checks collision of regular enemies and player and decreases the players health by 10
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Cube")
            {
                manager.GetComponent<GameManager>().DecreaseHealth(10);
                //PlayClip(damge);
                //audioS.Play();
            }
        }
    }
}
