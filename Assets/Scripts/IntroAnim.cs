using UnityEngine;

public class IntroAnim : MonoBehaviour
{
    public float speed;
    public float delay;
    public int direction;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (delay <= 0) {
            this.transform.position += new Vector3(direction%2 == 1 ? (direction == 1 ? 1f : -1f) * speed * Time.deltaTime : 0f, direction%2 == 0 ? (direction == 0 ? 1f : -1f) * speed * Time.deltaTime : 0f, 0f);
            if (Mathf.Abs(this.transform.position.x) >= 18f || Mathf.Abs(this.transform.position.y) >= 8.4f) {
                Destroy(this.gameObject);
            }
        } else {
            delay -= Time.deltaTime;
        }
    }
}
