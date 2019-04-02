using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace HoloToolkit.Unity.InputModule
{

    public class CircleSpawn : MonoBehaviour
    {
        int numObjects = 12;
        public GameObject prefab;
        public GameObject prefab2;
        public GameObject prefab3;
        public GameObject OtherObject;
        // public float speed = 1.0f;
        int number = 0;
        GameObject[] objs = new GameObject[12];
        //  Random rnd = new Random();
        private bool gameOver;
        public Text gameOverText;
        private bool restart;
        public Text restartText;
        public GameManager script2;
       
       
        AudioSource audioS;
        public AudioClip spawn;
        //  Color zm;



        public

          void Start()
        {
            gameOver = false;
            restart = false;
            gameOverText.text = "";
            //  StartCoroutine(spawner());
            // Coroutine b = StartCoroutine(spawner());
            //  Color zm = roundText.color;
            audioS = GetComponent<AudioSource>();
            audioS.Play();
            //  Coroutine c = StartCoroutine(spawner());
            StartCoroutine(spawnCheck());
         //   Debug.Log(script2.gameHalt);
        }


        void Update()
        {


            float step = script2.speed * Time.deltaTime;
            for (int i = 0; i < numObjects; i++)
            {
                if (objs[i] != null)
                {
                    objs[i].transform.position = Vector3.MoveTowards(objs[i].transform.position, OtherObject.transform.position, step);
                    objs[i].transform.LookAt(OtherObject.transform.position);
                }
            }

            //    Debug.Log(script2.a);
            Debug.Log(script2.GetGame());
            if (script2.GetGame() == true)
            {

                //gameOver = true;
                  Debug.Log("GAME OVER GAME OVER GAME OVER");
                  script2.roundNm = 1;
                //StopCoroutine(c);
                //break;
            }


        }
        Vector3 RandomCircle(Vector3 center, float radius, int a)
        {
            //Debug.Log(a);
            float ang = a;
            radius = 10;
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            pos.y = center.y;
            return pos;
        }
        IEnumerator spawnCheck()
        {
            Coroutine b = StartCoroutine(spawner());
            if (script2.GetGame() == true)
            {
                Debug.Log("Inside of the spawn check");
                //gameOver = true;
                //  Debug.Log("GAME OVER GAME OVER GAME OVER");
                yield return new WaitForSeconds(1);
                script2.roundNm = 1;
                StopCoroutine(b);
                //break;
            }

        }
        IEnumerator spawner()
        {
            //Coroutine c = StartCoroutine(spawner());
            Vector3 center = transform.position;
           
            for (int i = 0; i < numObjects; i++)
            {

                //int a = 360 / numObjects * i;
                //Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
                int a = Random.Range(-0, 359);
                Vector3 pos = RandomCircle(center, 1.0f, a);
                yield return new WaitForSeconds(3);
                //  Debug.Log("Test");
                
                int b = Random.Range(-0, 6);

                if (b == 0)
                {
                    AudioSource.PlayClipAtPoint(spawn, this.transform.position * script2.speed);
                    objs[i] = Instantiate(prefab, pos, Quaternion.identity);
                   
                    audioS.Play();
                }
                if (b == 1)
                {
                    AudioSource.PlayClipAtPoint(spawn, this.transform.position * script2.speed);
                    objs[i] = Instantiate(prefab2, pos, Quaternion.identity);
                   
                    audioS.Play();
                }
                if (b == 2)
                {
                    AudioSource.PlayClipAtPoint(spawn, this.transform.position * script2.speed);
                    objs[i] = Instantiate(prefab3, pos, Quaternion.identity);
                   
                    audioS.Play();
                }
                if (b == 3)
                {
                    AudioSource.PlayClipAtPoint(spawn, this.transform.position * script2.speed);
                    objs[i] = Instantiate(prefab, pos, Quaternion.identity);
                   
                    audioS.Play();
                }
                if (b == 4)
                {
                    AudioSource.PlayClipAtPoint(spawn, this.transform.position * script2.speed);
                    objs[i] = Instantiate(prefab, pos, Quaternion.identity);
                  
                    audioS.Play();
                }
                if (b == 5)
                {
                    AudioSource.PlayClipAtPoint(spawn, this.transform.position * script2.speed);
                    objs[i] = Instantiate(prefab2, pos, Quaternion.identity);
                  
                    audioS.Play();
                }

                if (i >= 11)
                {
                    if (script2.GetGame() == false)
                    {
                        yield return new WaitForSeconds(10);
                        StartCoroutine(script2.round());
                        //   zm.a += 1.5f;
                        i = 0;
                    }
                }
            }
        }

        public void GameOver()
        {

            gameOverText.text = "Game Over!";
            gameOver = true;
            StopCoroutine(script2.round());
        }

        //public void round()
        //{
        //    roundNm++;
        //    string roundString = "Round: " + roundNm.ToString();
        //    string roundUIString = "Round: " + roundNm.ToString();
        //    roundText.enabled = true;

        //    roundText.text = roundString;
        //    RoundUI.text = roundUIString;
        //    if (roundNm == 1)
        //    {
        //        speed = 1f;
        //    }
        //    speed += .5f;
        //    //yield return new WaitForSeconds(5);
        //    //roundText.enabled = false;


        //}
    }
}

    