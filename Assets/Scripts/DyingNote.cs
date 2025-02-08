using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DyingNote : MonoBehaviour
{
    public float fadeSpeed;
    public GameObject text;
    private SpriteRenderer spriteRenderer; 
    private TextMeshProUGUI textMeshPro;
    public string mytext;
    public bool isFakeAlive;
    public float velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = mytext;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isFakeAlive) {
            Color color = this.spriteRenderer.color;
            color.a = Mathf.Max(0, color.a - fadeSpeed * Time.deltaTime);          
            if (color.a <= 0) {
                Destroy(this.gameObject);
            }
            this.spriteRenderer.color = color;
        } else {
            transform.position = new Vector3(transform.position.x - (velocity * Time.deltaTime), transform.position.y, transform.position.z);
            if(transform.position.x < -13) {
            Destroy(this.gameObject);
            }  
        }
    }
}
