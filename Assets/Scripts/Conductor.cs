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
    
    [Header("Accuracy in ms")]
    float perfectWindow = 21.5f;
    float goodWindow = 43f;
    float okayWindow = 102f;
    // float missWindow = 150f;

    [Header("Public Values: Don't Touch")]
    public float progress = 0f;
    public float songLength;
    public float secPerBeat;
    public float songPosition;
    public float songPositionInBeats;
    public float dspSongTime;
    public GameObject lastNoteTop;
    public GameObject lastNoteBot;
    
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

        lastNoteTop = null;
        lastNoteBot = null;

        songLength = music.length;
    }
    

    private void Update() {
        // Get the current position in the song in seconds
        songPosition = (float) (AudioSettings.dspTime - dspSongTime);
        // Get the current position in beats
        songPositionInBeats = songPosition / secPerBeat;
        // Get the current progress
        progress = songPosition/songLength;

        // Actual song stuff
        if(nextIndexTop < notesTop.Length && notesTop[nextIndexTop] < songPositionInBeats + beatsShownInAdvance) {
            GameObject newNote = Instantiate(notePrefab, new Vector3(13f, 2.2f, -1f), Quaternion.identity);
            NoteBehaviour noteBehaviour = newNote.GetComponent<NoteBehaviour>();
            noteBehaviour.prevNote = lastNoteTop;
            lastNoteTop = newNote;
            noteBehaviour.beatsShownInAdvance = beatsShownInAdvance;
            noteBehaviour.secPerBeat = secPerBeat;
            noteBehaviour.isTop = true;
            nextIndexTop++;
        }
        if(nextIndexBottom < notesBottom.Length && notesBottom[nextIndexBottom] < songPositionInBeats + beatsShownInAdvance) {
            GameObject newNote = Instantiate(notePrefab, new Vector3(13f, -2.2f, -1f), Quaternion.identity);
            NoteBehaviour noteBehaviour = newNote.GetComponent<NoteBehaviour>();
            noteBehaviour.prevNote = lastNoteBot;
            lastNoteBot = newNote;
            noteBehaviour.beatsShownInAdvance = beatsShownInAdvance;
            noteBehaviour.secPerBeat = secPerBeat;
            noteBehaviour.isTop = false;
            nextIndexBottom++;
        }
        //Debug.Log("Beat" + songPositionInBeats);
    }
    

}
