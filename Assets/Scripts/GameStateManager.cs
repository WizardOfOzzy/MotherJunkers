using System;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public enum EGameState
    {
        MainMenu,
        JoinScreen,
        PlayScreen,
        WinScreen
    }
    public GameObject MainMenuUI;
    public GameObject JoinScreenUI;
    public GameObject PlayerUI;
    public GameObject PlayerWinsUI;

    public EGameState GameState = EGameState.MainMenu;


    private void Start()
    {
        GameState = EGameState.MainMenu;
        MainMenuUI.gameObject.SetActive(true);
        JoinScreenUI.gameObject.SetActive(false);
        PlayerUI.gameObject.SetActive(false);
        PlayerWinsUI.gameObject.SetActive(false);

        Publisher.Subscribe<PlayerCountEvent>(OnPlayerCountEvent);
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
            case EGameState.WinScreen:
                WinScreenState();
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

        if (PlayerManager.Players.Count > 1)
        {
            if (CheckForStart())
            {
                PlayerManager.SpawnAllPlayers();
                JoinScreenUI.gameObject.SetActive(false);
                GameState = EGameState.PlayScreen;
                PlayerUI.gameObject.SetActive(true);
            } 
        }
    }

    private void PlayScreenState()
    {

    }

    private void WinScreenState()
    {
        if (CheckForStart())
        {
            JoinScreenUI.GetComponent<PlayerJoinUI>().ResetScreen();
            PlayerManager.ClearPlayers();
            MainMenuUI.gameObject.SetActive(true);
            PlayerWinsUI.gameObject.SetActive(false);
            GameState = EGameState.MainMenu;
        }
    }

    private void CheckForPlayerJoin()
    {
        for (int i = 1; i <= 4; i++)
        {
            EController player = (EController) i;
            if (PlayerInput.Instance.GetButtonUp(player, EControllerButton.Button_A))
            {
                PlayerManager.AddPlayer(player);
            }

            if (PlayerInput.Instance.GetButtonUp(player, EControllerButton.Button_B))
            {
                PlayerManager.RemovePlayer(player);
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

    private void OnPlayerCountEvent(PlayerCountEvent e)
    {
        if (e.PlayerCount == 1)
        {
            PlayerWinsUI.gameObject.SetActive(true);

            Text text = PlayerWinsUI.GetComponentInChildren<Text>();
            text.text = "Player " + (int) PlayerManager.Players[0].controller + " Wins!";
            text.color = PlayerManager.GetPlayerColor(PlayerManager.Players[0].controller);

            GameState = EGameState.WinScreen;
        }
    }
}
