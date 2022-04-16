using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private ResultScreen _resultScreen;
    [SerializeField] private Game _game;
    
    private void OnValidate()
    {
        if (_mainMenu == null)
            throw new NullReferenceException($"{gameObject.name} - Main Menu - not implemented");
        
        if (_resultScreen == null)
            throw new NullReferenceException($"{gameObject.name} - Result - not implemented");
        
        if (_game == null)
            throw new NullReferenceException($"{gameObject.name} - Game - not implemented");
    }
    
    private void OnEnable()
    {
        _game.GameStarted += OnGameStarted;
        _game.GameStopped += OnGameStopped;
        _game.GameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        _game.GameStarted -= OnGameStarted;
        _game.GameStopped -= OnGameStopped;
        _game.GameEnded -= OnGameEnded;
    }

    public void ShowLastResult() => ShowResultMenu(true, false);
    public void Play() => _game.StartGame();

    private void OnGameStarted() => HideMenu();
    private void OnGameStopped() => ShowMainMenu();
    private void OnGameEnded(bool isWon) => ShowResultMenu(false, isWon);

    private void ShowResultMenu(bool isMenu, bool isWon)
    {
        _mainMenu.Hide();
        _resultScreen.Show(isMenu, isWon);
    }

    private void ShowMainMenu()
    {
        _resultScreen.Hide();
        _mainMenu.Show();
    }

    private void HideMenu()
    {
        _resultScreen.Hide();
        _mainMenu.Hide();
    }
}
