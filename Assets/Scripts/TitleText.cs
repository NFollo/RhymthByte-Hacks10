using UnityEngine;

public class TitleText : MonoBehaviour
{
    public float speed;
    public float delay;
    public float dist;
    public float var;
    public float varspeed;
    float x;
    float y;
    bool isDown;
    float varActual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        x = this.transform.position.x;
        y = this.transform.position.y;
        isDown = false;
        varActual = var;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        x = this.transform.position.x;
        y = this.transform.position.y;
        if (delay <= 0) {
            y = Mathf.Lerp(y, dist + varActual, speed * Time.deltaTime);
            this.transform.position = new Vector3(x, y, this.transform.position.z);
            if (isDown) {
                varActual += varspeed * Time.deltaTime;
                if (Mathf.Abs(varActual - var) < 0.1) {
                    isDown = false;
                }
            } else {
                varActual -= varspeed * Time.deltaTime;
                if (Mathf.Abs(varActual) < 0.1) {
                    isDown = true;
                }
            }
        } else {
            delay -= Time.deltaTime;
        }
    }
}
