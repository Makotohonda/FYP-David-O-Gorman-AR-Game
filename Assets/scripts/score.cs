using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Small script to keep track of the users ingame score
public class score : MonoBehaviour {

    int time, a;
    float x;
    bool count;
    public string timeDisp;
    public Canvas canv;
    public Text txt;

    int m_score = 0;
    public Text countText;  

    void Start()
    {
        time = 2;
        count = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

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
        countText.text = "Score: " + m_score.ToString();
    }
}
