using System;
using UnityEngine;

[Serializable]
public class ScoreModel : MonoBehaviour
{
    private int _score = 0;
    
    public int Score
    {
        get => _score;
        set
        {
            if (_score < 0)
                throw new ArithmeticException();

            _score = value;
        }
    }
    
    public void Reset() => _score = 0;
}
