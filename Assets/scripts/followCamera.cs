using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace HoloToolkit.Unity.InputModule
{

    public class followCamera : MonoBehaviour, IInputClickHandler
    {

        
        // Use this for initialization

        public Transform cameraTransform;
        public float distanceFromCamera;
        bool launch = false;
        bool goBack = false;
        bool trigger = false;
        public GameObject hammer;
        AudioSource audioS;
        public AudioClip whoosh;
        //public float offset;

        void Start()
        {
            audioS = GetComponent<AudioSource>();
            audioS.Play();
        }


        // Update is called once per frame
        void Update()
        {

            Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
            hammer.transform.position = new Vector3(resultingPosition.x, resultingPosition.y, resultingPosition.z);

           // if (Input.GetKeyDown("space"))
           if(trigger == true)
            {
                launch = true;
                trigger = false;
            }
          //  Debug.Log(trigger);
            if (launch)
            {
                if ((distanceFromCamera < 10) && goBack == false)
                {
                    distanceFromCamera += 0.2f;
                }

                if (distanceFromCamera >= 10)
                {
                    goBack = true;
                }

                if ((distanceFromCamera > -1) && goBack == true)
                {
                    distanceFromCamera -= 0.2f;
                }

                if (goBack == true && distanceFromCamera <= -1)
                {
                    goBack = false;
                    launch = false;
                    distanceFromCamera = -1;
                }
            }
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            audioS.Play();
            trigger = true;
            eventData.Use();
        }

    }
}
