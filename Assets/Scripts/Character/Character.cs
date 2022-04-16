using System;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(CharacterPositionSaver))]
[RequireComponent(typeof(CharacterMovement))]
public class Character : MonoBehaviour
{
    [SerializeField] private Game _game;
    
    private CharacterPositionSaver _characterPositionSaver;
    private CharacterMovement _characterMovement;
    
    private Transform _transform;
    
    public Action Dead;

    private void OnValidate()
    {
        if (_game == null)
            throw new NullReferenceException($"{gameObject.name} - Game - not implemented");
    }

    private void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
        _characterPositionSaver = gameObject.GetComponent<CharacterPositionSaver>();
        _characterMovement = gameObject.GetComponent<CharacterMovement>();
    }

    private void Start()
    {
        _characterPositionSaver.Save();
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

    private void Update()
    {
        if (_transform.position.y < -2)
            Dead?.Invoke();
    }

    private void Reset()
    {
        _characterPositionSaver.Reset();
        _characterMovement.Enable();
    }

    private void OnGameStarted() => Reset();
    private void OnGameStopped() => _characterMovement.Disable();
    private void OnGameEnded(bool isWon) => _characterMovement.Disable();
}
