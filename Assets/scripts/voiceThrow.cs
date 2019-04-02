using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class voiceThrow : MonoBehaviour {
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public Transform cameraTransform;
    public float distanceFromCamera;
    bool launch = false;
    bool goBack = false;
    bool trigger = false;
    public GameObject hammer;
  //  public AudioClip whoosh;

    void Start()
    {
        actions.Add("fire", Fire);
       
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    void Update()
    {

        Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        hammer.transform.position = new Vector3(resultingPosition.x, resultingPosition.y, resultingPosition.z);

        if (Input.GetKeyDown("space"))
       // if (trigger == true)
        {
            launch = true;
            trigger = false;
        }
        //  Debug.Log(trigger);
        if (launch)
        {
            if ((distanceFromCamera < 10) && goBack == false)
            {
                distanceFromCamera += 1;
            }

            if (distanceFromCamera >= 10)
            {
                goBack = true;
            }

            if ((distanceFromCamera > -1) && goBack == true)
            {
                distanceFromCamera -= 1f;
            }

            if (goBack == true && distanceFromCamera <= -1)
            {
                goBack = false;
                launch = false;
                distanceFromCamera = -1;
            }
        }


    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }


    private void Fire()
    {
        transform.Translate(1, 0, 0);
        trigger = true;
    }
   


}
