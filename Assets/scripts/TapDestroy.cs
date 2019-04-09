using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HoloToolkit.Unity.InputModule
{
    /* This script was intially what was used to as my first type of input and was created so that
        I could destroy incoming objects by tapping at them, I went on to develop this further
        (see followCamera.cs) by creating a weapon that would trigger on command of a tap*/
 

    public class TapDestroy : MonoBehaviour, IInputClickHandler
    {
        public GameObject particle;
        public ParticleSystem ps;
        public Text countText;
        public Text winText;
        public Canvas canv;
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
            count = 0;

        }

        void FixedUpdate()
        {

        }
        public void OnInputClicked(InputClickedEventData eventData)
        {
            AudioSource.PlayClipAtPoint(boom, this.transform.position);
            Instantiate(particle, transform.position, transform.rotation);
            count = count + 1 ;
            SetCountText();
           
            audioS.Play();
            manager.GetComponent<GameManager>().IncreaseScore(1);
            Destroy(gameObject);
            Destroy(particle);
            eventData.Use(); // Mark the event as used, so it doesn't fall through to other handlers.
        }

        void SetCountText()
        {
            score = score + 1;
            Debug.Log("Score: " + count.ToString());
            countText.text = "Score: " + count.ToString();

            if (score >= 12)
            {
                RoundNumber = RoundNumber + 1;
            }
        }

    }
}
