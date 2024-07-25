using UnityEngine;

public class GameInput : MonoBehaviour
{
    public Vector2 GetInputMoveDirectionOfPlayer()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");
        return new Vector2(inputHorizontal, inputVertical).normalized;
    }
}
