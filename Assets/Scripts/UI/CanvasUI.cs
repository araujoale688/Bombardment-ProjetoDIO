using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    private static readonly int SCORE_FACTOR = 10;

    [SerializeField]
    private TextMeshProUGUI scoreLabel;
    [SerializeField]
    private TextMeshProUGUI highestScoreLabel;

    void Start()
    {
        scoreLabel.text = GetScoreString();
        highestScoreLabel.text = GetHighstScoreString();
    }

    void Update()
    {
        scoreLabel.text = GetScoreString();
    }

    private string GetScoreString()
    {
        return (GameManager.Instance.GetScore() * SCORE_FACTOR).ToString();
    }

    private string GetHighstScoreString()
    {
        return (GameManager.Instance.GetHighstScore() * SCORE_FACTOR).ToString();
    }
}
