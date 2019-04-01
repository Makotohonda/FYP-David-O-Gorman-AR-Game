using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

namespace HoloToolkit.Unity.InputModule
{


    public class VoiceHUD : MonoBehaviour
    {


        public string health;
        public int healthI;
        float count = 0;
        public Canvas canv;
        public Text healthtxt;
        public GameManager playerHealthC;

        private KeywordRecognizer keywordRecognizer;
        private Dictionary<string, Action> actions = new Dictionary<string, Action>();
        // Use this for initialization
        void Start()
        {
            actions.Add("Health", caller);

            keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
            keywordRecognizer.Start();
        }

        // Update is called once per frame
        void Update()
        {

            if (healthtxt.enabled == true)
            {
                count = count + Time.deltaTime;
                if (count >= 4)
                {
                    healthtxt.enabled = false;
                    count = 0;
                }

            }
            if (Input.GetKeyDown("space"))
            {
                caller();
            }
        }

        private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
        {
            Debug.Log(speech.text);
            actions[speech.text].Invoke();
        }


        private void caller()
        {
            healthI = playerHealthC.GetHealth();
            string healthString = "Health: " + healthI.ToString();
            healthtxt.enabled = true;
            healthtxt.text = healthString;
            //yield return new WaitForSeconds(5);

            // healthtxt.enabled = false;
        }

        //IEnumerator Health()
        //{
        //    //health = playerHealthC.GetHealth();
        //   // string healthString = "Health: ";
        // //   healthtxt.enabled = true;
        // //   healthtxt.text = healthString;
        //    yield return new WaitForSeconds(1);
        // //   healthtxt.enabled = false;
        //}
    }
}