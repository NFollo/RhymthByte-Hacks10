using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This script is for when the player is actually playing the game.
 * It includes all the checking for the notes and the actual gameplay as well.
 */

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    private Conductor conductor;

    private void Awake() {
        conductor = GetComponent<Conductor>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Top"].performed += HitTop; 
        playerInput.actions["Bottom"].performed += HitBottom; 
    }

    private void OnDisable() {
        playerInput.actions["Top"].performed -= HitTop; 
        playerInput.actions["Bottom"].performed -= HitBottom; 
    }

    private void HitTop(InputAction.CallbackContext ctx) {
        if(ctx.ReadValueAsButton()) {
            conductor.actOnTop(conductor.songPosition);
        } else if (!ctx.ReadValueAsButton()) {
        }
    }

    private void HitBottom(InputAction.CallbackContext ctx) {
        if(ctx.ReadValueAsButton()) {
            conductor.actOnBottom(conductor.songPosition);
        } else if (!ctx.ReadValueAsButton()) {
        }
    }
}
