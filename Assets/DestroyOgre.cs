using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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