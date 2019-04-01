using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

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
