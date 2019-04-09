using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HoloToolkit.Unity.InputModule
{

    public class BossControl : MonoBehaviour
    {
     
        public GameObject player;
        public GameManager script2;
        float speed = 1f;
        public bool movement = true;
        // Use this for initialization
        void Start()
        {
            player = GameObject.FindWithTag("PlayerBox");
        }

        // Update is called once per frame
        void Update()
        {
            if (movement == true)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
                transform.LookAt(player.transform.position);
            }
        }





    }
}