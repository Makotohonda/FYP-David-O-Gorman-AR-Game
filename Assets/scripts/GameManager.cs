using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

namespace HoloToolkit.Unity.InputModule
{


    public class GameManager : MonoBehaviour
    {
        // Creates Keyword recognizer 
        private KeywordRecognizer keywordRecognizer;
        // creates a dictionary and adds an empty string to it
        private Dictionary<string, Action> actions = new Dictionary<string, Action>();

        public int roundNm = 1;
        public int score;
        int playerHealth = 100;
        public Text scoreText;
        public Text gameOverText;
        private List<Joycon> m_joy;
        public int jc_ind = 0;
        GameObject box;
        public Text restartText;
        public Vector3 gyro;
        Vector3 position1;
        Vector3 position2;
        public bool gameHalt = false;
        bool spawn = false;
        GameObject culling;
        bool isCreated = false;

        public float speed = 1f;
        public Text roundText;
        public Text RoundUI;
        public Text yes;
        public Text no;
        public bool restartG = false;
        public int deadCounter;
        // Use this for initialization
        void Start()
        {
            //strings retry and exit to dictionary, connected and calling corresponding functions
            actions.Add("Retry", YesCall);
            actions.Add("Exit", NoCall);

            //adds the strings to the dictionary
            keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
            keywordRecognizer.Start();

            box = GameObject.FindWithTag("PlayerBox");
            m_joy = JoyconManager.Instance.j;
            culling = GameObject.FindWithTag("Culling");
        }

        private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
        {
            Debug.Log(speech.text);
            actions[speech.text].Invoke();
        }

        //Keeps track of enemies killed
        public void IncreaseDeadCount()
        {
            deadCounter++;
        }
        //Returns dead count
        public int getDeadCount()
        {
            return deadCounter;
        }
        //Rests Dead count on new games
        public void ResetDeadCount()
        {
            deadCounter = 0;
        }

        void Update()
        {

            //Editor command for debugging purposes
            if (Input.GetKeyDown("y"))
            {
                YesCall();
            }

            if (m_joy.Count > 0)
            {
                Joycon j = m_joy[jc_ind];
                gyro = j.GetGyro();
                Debug.Log(gyro);
            }

            //sets text to strings when the game is triggered to be over
            if (gameHalt == true)
            {
                gameOverText.text = "Game Over!";
                restartText.text = "Try Again?";
                yes.text = "'retry'";
                no.text = "'exit'";
            }
        }

        public bool GetGame()
        {
            return gameHalt;
        }

        public void IncreaseScore(int num)
        {
            score += num;
        }
        //This funtion decrease rthe players health and will end game if reaches zero 
        public void DecreaseHealth(int num)
        {
            playerHealth -= num;
            if (playerHealth <= 0)
            {
                playerHealth = 0;
                spawn = true;
                gameHalt = true;
            }
        }

        //Returns the players health
        public int GetHealth()
        {
            return playerHealth;
        }
        //returns the restart game so CircleSpawn can reset in corelations
        public bool restart()
        {
            return restartG;
        }

        //resets players health, score and round. 
        //clears screen of all text
        public void RestartGame()
        {
            playerHealth = 100;
            score = 0;
            roundNm = 1;
            gameOverText.text = "";
            restartText.text = "";
            yes.text = "";
            no.text = "";
            restartG = true;
        }

       
        public void OnCollisionEnter(Collision collision)
        {

        }

        /*if player says 'Retry'this function is called  which will call to restart the game
          this will only trigger is the game is over, enemies speed is reset to zero and gameover set to false
             */
        public void YesCall()
        {
            if (gameHalt == true)
            {
                RestartGame();
                Destroy(yesBox);
                gameHalt = false;
                speed = 1f;
            }
        }
        /*if player says 'Exit' this function is called which will exit the application and stop it
          unlike the YesCall this is accesible at any time during the game so the player can quit whenever
          they want. Note: Application.Quit is not a function that can be used in Editor*/ 
        public void NoCall()
        {
            Application.Quit();
        }

        //Funtion to keep track of rounf count and output to screen on the start of each round
        public IEnumerator round()
        {
            roundNm++;
            string roundString = "Round: " + roundNm.ToString();
            roundText.enabled = true;
            roundText.text = roundString;

            if (roundNm == 1)
            {
                speed = 1f;
            }

            speed += .5f;
            yield return new WaitForSeconds(5);
            roundText.enabled = false;


        }
    }
}
