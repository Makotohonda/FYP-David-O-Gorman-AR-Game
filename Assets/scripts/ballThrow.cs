using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;


/*This class is used to operate the players secondary weapon
 * The ball is launched forward in the direction the player faces
 * This weapon is voice activated, when the player says the correct word
 */
namespace HoloToolkit.Unity.InputModule
{

    public class ballThrow : MonoBehaviour
    {

        private KeywordRecognizer keywordRecognizer;
        //creates new dictionary and adds string to it
        private Dictionary<string, Action> actions = new Dictionary<string, Action>();
        public Transform cameraTransform;
        public float distanceFromCamera;
        bool launch = false;
        public GameObject prefab;
        GameObject manager;
        public Vector3 resultPos;
        bool trigger = false;
        AudioSource audioS;
        public AudioClip boom;
        public GameObject particle;
        private int count;

        // Use this for initialization
        void Start()
        {
            manager = GameObject.FindWithTag("GameManager");
            audioS = GetComponent<AudioSource>();

            //command to use to operate the ball
            actions.Add("flame", ballT);
            keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
            keywordRecognizer.Start();

        }

        // Update is called once per frame
        void Update()
        {
            //Debugging in editor command for firing
            if (Input.GetKeyDown("r"))
            {
                ballT();
            }

            //checks to see if launch is triggered, creates object and fires it in direction player is facing
            if (launch)
            {
                GameObject ball = Instantiate(prefab) as GameObject;
                ball.transform.position = cameraTransform.position + cameraTransform.forward;
                Rigidbody rb = ball.GetComponent<Rigidbody>();
                rb.velocity = Camera.main.transform.forward * 40;
                launch = false;
            }

        }

        // checks to see what player says and if correct command
        private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
        {
            Debug.Log(speech.text);
            actions[speech.text].Invoke();
        }

        void ResetPosition()
        {

        }

        void ballT()
        {
            launch = true;
        }

        //checks for collision between standard enemies and balls
        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Cube")
            {
                AudioSource.PlayClipAtPoint(boom, this.transform.position);
                Instantiate(particle, transform.position, transform.rotation);
                collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Animator anime = collision.gameObject.GetComponent<Animator>();
                collision.gameObject.GetComponent<moveToPlaye>().movement = false;
                anime.SetBool("Dead", true);
                collision.gameObject.GetComponent<DestroyOgre>().ActivateTimer();
                Destroy(gameObject);
            }

        }
    }
}