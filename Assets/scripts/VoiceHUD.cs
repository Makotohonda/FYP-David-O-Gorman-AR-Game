using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

/*This class is used to so that the player can check how much health they have
 * originally it was designed that the health, score and round number were always
 * displayed on the screen, however due to the limitations of the window width and
 * not wanting to limit the players view any further
  *I decided to only give player the necessary information and on command
  * The necessary information being health, and by using voice command "Health"
  * I appears on the top left of the screen*/

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

        //creates Keyword recognizer
        private KeywordRecognizer keywordRecognizer;
        //creates new dictionary of strings named actions
        private Dictionary<string, Action> actions = new Dictionary<string, Action>();
        // Use this for initialization
        void Start()
        {

            //on start creates an actions called "Health" when command is entered will get the Caller function
            actions.Add("Health", caller);

            //adds the command to the dictonary so it knows what to listen for
            keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
            keywordRecognizer.Start();
        }

        // Update is called once per frame
        void Update()
        {
            //what to do when the health text is called to draw
            //timer so that it doesnt permenantly stay on the screen
            if (healthtxt.enabled == true)
            {
                count = count + Time.deltaTime;
                if (count >= 4)
                {
                    healthtxt.enabled = false;
                    count = 0;
                }

            }
            //bedugging command for getting the caller function
            if (Input.GetKeyDown("space"))
            {
                caller();
            }
        }

        //Function to listen out for the correct speech input being spoken by the player
        private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
        {
            Debug.Log(speech.text);
            actions[speech.text].Invoke();
        }

        /*caller function attached to the action "Health"
        When called sets string and float to the healthstring
        sets the text to be true so it will draw on the screen*/
        private void caller()
        {
            healthI = playerHealthC.GetHealth();
            string healthString = "Health: " + healthI.ToString();
            healthtxt.enabled = true;
            healthtxt.text = healthString;
        }
    }
}