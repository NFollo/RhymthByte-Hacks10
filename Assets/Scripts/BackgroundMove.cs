using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float maxX;
    public float maxY;
    public float t;
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        mousePos.x = Mathf.Lerp(this.transform.position.x, Mathf.Clamp(mousePos.x * -0.03f, -1f * maxX, maxX), t * Time.deltaTime);
        mousePos.y = Mathf.Lerp(this.transform.position.y, Mathf.Clamp(mousePos.y * -0.03f, -1f * maxY, maxY), t * Time.deltaTime);
        this.transform.position = mousePos;
    }
}
