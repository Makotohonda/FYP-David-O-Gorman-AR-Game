using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using System.Linq;

namespace HoloToolkit.Unity.InputModule
{


    public class GameManager : MonoBehaviour
    {
        private KeywordRecognizer keywordRecognizer;
        private Dictionary<string, Action> actions = new Dictionary<string, Action>();


        int score;
        int playerHealth = 100;
        public Text scoreText;
        public Text gameOverText;
        private List<Joycon> m_joy;
        public int jc_ind = 0;
        GameObject box;
        public GameObject yesBox;
        public GameObject noBox;
        public Text restartText;
        public Vector3 gyro;
        Vector3 position1;
        Vector3 position2;
        public bool gameHalt = false;
        bool spawn = false;
        public AudioClip yes;
        public AudioClip no;
        GameObject culling;
        // Use this for initialization
        void Start()
        {
            actions.Add("Yes", YesCall);
            actions.Add("No", NoCall);

            keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
            keywordRecognizer.Start();

            box = GameObject.FindWithTag("PlayerBox");
            m_joy = JoyconManager.Instance.j;
            position1 = new Vector3(1, -1, 4);
            position2 = new Vector3(2, -1, 4);
            culling = GameObject.FindWithTag("Culling");
        }

        private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
        {
            Debug.Log(speech.text);
            actions[speech.text].Invoke();
        }

        // Update is called once per frame
        void Update()
        {
            if (score > 10)
            {

            }
            if (spawn == true)
            {
                spawnButtons();
                // gameHalt = false;
              //  spawn = false;
            }

            if (m_joy.Count > 0)
            {
                Joycon j = m_joy[jc_ind];
                gyro = j.GetGyro();
                Debug.Log(gyro);
            }
        //    GetGame();
        }

        public bool GetGame()
        {
            return gameHalt;

        }
        public void IncreaseScore(int num)
        {
            score += num;
            scoreText.text = "Score: " + score;
        }
        public void DecreaseHealth(int num)
        {
            playerHealth -= num;
            if (playerHealth <= 0)
            {
                playerHealth = 0;
                spawn = true;
                gameHalt = true;
            }
           // Debug.Log("Health: " + playerHealth);
             //   spawn = true;
        }

        public int GetHealth()
        {
            return playerHealth;
        }
        public void RestartGame()
        {
            playerHealth = 100;
            score = 0;
            gameOverText.text = "";
            restartText.text = "";
            culling.GetComponent<cull>().gameOver = true;
        }

        public void spawnButtons()
        {
            if (spawn == true)
            {
                gameOverText.text = "Game Over!";
                restartText.text = "Try Again?";
                // Instantiate(yesBox, yesBox.transform.position, yesBox.transform.rotation);
                GameObject yesBox1 = Instantiate(yesBox, yesBox.transform.position, yesBox.transform.rotation);
                yesBox.transform.position = new Vector3(position1.x, position1.y + transform.localScale.y / 2, position1.z);

                GameObject noBox1 = Instantiate(noBox, noBox.transform.position, noBox.transform.rotation);
                noBox.transform.position = new Vector3(position2.x, position2.y + transform.localScale.y / 2, position2.z);
                //  Instantiate(noBox, 0,0,0);
                //   gameHalt = false;
                spawn = false;
            }

        }

        public void OnCollisionEnter(Collision collision)
        {


            //if (GameObject.tag == "No")
            //{

        
            if (collision.gameObject.tag == "Yes")
            {
                RestartGame();
                Destroy(yesBox);
                //Debug.Log("HITHITHITHITHITHITHIT");
                gameHalt = false;
                
            }
        }

        public void YesCall()
        {
            if (gameHalt == true)
            {
                RestartGame();
                Destroy(yesBox);
                //Debug.Log("HITHITHITHITHITHITHIT");
                gameHalt = false;
            }
        }

        public void NoCall()
        {
            if(gameHalt == true)
            {
                RestartGame();
                Destroy(noBox);
                //Debug.Log("HITHITHITHITHITHITHIT");
                gameHalt = false;
            }
        }
    }
}
