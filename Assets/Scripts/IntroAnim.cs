using UnityEngine;

public class IntroAnim : MonoBehaviour
{
    public float speed;
    public float delay;
    public int direction
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += new Vector3((direction == speed * Time.deltaTime), , 0f);
    }
}
