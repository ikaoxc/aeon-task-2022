using System;
using UnityEngine;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private GameObject _resultScreen;
    
    [SerializeField] private ScoreModel _currentScore;
    [SerializeField] private ScoreModel _previousScore;
    
    [SerializeField] private GameResultPresenter _gameResultPresenter;
    [SerializeField] private ResultScreenPresenter _resultScreenPresenter;
    [SerializeField] private ScorePresenter _currentScorePresenter;
    [SerializeField] private ScorePresenter _previousScorePresenter;

    private void OnValidate()
    {
        if (_currentScore == null)
            throw new NullReferenceException($"{gameObject.name} - Current Score - not implemented");
        
        if (_previousScore == null)
            throw new NullReferenceException($"{gameObject.name} - Previous Score - not implemented");
        
        if (_gameResultPresenter == null)
            throw new NullReferenceException($"{gameObject.name} - Game Result Presenter - not implemented");
        
        if (_resultScreenPresenter == null)
            throw new NullReferenceException($"{gameObject.name} - Result Screen Presenter - not implemented");
        
        if (_currentScorePresenter == null)
            throw new NullReferenceException($"{gameObject.name} - Current Score Presenter - not implemented");
        
        if (_previousScorePresenter == null)
            throw new NullReferenceException($"{gameObject.name} - Previous Score Presenter - not implemented");
    }
    
    public void Show(bool isMenu, bool isWon)
    {
        _resultScreen.SetActive(true);
        
        _gameResultPresenter.Present(isWon);
        _resultScreenPresenter.Present(isMenu, isWon);
        _currentScorePresenter.Present(_currentScore.Score);
        _previousScorePresenter.Present(_previousScore.Score);
    }

    public void Hide()
    {
        _resultScreen.SetActive(false);
    }
}
