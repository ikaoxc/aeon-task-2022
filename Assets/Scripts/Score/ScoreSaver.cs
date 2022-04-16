using System;
using UnityEngine;

public class ScoreSaver : MonoBehaviour
{
    [SerializeField] private ScoreModel _currentScore;
    [SerializeField] private ScoreModel _previousScore;
    
    private void OnValidate()
    {
        if (_currentScore == null)
            throw new NullReferenceException($"{gameObject.name} - Current Score - not implemented");
        
        if (_previousScore == null)
            throw new NullReferenceException($"{gameObject.name} - Previous Score - not implemented");
    }
    
    public void Save()
    {
        _previousScore.Score = _currentScore.Score;
    }

}
