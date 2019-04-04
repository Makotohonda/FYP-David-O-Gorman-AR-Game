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
        public GameObject light;
        public Vector3 resultPos;
        //public float offset;

        void Start()
        {
            audioS = GetComponent<AudioSource>();
            audioS.Play();
        }

        public bool isAtMaxDistance()
        {
            return goBack;
        }
        public Vector3 getPosition()
        {
            return resultPos;
        }
        // Update is called once per frame
        void Update()
        {

            Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
            hammer.transform.position = new Vector3(resultingPosition.x, resultingPosition.y, resultingPosition.z);
            resultPos = resultingPosition;
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
                    distanceFromCamera += 1f;
                }

                if (distanceFromCamera >= 10)
                {
                    goBack = true;
                    //Instantiate(light, resultingPosition, transform.rotation * Quaternion.Euler(0, 0, 90));
                }

                if ((distanceFromCamera > -1) && goBack == true)
                {
                    distanceFromCamera -= 1f;
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
