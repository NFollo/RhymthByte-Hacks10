using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{

    [Header("Behaviour")]
    public float fadeSpeed; 
    private string mytext;


    [Header("Public Values: Don't Touch")]
    private Conductor conductor;
    private float beatsShownInAdvance;
    private float secPerBeat;
    public float hitBeat;
    public bool isTop;

    private float velocity;
    private bool shouldMove;

    private SpriteRenderer spriteRenderer; 
    private TextMeshProUGUI textMeshPro;

    private void Awake() {
        conductor = GetComponentInParent<Conductor>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
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
        shouldMove = true;
        mytext = "";
        textMeshPro.text = mytext;
    }

    public void Miss() {
        // Destroy(this.gameObject);
        Debug.Log("Miss!");
    }

    public void Okay() {
        mytext = "OKAY";
        shouldMove = false;
        textMeshPro.text = mytext;
    }

    public void Good() {
        mytext = "GOOD";
        shouldMove = false;
        textMeshPro.text = mytext;
    }

    public void Perfect() {
        mytext = "PERFECT!";
        shouldMove = false;
        textMeshPro.text = mytext;
    }

    private void OnBecameInvisible() {
        Destroy(this.gameObject);
        
    }

    private void Update() { 

        if (shouldMove) {
            transform.position = new Vector3(transform.position.x - (velocity * Time.deltaTime), transform.position.y, transform.position.z);
        }

        if (!shouldMove) {
            Color color = this.spriteRenderer.color;
            textMeshPro.alpha = Mathf.Max(0, textMeshPro.alpha - fadeSpeed * Time.deltaTime); 
            color.a = Mathf.Max(0, color.a - fadeSpeed * Time.deltaTime);          
            if (color.a <= 0) {
                Destroy(this.gameObject);
            }
            this.spriteRenderer.color = color;
            if(color.a == 0f) {
                Destroy(this.gameObject);
            }
        }

    }

    // private void FixedUpdate() {
    //     if (!shouldMove) {
    //         Color color = this.spriteRenderer.color;
            // color.a = Mathf.Max(0, color.a - fadeSpeed * Time.fixedDeltaTime);          
    //         if (color.a <= 0) {
    //             Destroy(this.gameObject);
    //         }
    //         this.spriteRenderer.color = color;
    //     }    
    // }

}
