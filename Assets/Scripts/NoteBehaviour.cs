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
    public float beatsShownInAdvance;
    public float secPerBeat;
    public bool isTop;

    private float velocity;
    public GameObject prevNote;
    bool isAlive;


    private void Start() {
        Vector3 startPosition = transform.position;
        Vector3 target = new Vector3(-4.5f, transform.position.y, transform.position.z);
        float difference = startPosition.x - target.x;
        float timeToReachTarget = beatsShownInAdvance * secPerBeat;
        velocity = difference/timeToReachTarget;
        isAlive = true;
        mytext = "";
        isFakeAlive = false;
        //dyingNotePrefab = null;
        //spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    /*
    * x values for hits:
    * -4.8 to -4.2 = PERFECT
    * -5 to -4 = GOOD
    * -5.5 to -3.5 = OKAY    
    */
    private void Update() { 

        if (isAlive || isFakeAlive) {
            transform.position = new Vector3(transform.position.x - (velocity * Time.deltaTime), transform.position.y, transform.position.z);

            // -------------- TEMPORARY FIX --------------

            if(transform.position.x < -5.5) {
                isAlive = false;
                isFakeAlive = true;
            }

            // -------------------------------------------

        }

        // if(transform.position.x > -4.55 && transform.position.x < -4.45) {
        //     Debug.Log("Hit Zone");
        // }

        if(transform.position.x < -13) {
            Destroy(this.gameObject);
        }

        //Hit Zones
        if (prevNote == null) {
            if (Input.GetKeyDown(KeyCode.F) && isTop) {
                if (transform.position.x > -4.8 && transform.position.x < -4.2) {
                    //Debug.Log("PERFECT");
                    mytext = "PERFECT!";
                    isAlive = false;
                } else if (transform.position.x > -5 && transform.position.x < -4) {
                    //Debug.Log("GOOD");
                    mytext = "GOOD";
                    isAlive = false;
                } else if (transform.position.x > -5.5 && transform.position.x < -3.5) {
                    //Debug.Log("OKAY");
                    mytext = "OKAY";
                    isAlive = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.J) && !isTop) {
                if (transform.position.x > -4.8 && transform.position.x < -4.2) {
                    //Debug.Log("PERFECT");
                    mytext = "PERFECT!";
                    isAlive = false;
                } else if (transform.position.x > -5 && transform.position.x < -4) {
                    //Debug.Log("GOOD");
                    mytext = "GOOD";
                    isAlive = false;
                } else if (transform.position.x > -5.5 && transform.position.x < -3.5) {
                    //Debug.Log("OKAY");
                    mytext = "OKAY"; 
                    isAlive = false;
                }
            } 
        } else {
            //Debug.Log(prevNote);
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
