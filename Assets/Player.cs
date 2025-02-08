using UnityEngine;

/**
 * This script is for when the player is actually playing the game.
 * It includes all the checking for the notes and the actual gameplay as well.
 */
 
public class Player : MonoBehaviour
{
    [Header("Accuracy")]


    private Conductor conductor;
    private void Start() {
        conductor = GetComponent<Conductor>();
    }

    void OnTop() {
        
    }

    void OnBottom() {

    }
}
