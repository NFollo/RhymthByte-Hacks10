using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    [Header("Public Values: Don't Touch")]
    public float beatsShownInAdvance;
    public float secPerBeat;

    private float velocity;
    
    private void Start() {
        Vector3 startPosition = transform.position;
        Vector3 target = new Vector3(-4.5f, transform.position.y, transform.position.z);
        float difference = startPosition.x - target.x;
        float timeToReachTarget = beatsShownInAdvance * secPerBeat;
        velocity = difference/timeToReachTarget;

    }

    private void Update() {
        transform.position = new Vector3(transform.position.x - (velocity * Time.deltaTime), transform.position.y, transform.position.z);

        if(transform.position.x > -4.55 && transform.position.x < -4.45) {
            Debug.Log("Hit Zone");
        }

        if(transform.position.x < -13) {
            Destroy(this.gameObject);
        }
    }

}
