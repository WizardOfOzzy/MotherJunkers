using System.Collections.Generic;
using UnityEngine;

public enum EControllerAxis
{
    LeftHorizontal,
    LeftVertical,
    RightHorizontal,
    RightVertical,
    LeftTrigger,
    RightTrigger,
    DPadHorizontal,
    DPadVertical,
}

public enum EControllerButton
{
    LeftDown,
    RightDown,
    LeftBumper,
    RightBumper,
    Button_X,
    Button_A,
    Button_B,
    Button_Y,
    Button_Back,
    Button_Start,
    Button_Register
}

public enum EController
{
    Controller1 = 1,
    Controller2 = 2,
    Controller3 = 3,
    Controller4 = 4,
    Controller5 = 5,
    Controller6 = 6,
    Controller7 = 7,
    Controller8 = 8
}

public class PlayerInput : Singleton<PlayerInput>
{
    private Dictionary<EControllerAxis, string> _axisLookup;
    private Dictionary<EControllerButton, string> _buttonLookup;

    private int _controllerCount = 8;

    public int ControllerCount { get { return _controllerCount; } set { _controllerCount = value; } }

    private void Awake()
    {
        _buttonLookup = new Dictionary<EControllerButton, string>()
        {
            {EControllerButton.LeftDown, "LeftDown_" },
            {EControllerButton.RightDown, "RightDown_" },
            
            {EControllerButton.LeftBumper, "LeftBumper_" },
            {EControllerButton.RightBumper, "RightBumper_" },
            {EControllerButton.Button_X, "ButtonX_" },
            {EControllerButton.Button_A, "ButtonA_" },
            {EControllerButton.Button_B, "ButtonB_" },
            {EControllerButton.Button_Y, "ButtonY_" },
            {EControllerButton.Button_Start, "ButtonStart_" },
            {EControllerButton.Button_Back, "ButtonBack_" },
            {EControllerButton.Button_Register, "ButtonRegister_" }
        };
        _axisLookup = new Dictionary<EControllerAxis, string>()
        {
            {EControllerAxis.LeftHorizontal, "LeftHorizontal_" },
            {EControllerAxis.LeftVertical, "LeftVertical_" },
            {EControllerAxis.RightHorizontal, "RightHorizontal_" },
            {EControllerAxis.RightVertical, "RightVertical_" },
            {EControllerAxis.LeftTrigger, "LeftTrigger_" },
            {EControllerAxis.RightTrigger, "RightTrigger_" },
            {EControllerAxis.DPadHorizontal, "DPadHorizontal_" },
            {EControllerAxis.DPadVertical, "DPadVertical_" },
        };
    }

    public float GetAxis(EController controller, EControllerAxis axis)
    {
        return Input.GetAxisRaw(_axisLookup[axis] + (int)controller);
    }

    public bool GetButtonDown(EController controller, EControllerButton button)
    {
        return Input.GetButtonDown(_buttonLookup[button] + (int)controller);
    }

    public bool GetButtonUp(EController controller, EControllerButton button)
    {
        return Input.GetButtonUp(_buttonLookup[button] + (int)controller);
    }

    public bool GetButton(EController controller, EControllerButton button)
    {
        return Input.GetButton(_buttonLookup[button] + (int)controller);
    }
}