using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    int miss;
    int okay;
    int good;
    int perfect;
    bool hasEnded;
    public TextMeshProUGUI scores;
    public TextMeshProUGUI scoreTotal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        miss = 0;
        okay = 0;
        good = 0;
        perfect = 0; 
        hasEnded = false;
    }

    void FixedUpdate() {
        if (hasEnded) {
            this.transform.position = new Vector3(Mathf.Max(Mathf.Lerp(this.transform.position.x ,6.6f, 2f * Time.deltaTime), 6.7f), this.transform.position.y, this.transform.position.z);
        }
    }

    // Update is called once per frame
    void OnEnable()
    {
        Conductor.OnNoteScored += UpdateScore;
    }

    void UpdateScore (string text) {
        //Debug.Log("event occurred");
        //Debug.Log(text);
        if(text == "miss") {
            miss++;
        } else if(text == "okay") {
            okay++;
        } else if(text == "good") {
            good++;
        } else if(text == "perfect") {
            perfect++;
        } else if (text == "end"){
            hasEnded = true;
        } else {
            Debug.Log("text field should have been miss, okay, good, or perfect");
            Debug.Log(text);
        }
        String newstring = perfect.ToString() + "\n" + good.ToString() + "\n" + okay.ToString() + "\n" + miss.ToString();
        scores.text = newstring;
        scoreTotal.text = (perfect*60 + good * 40 + okay * 20).ToString();
    }

}
