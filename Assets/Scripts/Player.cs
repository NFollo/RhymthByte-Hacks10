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
    public ColorIndicators colorIndicatorTop;
    public ColorIndicators colorIndicatorBottom;

    bool paused = false;

    private void Awake() {
        conductor = GetComponent<Conductor>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Top"].performed += HitTop; 
        playerInput.actions["Bottom"].performed += HitBottom; 
        playerInput.actions["Pause"].performed += Pause;
    }

    private void Pause(InputAction.CallbackContext ctx) {
        paused = !paused;
        if(paused) {
            Time.timeScale = 0f;
            AudioListener.pause = true;
        } else {
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }
    }

    private void OnDisable() {
        playerInput.actions["Top"].performed -= HitTop; 
        playerInput.actions["Bottom"].performed -= HitBottom; 
    }

    private void HitTop(InputAction.CallbackContext ctx) {
        if(ctx.ReadValueAsButton() && !paused) {
            conductor.actOnTop(conductor.songPosition);
            colorIndicatorTop.show();
        } else if (!ctx.ReadValueAsButton()) {
            colorIndicatorTop.hide();
        }
    }

    private void HitBottom(InputAction.CallbackContext ctx) {
        if(ctx.ReadValueAsButton() && !paused) {
            conductor.actOnBottom(conductor.songPosition);
            colorIndicatorBottom.show();
        } else if (!ctx.ReadValueAsButton()) {
            colorIndicatorBottom.hide();
        }
    }
}
