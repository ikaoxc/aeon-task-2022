using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private ScoreSaver _saver;
    [SerializeField] private Character _character;
    [SerializeField] private Waypoint _waypoint;

    private bool _isStarted;
    
    public Action GameStarted;
    public Action GameStopped;
    public Action<bool> GameEnded;
    
    private void OnValidate()
    {
        if (_saver == null)
            throw new NullReferenceException($"{gameObject.name} - Saver - not implemented");
        
        if (_character == null)
            throw new NullReferenceException($"{gameObject.name} - Character - not implemented");
        
        if (_waypoint == null)
            throw new NullReferenceException($"{gameObject.name} - Waypoint - not implemented");

    }

    private void OnEnable()
    {
        _character.Dead += OnCharacterDead;
        _waypoint.Reached += OnWaypointReached;
    }

    private void OnDisable()
    {
        _character.Dead -= OnCharacterDead;
        _waypoint.Reached -= OnWaypointReached;
    }

    public void BackMenu()
    {
        GameStopped?.Invoke();
    }

    public void StartGame()
    {
        if (_isStarted == true)
            return;
        
        GameStarted?.Invoke();

        _isStarted = true;
    }
    
    private void EndGame(bool isWon)
    {
        if (_isStarted == false)
            return;
        
        GameEnded?.Invoke(isWon);
        
        if (isWon == true)
            _saver.Save();

        _isStarted = false;
    }

    private void OnWaypointReached() => EndGame(true);
    private void OnCharacterDead() => EndGame(false);
}
