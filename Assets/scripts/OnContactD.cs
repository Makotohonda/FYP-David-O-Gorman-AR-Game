using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnContactD : MonoBehaviour {

    //create a list of JoyCons to support multiple
    private List<Joycon> m_joy;
    GameObject other;
    public GameObject particle;
    public int jc_ind = 0;
    public Text countText;
    public Text winText;
    private int count;

    void Start () {

        count = 0;
        SetCountText();
        winText.text = "";
        //create JoyCon manager
        m_joy = JoyconManager.Instance.j;
    }
	
	// Update is called once per frame
	void Update () {
        //Detect the amount of joycons detected to the game
        if (m_joy.Count > 0)
        {
            Joycon j = m_joy[jc_ind];
            // OnTriggerEnter();
        }
	}


   public  void OnCollisionEnter(Collision collision)
    {
        //When JoyCon Was implemented this code was used so if an object made collsion with sword the joyCon would rumble and objecy 
        // would be destroyed, 
        //if (m_joy.Count > 0)
        //{
        //    Joycon j = m_joy[jc_ind];
        //    //  OnTriggerEnter();
        //    Instantiate(particle, transform.position, transform.rotation);
        //    Debug.Log("Hit");
        //    count = count + 1;
        //    SetCountText();
        //    Destroy(collision.gameObject);
        //    j.SetRumble(160, 320, 0.6f, 200);
        //}
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12)
        {
            winText.text = "You Win";
        }
    }
}
