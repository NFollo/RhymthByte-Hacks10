using UnityEngine;

public class DyingNote : MonoBehaviour
{
    public float fadeSpeed;
    private SpriteRenderer spriteRenderer; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Color color = this.spriteRenderer.color;
        color.a = Mathf.Max(0, color.a - fadeSpeed * Time.deltaTime);          
        if (color.a <= 0) {
            Destroy(this.gameObject);
        }
        this.spriteRenderer.color = color;
    }
}
