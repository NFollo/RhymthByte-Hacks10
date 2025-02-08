using UnityEngine;
using UnityEngine.Android;

    [RequireComponent(typeof(AudioSource))]
public class Conductor : MonoBehaviour
{
    [Header("Music Settings")]
    public AudioClip music;
    public float songBPM;
    public AudioSource musicSource;

    [Header("Public Values: Don't Touch")]
    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    public float dspSongTime;
    public float currentTime;
    
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
        currentTime = (float) AudioSettings.dspTime;
        // Get the current position in beats
        songPositionInBeats = songPosition / secPerBeat;
    }
    

}
