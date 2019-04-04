﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

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
        float cooldown = 0;
        public GameObject particle;
        public Image imageC;

        private float r;
        private float g;
        private float b;
        public float a;
        // Use this for initialization
        void Start()
        {

            actions.Add("Fire", Fire);
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
        
            if (cooldown >= 0)
            {
                a += 0.05f;
                cooldown--;
                //AdjustColor();
            }
        }

        private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
        {
            Debug.Log(speech.text);
            actions[speech.text].Invoke();
        }


        private void Fire()
        {
          //  if (cooldown == 0)
          //  {
               // transform.Translate(0, 0, 0);
                trigger = true;
                strike = true;
                cooldown = 5;
                a = 0;
               // imageC.color = new Color(r, g, b, a);
            //}
            // Instantiate(light, resultingPosition, transform.rotation * Quaternion.Euler(0, 0, 90));
            //light.transform.rotation.y += 90;
        }

        void Kill()
        {
            
            Destroy(temp);
            Destroy(particle);

        }

        void AdjustColor()
        {
            Color c = new Color(r, g, b, a);
            imageC.color = c;

        }

    }

}