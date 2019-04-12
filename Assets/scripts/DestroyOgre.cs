using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to destroy the main enemies after a set amount of time
//The script will delete the object after three seconds, enough time to play the animation

namespace HoloToolkit.Unity.InputModule
{
    public class DestroyOgre : MonoBehaviour
    {

        bool startTime = false;
        float count = 0;
        GameObject manager;

        public void ActivateTimer()
        {
            startTime = true;
        }
        // Use this for initialization
        void Start()
        {
            manager = GameObject.FindWithTag("GameManager");
        }

        // Update is called once per frame
        void Update()
        {

            //sets the deletion of the ogre onto a timer so the death animation can play out 
            //once animation is complete deletes the ogre
            if (startTime)
            {
                //timer
                count += Time.deltaTime;

                if (count >= 3)
                {
                    manager.GetComponent<GameManager>().IncreaseDeadCount();
                    Destroy(gameObject);
                    count = 0;

                }
            }
        }
    }
}