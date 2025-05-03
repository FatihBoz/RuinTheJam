using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected InputSystem_Actions inputActions;

    [Header("LOOP ICONS")]
    [SerializeField] protected LoopIcon swordIcon;
    [SerializeField] protected LoopIcon jumpIcon;
    [SerializeField] protected LoopIcon dashIcon;

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
