using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{


    public class TapHammer : MonoBehaviour, IInputClickHandler, IInputHandler
    {

        AudioSource audioS;
        public AudioClip woosh;
        public GameObject hammer;
        public GameObject cursor;
        public float speed = 0.5f;
        bool swing = false;
        bool returnTo = false;
        private float distance = 200f;
        public float orbitDegreesPerSec = 180.0f;
        float timer = 0;
        Vector3 initial_pos;

        void Start()
        {
           // manager = GameObject.FindWithTag("GameManager");
            audioS = GetComponent<AudioSource>();
            audioS.Play();
            initial_pos = hammer.transform.position;
        }

        void Update()
        {
            Vector3 target = Camera.main.transform.forward;
            Vector3 rotation = new Vector3(200, 0, 0);
            Vector3 rotationC = Camera.main.transform.rotation.eulerAngles;

          
            if (swing == true)
            {
                timer += 1/60f;
                hammer.transform.position = Vector3.MoveTowards(hammer.transform.position, target * distance, speed * Time.deltaTime);
                //GetComponent<Animator>().SetTrigger("Swing");
                rotationC.y += 10;
                hammer.transform.RotateAround(hammer.transform.position, rotationC, orbitDegreesPerSec * Time.deltaTime * 5);

                if (timer > 3)
                {
                    swing = false;
                    returnTo = true;
                    timer = 0;
                }
            }


            if (returnTo == true)
            {
                Vector3 returnPos = hammer.transform.position - initial_pos;
                hammer.transform.position = Vector3.MoveTowards(hammer.transform.position, initial_pos, speed * Time.deltaTime);
                rotationC.y += 10;
                hammer.transform.RotateAround(hammer.transform.position, rotationC, orbitDegreesPerSec * Time.deltaTime * 5);

                if (hammer.transform.position == initial_pos)
                {
                    returnTo = false;
                }
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                swing = true;
                hammer.transform.eulerAngles = new Vector3(0, 0, 0);
              
               // hammer.transform.rotate = Vector3.MoveTowards(hammer.transform.rotate, target * distance, speed / 2);
            }

            if (Input.touchCount > 1)
            {
                swing = true;
            }
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            swing = true;

        }

        public void OnInputDown(InputEventData eventData)
        {
            //    AudioSource.PlayClipAtPoint(boom, this.transform.position);

            //  Instantiate(particle, transform.position, transform.rotation);
            swing = true;
            //  this.hammer.transform.localScale = new Vector3(this.gameObject.transform.translate.x * 2, 0, 0);
            audioS.Play();
            Debug.Log(swing);
            Destroy(audioS);
            eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.
        }

        public void OnInputUp(InputEventData eventData)
        {
            swing = true;
          //  Vector3 target = Camera.main.transform.forward;
           // hammer.transform.position = Vector3.MoveTowards(hammer.transform.position, target * distance, speed / 2);
        }
    }
}
