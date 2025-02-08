using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

    [RequireComponent(typeof(AudioSource))]
public class Conductor : MonoBehaviour
{
    [Header("Music Settings")]
    public AudioClip music;
    public float songBPM;
    public AudioSource musicSource;

    [Header("Level Notes")]
    public GameObject notePrefab;
    public float[] notes;
    private int nextIndex = 0;
    public float beatsShownInAdvance;

    [Header("Public Values: Don't Touch")]
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
        dspSongTime = (float) AudioSettings.dspTime;

        // Start the song
        musicSource.Play();
    }

    private void Update() {
        // Get the current position in the song in seconds
        songPosition = (float) (AudioSettings.dspTime - dspSongTime);
        // Get the current position in beats
        songPositionInBeats = songPosition / secPerBeat;

        // Actual song stuff
        if(nextIndex < notes.Length && notes[nextIndex] < songPositionInBeats + beatsShownInAdvance) {
            GameObject newNote = Instantiate(notePrefab, new Vector3(13f, -2.2f, -1f), Quaternion.identity);
            NoteBehaviour noteBehaviour = newNote.GetComponent<NoteBehaviour>();
            noteBehaviour.beatsShownInAdvance = beatsShownInAdvance;
            noteBehaviour.secPerBeat = secPerBeat;
            nextIndex++;
        }
        Debug.Log("Beat" + songPositionInBeats);
    }
    

}
