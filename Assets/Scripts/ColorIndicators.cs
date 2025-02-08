using UnityEngine;

public class ColorIndicators : MonoBehaviour
{
    public float speed;
    public float hold;
    public bool isTop;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.localScale = new Vector3(0f, 0f, 1f);
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isTop) {
            this.transform.localScale = new Vector3(0.23f, 0.23f, 1f);
            timer = hold;
        }
        if (Input.GetKeyDown(KeyCode.J) && !isTop) {
            this.transform.localScale = new Vector3(0.23f, 0.23f, 1f);
            timer = hold;
        }
    }

    void FixedUpdate() {
        if (timer > 0) {
            timer = Mathf.Max(timer - Time.deltaTime, 0);
        } else if (this.transform.localScale.x >= 0.2) {
            this.transform.localScale -= new Vector3(speed*Time.fixedDeltaTime, speed*Time.fixedDeltaTime, 0f);
        } else if (this.transform.localScale.x != 0 && ( (!Input.GetKey(KeyCode.UpArrow) && isTop) || (!Input.GetKey(KeyCode.DownArrow) && !isTop) ) ) {
            this.transform.localScale = new Vector3(0f, 0f, 1f);
        }  
    }

}
