using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

/* This class handles the players third form of attack which is using voice
 * command "Bolt" to strike a lightning bolt down on their enemies */

namespace HoloToolkit.Unity.InputModule
{

    public class voiceStrike : MonoBehaviour
    {
        
        private KeywordRecognizer keywordRecognizer;
        //creates new dictionary and adds string to it
        private Dictionary<string, Action> actions = new Dictionary<string, Action>();
        public Transform cameraTransform;
        bool strike = false;
        bool spawn = false;
        public GameObject light;
        public GameObject target;
        public float distanceFromCamera = 10;
        bool trigger = false;
        public AudioClip lightA;
        AudioSource audioS;
        public GameObject hammer;
        GameObject temp;
        float timer = 0;
        float cooldown = 0;
        public GameObject particle;
        public Image imageC;

        private float r;
        private float g;
        private float b;
        public float a;
        private float speed = 26;
        Color tempColor;
        // Use this for initialization
        void Start()
        {
            //creates new action and assigns string "Bolt" and connects it to the function Fire
            actions.Add("Bolt", Fire);
            //adds the action to the dictionary
            keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
            keywordRecognizer.Start();
            audioS = GetComponent<AudioSource>();

            r = imageC.color.r;
            g = imageC.color.g;
            b = imageC.color.b;
            a = imageC.color.a;
        }

        // Update is called once per frame
        void Update()
        {
            AdjustColor();
            //updates the resulting position with every update and lets it equal to the lights positison
            Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
            light.transform.position = new Vector3(resultingPosition.x, resultingPosition.y, resultingPosition.z);

            /*this trigger to instantiate the lightning bolt relies on raycasting,
             * it projects forward from where the player is looking in the room and if they
             are currently looking at an enemy when they say bolt than the action will be triggered*/
            if(trigger == true)
            {
                RaycastHit[] hits;
                hits = Physics.RaycastAll(cameraTransform.position, cameraTransform.forward, 100.0F);
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit hit = hits[i];
                   
                    if (hit.collider != null && hit.collider.tag == "Cube")
                    {
                        if (strike == true)
                        {
                            temp = Instantiate(light, hit.point, transform.rotation * Quaternion.Euler(0, 0, 90));
                            Instantiate(particle, hit.point, transform.rotation);
                            strike = false;
                            Destroy(hit.collider.gameObject);
                            audioS.Play();
                        }
                    } 
                }
                timer += Time.deltaTime;
                if (timer >= 2)
                {
                    Kill();
                    timer = 0;
                    trigger = false;
                }
            }
        
            //increases the alpha on the indicator and indicates when it possible to use again
            if (cooldown > 0)
            {
                a += 0.005f;
                cooldown -= Time.deltaTime;
            }
            //when the cooldown has reached zero the indicator will start spinning to show it can be used again
            if (cooldown <= 0)
            {
                speed = 26;
                imageC.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * speed);
            }    
        }

        //listens for the correct voice command
        private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
        {
            Debug.Log(speech.text);
            actions[speech.text].Invoke();
        }

        //this is the function being called when the user says "Fire"
        private void Fire()
        {
            if (cooldown <= 0)
            {
                trigger = true;
                strike = true;
                cooldown = 10;
                speed = 0f;
                a = 0f;
                Debug.Log(a);
           }
        }

        //destroys the prefab in temp and destroys the particle
        void Kill()
        {   
            Destroy(temp);
            Destroy(particle);
        }

        //function for setting the indicators position
        void AdjustColor()
        {
            Color c = new Color(r, g, b, a);
            imageC.color = c; 
        }
    }
}