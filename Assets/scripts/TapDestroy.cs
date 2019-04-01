using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HoloToolkit.Unity.InputModule
{

 

    public class TapDestroy : MonoBehaviour, IInputClickHandler
    {
        public GameObject particle;
        public ParticleSystem ps;
        public Text countText;
        public Text winText;
        public Canvas canv;
      //  public Text countText;
        int RoundNumber = 1;
        public string scoreDisp;
        int score = 0;
        AudioSource audioS;
        public AudioClip boom;
        private int count;

        GameObject manager;

        void Start()
        {
            ps = GetComponent<ParticleSystem>();
            manager = GameObject.FindWithTag("GameManager");
            audioS = GetComponent<AudioSource>();
            audioS.Play();
           // winText.text = "";
         //   score = 0;
            count = 0;
          //  SetCountText();
        }

        void FixedUpdate()
        {
            //  SetCountText();
            // Debug.Log("Score: " + score.ToString());
            //if (!ps.IsAlive())
            //{
            //    Destroy(gameObject);
            //}
        }
        public void OnInputClicked(InputClickedEventData eventData)
        {
            AudioSource.PlayClipAtPoint(boom, this.transform.position);
            // Increase the scale of the object just as a response.
            //  gameObject.transform.localScale -= 0.9f * gameObject.transform.localScale;
            Instantiate(particle, transform.position, transform.rotation);

            //  Debug.Log("Score: " + score.ToString());
            count = count + 1 ;
            SetCountText();
           
            audioS.Play();
            manager.GetComponent<GameManager>().IncreaseScore(1);
            Destroy(gameObject);
            Destroy(particle);
            //  count = count + 1;
            

            eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.
        }

        void SetCountText()
        {

            score = score + 1;
            Debug.Log("Score: " + count.ToString());
            // scoreDisp = score.ToString();
            // countText.text = scoreDisp;
            countText.text = "Score: " + count.ToString();

            //string scoreString = string.Format("", score);
            //countText.text = scoreString;

            if (score >= 12)
            {
                RoundNumber = RoundNumber + 1;
                // winText.text = "Round" + RoundNumber + "Complete";
            }
        }

    }
}
