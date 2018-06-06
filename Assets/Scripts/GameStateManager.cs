using System;
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
    private PlayerManager _playerManager;

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
            EController player = (EController) i;
            if (PlayerInput.Instance.GetButtonUp(player, EControllerButton.Button_A))
            {
                _playerManager.AddPlayer(player);
            }

            if (PlayerInput.Instance.GetButtonUp(player, EControllerButton.Button_B))
            {
                _playerManager.RemovePlayer(player);
            }
        }
    }


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
