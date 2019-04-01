using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text countText;
    public Text roundText;

    float gameTimer = 0f;
    float roundTimer = 0f;
    int roundNumber = 0;
    bool round1 = false;
    bool round2 = false;
    bool round3 = false;
    // Update is called once per frame
    void Update() {

        gameTimer += Time.deltaTime;
        roundTimer += Time.deltaTime;
        int seconds = (int)(gameTimer % 60);
       // roundNumberV();
        string timerString = string.Format("{0:0}", seconds);

        countText.text = timerString;

      

    }

    void roundNumberV() {

        if(round1 == true)
        {
            roundNumber = roundNumber + 1;
          //  Debug.Log(roundNumber);
            
                string roundString = string.Format("Round: ", roundNumber);

            //for (int i = 0; i <= 10; i++)
            //{
            //    roundText.text = roundString;
            //}
            roundTimer = 0;
            round1 = false;

        }

        if (round2 == true)
        {
            roundNumber = roundNumber + 1;
          //  Debug.Log(roundNumber);
            string roundString = string.Format("Round: ", roundNumber);
            roundText.text = roundString;
            roundTimer = 0;
            round2 = false;

        }

        if (round3 == true)
        {

        }

    }

}
