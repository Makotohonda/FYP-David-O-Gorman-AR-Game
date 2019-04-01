using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdown : MonoBehaviour {

    int time, a;
    float x;
    bool count;
    public string timeDisp;
    public Canvas canv;
    public Text txt;

    void Start()
    {
       // txt = canv.GetComponent<Text>();
        time = 2;
        count = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (count)
        {
            timeDisp = time.ToString();
            txt.text = timeDisp;
            x += Time.deltaTime;
            a = (int)x;
           // Debug.Log(a);
            switch (a)
            {
                case 0: txt.text = "3"; break;
                case 1: txt.text = "2"; break;
                case 2: txt.text = "1"; break;
                case 3:
                    txt.enabled = false;
                    count = false; break;
            }
        }
    }
}
