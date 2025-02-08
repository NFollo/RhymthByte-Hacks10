using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float maxX;
    public float maxY;
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        mousePos.x = Mathf.Clamp(mousePos.x * -0.03f, -1f * maxX, maxX);
        mousePos.y = Mathf.Clamp(mousePos.y * -0.03f, -1f * maxY, maxY);
        this.transform.position = mousePos;
    }
}
