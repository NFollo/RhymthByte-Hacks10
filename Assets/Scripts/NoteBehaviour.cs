using Unity.VisualScripting;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{

    [Header("Behaviour")]
    public float fadeSpeed;
    public GameObject dyingNotePrefab;
    private string mytext;
    private bool isFakeAlive;


    [Header("Public Values: Don't Touch")]
    private Conductor conductor;
    private float beatsShownInAdvance;
    private float secPerBeat;
    public float hitBeat;
    public bool isTop;

    private float velocity;
    public GameObject prevNote;
    bool isAlive;

    private void Awake() {
        conductor = GetComponentInParent<Conductor>();
    }

    private void Start() {
        conductor = GetComponentInParent<Conductor>();
        beatsShownInAdvance = conductor.beatsShownInAdvance;
        secPerBeat = conductor.secPerBeat;
        Vector3 startPosition = transform.position;
        Vector3 target = new Vector3(-4.5f, transform.position.y, transform.position.z);
        float difference = startPosition.x - target.x;
        float timeToReachTarget = beatsShownInAdvance * secPerBeat;
        velocity = difference/timeToReachTarget;
        isAlive = true;
        mytext = "";
        isFakeAlive = false;
    }

    public void Miss() {
        // Destroy(this.gameObject);
        Debug.Log("Miss!");
    }

    public void Okay() {
        mytext = "OKAY";
        isAlive = false;
    }

    public void Good() {
        mytext = "GOOD";
        isAlive = false;
    }

    public void Perfect() {
        mytext = "PERFECT!";
        isAlive = false;
    }

    private void OnBecameInvisible() {
        Destroy(this.gameObject);
        
    }

    private void Update() { 

        if (isAlive || isFakeAlive) {
            transform.position = new Vector3(transform.position.x - (velocity * Time.deltaTime), transform.position.y, transform.position.z);
        }

        if (!isAlive) {
            if (dyingNotePrefab != null) {
                GameObject fakeNote = Instantiate(dyingNotePrefab, transform.position, Quaternion.identity);
                DyingNote dyingNote = fakeNote.GetComponent<DyingNote>();
                dyingNote.fadeSpeed = fadeSpeed;
                dyingNote.mytext = mytext;
                dyingNote.isFakeAlive = isFakeAlive;
                dyingNote.velocity = velocity;
            }
            dyingNotePrefab = null;
            Destroy(this.gameObject);
        }

    }

    // private void FixedUpdate() {
    //     if (!isAlive) {
    //         Color color = this.spriteRenderer.color;
    //         color.a = Mathf.Max(0, color.a - fadeSpeed * Time.deltaTime);          
    //         if (color.a <= 0) {
    //             Destroy(this.gameObject);
    //         }
    //         this.spriteRenderer.color = color;
    //     }    
    // }

}
