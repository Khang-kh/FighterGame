using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ScoreController : MonoBehaviour
{
    [SerializeField] TMP_Text ScoreText;
    
    private int score;
    public static ScoreController Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void GetScore(int score)
    {
        this.score += score;
        ScoreText.text = "Score: " + this.score.ToString();
    }
    void Start()
    {

    }

    void Update()
    {

    }

    public void ResetScore()
    {
        score = 0;
    }
}
