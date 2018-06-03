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
        CheckForPlayerJoin();

        if (_playerManager.Players.Count > 1)
        {
            if (CheckForStart())
            {
                _playerManager.SpawnAllPlayers();
                JoinScreenUI.gameObject.SetActive(false);
                GameState = EGameState.PlayScreen;
                PlayerUI.gameObject.SetActive(true);
            } 
        }
    }

    private void PlayScreenState()
    {
        //Check for when someone wins
    }

    private void CheckForPlayerJoin()
    {
        if (_playerManager == null)
        {
            _playerManager = FindObjectOfType<PlayerManager>();
            _playerManager.enabled = true;
        }

        for (int i = 1; i <= 4; i++)
        {
            EController player = (EController)i;
            if (PlayerInput.Instance.GetButtonUp(player, EControllerButton.Button_A))
            {
                bool shouldAdd = true;
                for (int j = 0; j < _playerManager.Players.Count; j++)
                {
                    if (_playerManager.Players[j].controller == player)
                    {
                        shouldAdd = false;
                        break;
                    }
                }

                if (shouldAdd)
                {
                    _playerManager.AddPlayer(player, GetPlayerColor(player));
                }
            }
        }

    }

    private Color GetPlayerColor(EController player)
    {
        switch (player)
        {
            case EController.Controller1:
                return Color.red;
            case EController.Controller2:
                return Color.green;
            case EController.Controller3:
                return Color.blue;
            default:
                return Color.yellow;
        }
    }

    private PlayerManager _playerManager;

    private bool CheckForStart()
    {
        bool didStart = false;
        for (int i = 1; i <= 4; i++)
        {
            if (PlayerInput.Instance.GetButtonUp((EController)i, EControllerButton.Button_Start))
            {
                didStart =  true;
                break;
            }
        }
        return didStart;
    }
}
