using System;
using MotherJunkers;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum EGameState
    {
        MainMenu,
        JoinScreen,
        PlayScreen
    }
    public GameObject MainMenuUI;
    public GameObject JoinScreenUI;
    public GameObject PlayerUI;

    public EGameState GameState = EGameState.MainMenu;

    private void Start()
    {
        GameState = EGameState.MainMenu;
        MainMenuUI.gameObject.SetActive(true);
        JoinScreenUI.gameObject.SetActive(false);
        PlayerUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        switch (GameState)
        {
            case EGameState.MainMenu:
                MainMenuState();
                break;
            case EGameState.JoinScreen:
                JoinScreenState();
                break;
            case EGameState.PlayScreen:
                PlayScreenState();
                break;
        }
    }

    private void MainMenuState()
    {
        if (CheckForStart())
        {
            MainMenuUI.gameObject.SetActive(false);
            GameState = EGameState.JoinScreen;
            JoinScreenUI.gameObject.SetActive(true);
        }
    }

    private void JoinScreenState()
    {
        //Check for when enough players join
    }

    private void PlayScreenState()
    {
        //Check for when someone wins
    }

    private bool CheckForStart()
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlayerInput.Instance.GetButtonUp((EController)i, EControllerButton.Button_Start))
            {
                return true;
            }
        }
        return false;
    }
}
