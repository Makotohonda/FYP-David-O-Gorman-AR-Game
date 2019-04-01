using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

    int time, a;
    float x;
    bool count;
    public string timeDisp;
    public Canvas canv;
    public Text txt;

    int m_score = 0;
   // int RoundNumber = 1;
    public Text countText;  

    void Start()
    {
       // m_score = 0;
        // txt = canv.GetComponent<Text>();
        time = 2;
        count = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (count)
        //{
        //    timeDisp = time.ToString();
        //    txt.text = timeDisp;
        //    x += Time.deltaTime;
        //    a = (int)x;
        //    Debug.Log(a);
        //    switch (a)
        //    {
        //        case 0: txt.text = "3"; break;
        //        case 1: txt.text = "2"; break;
        //        case 2: txt.text = "1"; break;
        //        case 3:
        //            txt.enabled = false;
        //            count = false; break;
        //    }
        //}
       // Debug.Log("Score: " + m_score.ToString());
    }


    public void OnCollisionEnter(Collision collision) {

        if (collision.collider.tag == "Cube")
        {
            SetCountText();
        }

    }

    void SetCountText()
    {

        m_score = m_score + 1;
      //  Debug.Log("Score: " + m_score.ToString());
        // scoreDisp = score.ToString();
        // countText.text = scoreDisp;
        countText.text = "Score: " + m_score.ToString();

        //string scoreString = string.Format("", score);
        //countText.text = scoreString;

        //if (m_score >= 12)
        //{
        //    RoundNumber = RoundNumber + 1;
        //    // winText.text = "Round" + RoundNumber + "Complete";
        //}
    }
}
