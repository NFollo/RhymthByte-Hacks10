using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Conductor : MonoBehaviour
{
    [Header("Music Settings")]
    public AudioClip music;
    public float songBPM;
    public AudioSource musicSource;

    [Header("Level Notes")]
    public GameObject notePrefab;
    public float[] notesTop;
    public float[] notesBottom;
    private int nextIndexTop = 0;
    private int nextIndexBottom = 0;
    public float beatsShownInAdvance;
    
    [Header("Accuracy(+-) Window in ms")]
    public float perfectWindow = 43f;
    public float goodWindow = 102f;
    public float okayWindow = 135f;

    [Header("Public Values: Don't Touch")]
    public float progress = 0f;
    public float songLength;
    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    public float dspSongTime;
    
    private void Start() {
        // Get the audio player for the music
        musicSource = GetComponent<AudioSource>();

        // Sets the clip to the souce
        musicSource.clip = music;

        // Get the length of each beat in seconds
        secPerBeat = 60f / songBPM;

        // Get the time when the song started.
        dspSongTime = (float) AudioSettings.dspTime + beatsShownInAdvance * secPerBeat;

        // Start the song
        musicSource.PlayScheduled(dspSongTime);

        songLength = music.length;
    }
    
    private Queue<NoteBehaviour> topLane = new Queue<NoteBehaviour>();
    private Queue<NoteBehaviour> bottomLane = new Queue<NoteBehaviour>();

    private void Update() {
        // Get the current position in the song in seconds
        songPosition = (float) (AudioSettings.dspTime - dspSongTime);
        // Get the current position in beats
        songPositionInBeats = songPosition / secPerBeat;
        // Get the current progress
        progress = songPosition/songLength;

        // Spawns the notes
        if(nextIndexTop < notesTop.Length && notesTop[nextIndexTop] < songPositionInBeats + beatsShownInAdvance) {
            GameObject newNote = Instantiate(notePrefab, new Vector3(13f, 2.2f, -1f), Quaternion.identity, this.transform);
            NoteBehaviour noteBehaviour = newNote.GetComponent<NoteBehaviour>();
            noteBehaviour.isTop = true;
            noteBehaviour.hitBeat = notesTop[nextIndexTop];
            nextIndexTop++;
            topLane.Enqueue(noteBehaviour);
        }
        if(nextIndexBottom < notesBottom.Length && notesBottom[nextIndexBottom] < songPositionInBeats + beatsShownInAdvance) {
            GameObject newNote = Instantiate(notePrefab, new Vector3(13f, -2.2f, -1f), Quaternion.identity, this.transform);
            NoteBehaviour noteBehaviour = newNote.GetComponent<NoteBehaviour>();
            noteBehaviour.isTop = false;
            noteBehaviour.hitBeat = notesBottom[nextIndexBottom];
            nextIndexBottom++;
            bottomLane.Enqueue(noteBehaviour);
        }
        
        // Enter the if statement if you can no longer hit the note
        if(topLane.Count != 0 && topLane.Peek().hitBeat*secPerBeat - songPosition < (0-(okayWindow/1000))) {
            topLane.Dequeue().Miss();
        }

        // Enter the if statement if you can no longer hit the note
        if(bottomLane.Count != 0 && bottomLane.Peek().hitBeat*secPerBeat - songPosition < (0-(okayWindow/1000))) {
            bottomLane.Dequeue().Miss();
        }
    }

    public void actOnTop(float currentPosition) {
        if(topLane.Count != 0 && Mathf.Abs(topLane.Peek().hitBeat*secPerBeat - currentPosition) <(okayWindow/1000)) {
            if(Mathf.Abs(topLane.Peek().hitBeat*secPerBeat - currentPosition) <(goodWindow/1000)) {
                if(Mathf.Abs(topLane.Peek().hitBeat*secPerBeat - currentPosition) <(perfectWindow/1000)) {
                    topLane.Dequeue().Perfect();
                    return;
                }
                topLane.Dequeue().Good();
                return;
            }
            topLane.Dequeue().Okay();
            return;
        }
    }
    
    public void actOnBottom(float currentPosition) {
        if(bottomLane.Count != 0 && Mathf.Abs(bottomLane.Peek().hitBeat*secPerBeat - currentPosition) < (okayWindow/1000)) {
            if(Mathf.Abs(bottomLane.Peek().hitBeat*secPerBeat - currentPosition) <(goodWindow/1000)) {
                if(Mathf.Abs(bottomLane.Peek().hitBeat*secPerBeat - currentPosition) <(perfectWindow/1000)) {
                    bottomLane.Dequeue().Perfect();
                    return;
                }
                bottomLane.Dequeue().Good();
                return;
            }
            bottomLane.Dequeue().Okay();
            return;
        }
    }
}
