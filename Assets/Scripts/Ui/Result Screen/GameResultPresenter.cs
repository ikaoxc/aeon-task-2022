using System;
using UnityEngine;
using UnityEngine.UI;

public class GameResultPresenter : MonoBehaviour
{
    private const string _gameWinnedTitle = "You Win!";
    private const string _gameLoosedTitle = "You Lose!";

    [SerializeField] private Text _resultLabel;

    private void OnValidate()
    {
        if (_resultLabel == null)
            throw new NullReferenceException();
    }

    public void Present(bool isWon)
    {
        _resultLabel.text = isWon ? _gameWinnedTitle : _gameLoosedTitle;
    }
}
