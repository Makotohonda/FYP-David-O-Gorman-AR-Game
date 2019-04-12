using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
    //This class is used to delete the Boss after the animation has played.
    
    public class DestroyBigB : MonoBehaviour
    {
        bool startTime = false;
        float count = 0;
        GameObject manager;

        //sets time to true
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
            if (startTime)
            {
                //timer counts up by seconds
                count += Time.deltaTime;

                //if  the count is bigger than three, check to see what the ogres health is, if zero on, then destroy object
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