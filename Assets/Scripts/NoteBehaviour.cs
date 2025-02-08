using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    [Header("Public Values: Don't Touch")]
    public float beatsShownInAdvance;
    public float secPerBeat;

    private float velocity;

    public float fadeSpeed;
    bool isAlive;
    private SpriteRenderer spriteRenderer; 

    private void Start() {
        Vector3 startPosition = transform.position;
        Vector3 target = new Vector3(-4.5f, transform.position.y, transform.position.z);
        float difference = startPosition.x - target.x;
        float timeToReachTarget = beatsShownInAdvance * secPerBeat;
        velocity = difference/timeToReachTarget;
        isAlive = true;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    /*
    * x values for hits:
    * -4.8 to -4.2 = PERFECT
    * -5 to -4 = GOOD
    * -5.5 to -3.5 = OKAY    
    */
    private void Update() { 

        if (isAlive) {
            transform.position = new Vector3(transform.position.x - (velocity * Time.deltaTime), transform.position.y, transform.position.z);
        }

        if(transform.position.x > -4.55 && transform.position.x < -4.45) {
            Debug.Log("Hit Zone");
        }

        if(transform.position.x < -13) {
            Destroy(this.gameObject);
        }

        //Hit Zones
        if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y > 0) {
            if (transform.position.x > -4.8 && transform.position.x < -4.2) {
                Debug.Log("PERFECT");
                isAlive = false;
            } else if (transform.position.x > -5 && transform.position.x < -4) {
                Debug.Log("GOOD");
                isAlive = false;
            } else if (transform.position.x > -5.5 && transform.position.x < -3.5) {
                Debug.Log("OKAY");
                isAlive = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y < 0) {
            if (transform.position.x > -4.8 && transform.position.x < -4.2) {
                Debug.Log("PERFECT");
                isAlive = false;
            } else if (transform.position.x > -5 && transform.position.x < -4) {
                Debug.Log("GOOD");
                isAlive = false;
            } else if (transform.position.x > -5.5 && transform.position.x < -3.5) {
                Debug.Log("OKAY");
                isAlive = false;
            }
        }
    }

    private void FixedUpdate() {
        if (!isAlive) {
            Color color = this.spriteRenderer.color;
            color.a = Mathf.Max(0, color.a - fadeSpeed * Time.deltaTime);          
            if (color.a <= 0) {
                Destroy(this.gameObject);
            }
            this.spriteRenderer.color = color;
        }    
    }

}
