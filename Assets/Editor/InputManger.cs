using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InputManger : Editor
{
    private const string XBONE_INPUT_WIN = "xbone_win.yaml";
    private const string XBONE_INPUT_MAC = "xbone_mac.yaml";

    [MenuItem("HE Tools/Input/Xbox One Win")]
    public static void XboneWin()
    {
        SwapInputManager(XBONE_INPUT_WIN);
    }

    [MenuItem("HE Tools/Input/Xbox One Mac")]
    public static void XboneMac()
    {
        SwapInputManager(XBONE_INPUT_MAC);
    }

    public static void SwapInputManager(string inputString)
    {
        Debug.Log("Swapping input: " + inputString);

        Object obj = Resources.Load(inputString);
        Debug.Log(obj.name);
    }
}
