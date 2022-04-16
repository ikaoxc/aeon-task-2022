using System;
using System.Collections;
using UnityEngine;

public class ScoreTimer : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private ScoreModel _score;

    private Coroutine _coroutine;
    private bool _isStarted;

    private void OnValidate()
    {
        if (_game == null)
            throw new NullReferenceException($"{gameObject.name} - Game - not implemented");
        
        if (_score == null)
            throw new NullReferenceException($"{gameObject.name} - Current Score - not implemented");
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

    private IEnumerator Stopwatch()
    {
        while (_isStarted)
        {
            _score.Score++;
            yield return new WaitForSecondsRealtime(1);
        }
    }

    private void StopStopwatch()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        
        _isStarted = false;
    }

    private void StartStopwatch()
    {
        if (_coroutine != null)
            StopStopwatch();
        
        _score.Reset();
        _isStarted = true;
        
        _coroutine = StartCoroutine(Stopwatch());
    }
    
    private void OnGameStarted() => StartStopwatch();
    private void OnGameStopped() => StopStopwatch();
    private void OnGameEnded(bool isWon) => StopStopwatch();
}
