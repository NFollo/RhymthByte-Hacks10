using UnityEngine;

public class IntroCircle : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float delay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.localScale = new Vector3(0f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (delay <= 0) {

            this.transform.localScale += new Vector3( speed*Time.deltaTime, speed*Time.deltaTime, 1f);
            speed += acceleration * Time.deltaTime;

        } else {
            delay -= Time.deltaTime;
        }
    }
}
