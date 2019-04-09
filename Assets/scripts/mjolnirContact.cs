using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HoloToolkit.Unity.InputModule
{

    public class mjolnirContact : MonoBehaviour
    {

        bool dead = false;
        public GameObject particle;
        GameObject manager;
        public GameManager gameOverSwitch;
        AudioSource audioS;
        public AudioClip boom;
        public AudioClip bbPain;
        public float bbHealth = 30;
        private int count;
        public Text countText;
        int RoundNumber = 1;
        int score = 0;
        public GameManager script2;
        bool bbdeath = false;

        // Use this for initialization
        void Start()
        {
            //assigns game manager to manager
            manager = GameObject.FindWithTag("GameManager");
            audioS = GetComponent<AudioSource>();
            audioS.Play();
            count = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        /*checks the collision of regular enemies and the hammer, 
        if collision detected, triggers enemys animation, starts deletions counter
        plays audio, instantiates particle and increases score */
        public void OnCollisionEnter(Collision collision) 
        {
            if (collision.gameObject.tag == "Cube")
            {
                AudioSource.PlayClipAtPoint(boom, this.transform.position);
                Instantiate(particle, transform.position, transform.rotation);
                count = count + 1;
                SetCountText();

                audioS.Play();
                manager.GetComponent<GameManager>().IncreaseScore(1);
                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Animator anime = collision.gameObject.GetComponent<Animator>();
                collision.gameObject.GetComponent<moveToPlaye>().movement = false;
                audioS.clip = bbPain;
                audioS.Play();
                anime.SetBool("Dead", true);
                dead = true;
                collision.gameObject.GetComponent<DestroyOgre>().ActivateTimer();
            }

            /*checks the collision of the boss and the hammer, 
             if collision detected, decrements health , starts deletions counter after reaching zero
            */

            if (collision.gameObject.tag == "bigBoss")
            {
                bbHealth -= 10;
                  
                if (bbHealth <= 0)
                {
                    Debug.Log("Equal 0");
                    collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    collision.gameObject.GetComponent<BossControl>().movement = false;
                    Animator anime = collision.gameObject.GetComponent<Animator>();
                    anime.SetBool("death", true);
                    bbdeath = true;
                    collision.gameObject.GetComponent<DestroyBigB>().ActivateTimer();
                }
            }
        }

        void SetCountText()
        {
            score = score + 1;
            Debug.Log("Score: " + count.ToString());

            if (score >= 12)
            {
                RoundNumber = RoundNumber + 1;
            }
        }

    }
}