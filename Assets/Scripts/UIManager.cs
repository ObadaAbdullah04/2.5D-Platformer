using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText, _livesText;

    void Start()
    {
        if (_scoreText == null)
        {
            Debug.LogError("Score Text is not assigned in the UIManager.");
        }
        if (_livesText == null)
        {
            Debug.LogError("Lives Text is not assigned in the UIManager.");
        }
    }
    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score.ToString();
    }
    public void UpdateLives(int lives)
    {
        _livesText.text = "Lives: " + lives.ToString();
    }
}
