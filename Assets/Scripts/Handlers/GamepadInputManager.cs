using UnityEngine;

public class GamepadInputManager : MonoBehaviour
{
    // Gamepad input action map.
    private GamepadControls _rollingControls;

    /// <summary>
    /// Retrieve current state of analogue stick.
    /// </summary>
    public Vector2 AnalogueValue { get; private set; }

    /// <summary>
    /// Retrieve state of primary button.
    /// </summary>
    public bool PrimaryButtonValue { get; private set; }

    /// <summary>
    /// Retrieve state of secondary button.
    /// </summary>
    public bool SecondaryButtonValue { get; private set; }

    private void Awake()
    {
        _rollingControls = new GamepadControls();

        _rollingControls.gameplay.movement.performed += context => AnalogueValue = context.ReadValue<Vector2>();
        _rollingControls.gameplay.movement.canceled += context => AnalogueValue = Vector2.zero;

        _rollingControls.gameplay.button1Action.performed +=
            context => PrimaryButtonValue = context.ReadValue<float>() > 0.5f;
        _rollingControls.gameplay.button1Action.canceled +=
            context => PrimaryButtonValue = false;

        _rollingControls.gameplay.button2Action.performed +=
            context => SecondaryButtonValue = context.ReadValue<float>() > 0.5f;
        _rollingControls.gameplay.button2Action.canceled +=
            context => SecondaryButtonValue = false;
    }

    private void OnEnable()
    {
        _rollingControls?.Enable();
    }

    private void OnDisable()
    {
        _rollingControls?.Disable();
    }
}
