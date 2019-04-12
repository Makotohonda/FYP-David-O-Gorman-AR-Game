using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

//Test Script to see if Voice commands would work, created to work with a cube
//User could say "Forward", "Backward", "Left", "Right" and the cube would move in the corresponding direction
//Was removed as the code was implemented into the game this script lost use. Kept to show development
public class voice : MonoBehaviour {

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add("forward", Forward);
        actions.Add("Backward", Backwards);
        actions.Add("Up", Up);
        actions.Add("Down", Down);


        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }


    private void Forward()
    {
        transform.Translate(1, 0, 0);

    }
    private void Backwards()
    {
        transform.Translate(-1, 0, 0);

    }
    private void Up()
    {
        transform.Translate(0, 1, 0);

    }
    private void Down()
    {
        transform.Translate(0, -1, 0);

    }

}
