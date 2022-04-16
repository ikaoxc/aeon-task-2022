using System;
using UnityEngine;

public class ResultScreenPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _gameResultLabel;
    [SerializeField] private GameObject _currentScoreLabel;
    
    private void OnValidate()
    {
        if (_gameResultLabel == null)
            throw new NullReferenceException();
        
        if (_currentScoreLabel == null)
            throw new NullReferenceException();
    }

    public void Present(bool isMenu, bool isWon)
    {
        _gameResultLabel.SetActive(isMenu == false);
        _currentScoreLabel.SetActive(isWon);
    }
}
