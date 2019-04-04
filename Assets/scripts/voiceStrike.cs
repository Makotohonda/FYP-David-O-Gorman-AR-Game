using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace HoloToolkit.Unity.InputModule
{

    public class voiceStrike : MonoBehaviour
    {

        private KeywordRecognizer keywordRecognizer;
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
        public GameObject particle;
        
        // Use this for initialization
        void Start()
        {

            actions.Add("Fire", Fire);
            keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
            keywordRecognizer.Start();
            audioS = GetComponent<AudioSource>();
        }


        // Update is called once per frame
        void Update()
        {

            Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
            light.transform.position = new Vector3(resultingPosition.x, resultingPosition.y, resultingPosition.z);



            if (Input.GetKeyDown("space"))
            {
                trigger = true;
            }
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
                
                // Instantiate( light, resultingPosition, transform.rotation * Quaternion.Euler(0, 0, 90));
                //  Fire();
               // audioS.clip = lightA;
              //  audioS.Play();
               
                // lightA.Play();
            }
           
        }

        private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
        {
            Debug.Log(speech.text);
            actions[speech.text].Invoke();
        }


        private void Fire()
        {

            transform.Translate(0, 0, 0);
            trigger = true;
            strike = true;
           
            // Instantiate(light, resultingPosition, transform.rotation * Quaternion.Euler(0, 0, 90));
            //light.transform.rotation.y += 90;
        }

        void Kill()
        {
            
            Destroy(temp);
            Destroy(particle);

        }
       
    }

}