using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This class is used to send the players main weapon forward
 * the hammer constantly tracks the players position
 * fires forward until reaching max distance and the comes back
 * Also handles collision with each necessary enemy */

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

        //returns the goBack bool
        public bool isAtMaxDistance()
        {
            return goBack;
        }

        //returns the resulting position for the hammer
        public Vector3 getPosition()
        {
            return resultPos;
        }
        // Update is called once per frame
        void Update()
        {
            // Takes the direction that the player is facing and sets the hammers position to the resulting position
            //launches the hammer in the direction that th player is facing
            Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
            hammer.transform.position = new Vector3(resultingPosition.x, resultingPosition.y, resultingPosition.z);
            resultPos = resultingPosition;

           if(trigger == true)
            {
                launch = true;
                trigger = false;
            }

            if (launch)
            {
                //sends the hammer forward until reaching the limit
                if ((distanceFromCamera < 10) && goBack == false)
                {
                    distanceFromCamera += 1f;
                }
                 //checks for limit reached
                if (distanceFromCamera >= 10)
                {
                    goBack = true;
                }

                //sends the hammer back to the initial position
                if ((distanceFromCamera > -1) && goBack == true)
                {
                    distanceFromCamera -= 1f;
                }

                //resets all commands
                if (goBack == true && distanceFromCamera <= -1)
                {
                    goBack = false;
                    launch = false;
                    distanceFromCamera = -1;
                }
            }
        }

        //checks for input from the player
        public void OnInputClicked(InputClickedEventData eventData)
        {
            audioS.Play();
            trigger = true;
            eventData.Use();
        }

    }
}
