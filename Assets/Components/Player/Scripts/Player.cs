using UnityEngine;

public class Player : MonoBehaviour
{
    protected InputSystem_Actions inputActions;

    protected virtual void OnEnable()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
    }

    protected virtual void OnDisable()
    {
        inputActions.Player.Disable();
    }
}
