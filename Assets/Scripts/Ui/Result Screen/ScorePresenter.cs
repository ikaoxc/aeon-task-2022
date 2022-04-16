using System;
using UnityEngine;
using UnityEngine.UI;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField] private Text _scoreLabel;
    
    private void OnValidate()
    {
        if (_scoreLabel == null)
            throw new NullReferenceException();
    }

    public void Present(int score)
    {
        _scoreLabel.text = score > 0 ? $"{score} s" : $"no last score";
    }
}
