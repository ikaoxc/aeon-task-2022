using System;
using UnityEngine;

public class MenuExit : MonoBehaviour
{
    [SerializeField] private Game _game;

    private void OnValidate()
    {
        if (_game == null)
            throw new NullReferenceException($"{gameObject.name} - Game - not implemented");
    }
    
    private void OnExit() => _game.BackMenu();
    
}
