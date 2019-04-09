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
        public GameObject bigBoss;
        int number = 0;
        GameObject[] objs = new GameObject[12];
        private bool gameOver;
        public Text gameOverText;
        private bool restart;
        public Text restartText;
        public GameManager script2;
        bool spawnBoss = false;
        public AudioClip spawn;
        public AudioClip spawnB;
        public AudioClip spawnC;
      
        public AudioClip bigBossSpawn;
        int deadCounter=0;

        public

          void Start()
        {
            gameOver = false;
            restart = false;
            gameOverText.text = "";
            StartCoroutine(spawner());
           
        }


        void Update()
        {
            //sets the speed to be off of time and in seconds
            float step = script2.speed * Time.deltaTime;

            //makes sure that the enemies are facing and moving towards whatever position the player is in
            for (int i = 0; i < numObjects; i++)
            {
                if (objs[i] != null && objs[i].GetComponent<moveToPlaye>().movement == true)
                {
                    objs[i].transform.position = Vector3.MoveTowards(objs[i].transform.position, OtherObject.transform.position, step);
                    objs[i].transform.LookAt(OtherObject.transform.position);
                }
            }
       
            //if Game is set to true then set round number t zero
            if (script2.GetGame() == true)
            { 
                  script2.roundNm = 1;  
            }

            //If restart game is set to true then destroy all the enemies currently on the screen
            //Sets restart to false and then calls the spawner co routine to begin
            if (script2.restart() == true)
            {
                
                for (int i = 0; i < objs.Length; i++)
                {
                    Destroy(objs[i].gameObject);
                }
                Destroy(GameObject.FindWithTag("bigBoss"));


                 script2.restartG = false;
                StartCoroutine(spawner());
            }


            if (script2.getDeadCount() >= 11)
            {
                if (script2.GetGame() == false)
                {
                    StartCoroutine(spawner());
                    StartCoroutine(script2.round());
                }
                Vector3 center = transform.position;
                int a = Random.Range(-0, 359);
                Vector3 pos = RandomCircle(center, 1.0f, a);
                AudioSource.PlayClipAtPoint(bigBossSpawn, this.transform.position * script2.speed);
                Instantiate(bigBoss, pos, Quaternion.identity);
                script2.ResetDeadCount();

            }
        }
        //Creates random spawn points around the radius of a set sized circle
        Vector3 RandomCircle(Vector3 center, float radius, int a)
        {
            float ang = a;
            radius = 20;
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
                yield return new WaitForSeconds(1);
                script2.roundNm = 1;
            }

        }

        /*spawner function which runs through and array and instantiates an enemy prefab to a random positon
         around the radius of a circle surronding the player, each prefab is given a different audio spawn sound
         before each spawn there is a wait of three seconds so that there is not an over wheleming amount of enemies */
        IEnumerator spawner()
        {
            Vector3 center = transform.position;
           
            for (int i = 0; i < numObjects; i++)
            {
                int a = Random.Range(-0, 359);
                Vector3 pos = RandomCircle(center, 1.0f, a);
                yield return new WaitForSeconds(3);
                int b = Random.Range(-0, 6);

                if (b == 0)
                {
                    AudioSource.PlayClipAtPoint(spawn, this.transform.position * script2.speed);
                    objs[i] = Instantiate(prefab, pos, Quaternion.identity);
                }
                if (b == 1)
                {
                    AudioSource.PlayClipAtPoint(spawnC, this.transform.position * script2.speed);
                    objs[i] = Instantiate(prefab, pos, Quaternion.identity);
                }
                if (b == 2)
                {
                    AudioSource.PlayClipAtPoint(spawnB, this.transform.position * script2.speed);
                    objs[i] = Instantiate(prefab, pos, Quaternion.identity);
                }
                if (b == 3)
                {
                    AudioSource.PlayClipAtPoint(spawn, this.transform.position * script2.speed);
                    objs[i] = Instantiate(prefab, pos, Quaternion.identity);
                }
                if (b == 4)
                {
                    AudioSource.PlayClipAtPoint(spawnC, this.transform.position * script2.speed);
                    objs[i] = Instantiate(prefab, pos, Quaternion.identity);
                }
                if (b == 5)
                {
                    AudioSource.PlayClipAtPoint(spawnB, this.transform.position * script2.speed);
                    objs[i] = Instantiate(prefab, pos, Quaternion.identity);
                }
            }
        }

        public void GameOver()
        {
            gameOverText.text = "Game Over!";
            gameOver = true;
            StopCoroutine(script2.round());
        }
    }
}

    