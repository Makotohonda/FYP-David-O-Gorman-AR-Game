using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cull : MonoBehaviour {

    private List<Joycon> m_joy;
    public int jc_ind = 0;
    GameObject other;
    public GameObject particle;
    //private bool gameOver;
    AudioSource audioS;
    public AudioClip damage;
    public Text gameOverText;
    public bool gameOver;

    public Image overlayImage;
  
    private float r;
    private float g;
    private float b;
    public float a;

    public Text loseText;
    public Canvas canv;

    // Use this for initialization
    void Start()
    {
        r = overlayImage.color.r;
        g = overlayImage.color.g;
        b = overlayImage.color.b;
        a = overlayImage.color.a;

        m_joy = JoyconManager.Instance.j;

        audioS = GetComponent<AudioSource>();
      //  audioS.Play();

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
         //   Debug.Log(a);

        if (collision.gameObject.tag == "Cube")
        {
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            audioS.Play();
            Destroy(particle);

        }

        if (a >= 0.5f)
        {
            gameOver = true;
         //   Debug.Log("Game Over");
        }


        if (gameOver == true)
        {
            changeScene(0);
        }
        if (m_joy.Count > 0)
        {
            Joycon j = m_joy[jc_ind];
       //     j.SetRumble(160, 320, 0.6f, 200);

        }


    }

    private void AdjustColor()
    {
     // Color c = new Color(r, g, b, a);
     // overlayImage.color = c;

    }

    private void LoseGame()
    {


    }

    private void changeScene(int scene)
    {
        //  yield return new WaitForSeconds(5);
        //SceneManager.UnloadSceneAsync(scene - 1);
       /// SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
