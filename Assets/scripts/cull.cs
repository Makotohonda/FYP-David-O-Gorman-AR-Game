using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*This was an early iteration class used for the detection of collision
  *between the enemies and the player, 
  *It was used for Joycon feedback and also displaying visual feedback on the HUD each time the
  * player was hit by an enemy however was removed for gameplay reasons */

public class cull : MonoBehaviour {

    private List<Joycon> m_joy;
    public int jc_ind = 0;
    GameObject other;
    public GameObject particle;
    AudioSource audioS;
    public AudioClip damage;
    public Text gameOverText;
    public bool gameOver;
  
    private float r;
    private float g;
    private float b;
    public float a;

    public Image overlayImage;
    public Text loseText;
    public Canvas canv;

    // Use this for initialization
    void Start()
    {
        //r = overlayImage.color.r;
        //g = overlayImage.color.g;
        //b = overlayImage.color.b;
        //a = overlayImage.color.a;

        m_joy = JoyconManager.Instance.j;
        audioS = GetComponent<AudioSource>();
        //audioS.Play();
    }

    public bool GetGame()
    {
        return gameOver;
    }

    public float GetA()
    {
        return a;
    }

    // Update is called once per frame
    void Update()
    {
        AdjustColor();

        ///Commeneted out code related to the implementation of the Joycon command
        ///left in to show the implementation of the JoyCon was present
        //if (m_joy.Count > 0)
        //{
        //    Joycon j = m_joy[jc_ind];
        //}

        //if (!particle.IsAlive())
        //{
        //    Destroy(gameObject);
        //}
    }

    public void OnCollisionEnter(Collision collision)
    {
        a += 0.05f;
        if (collision.gameObject.tag == "Cube")
        {
            Instantiate(particle, transform.position, transform.rotation);
        }

        if (a >= 0.5f)
        {
            gameOver = true;
        }

        /*If a controller is connected and and the player is hit by and enemy
         * then the connected JoyCon will rumble
            will work when the game is played in editor and not on device*/
        if (m_joy.Count > 0)
        {
            Joycon j = m_joy[jc_ind];
            //j.SetRumble(160, 320, 0.6f, 200);
        }
    }

    private void AdjustColor()
    {
     // Color c = new Color(r, g, b, a);
     // overlayImage.color = c;
    }
}
