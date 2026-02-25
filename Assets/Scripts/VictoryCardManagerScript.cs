using UnityEngine;
using TMPro;

public class VictoryCardManagerScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI longestStreakText;
    [SerializeField] TextMeshProUGUI accuracyText;
    [SerializeField] TextMeshProUGUI missesText;

    private int _score;
    private int _highScore;
    private int _longestStreak;
    private float _accuracy;
    private int _misses;


    public void SendData(int score, int longestStreak, float accuracy, int misses)
    {
        _score = score;
        _longestStreak = longestStreak;
        _accuracy = accuracy;
        _misses = misses;
        UpdateInfo();
    }

    void UpdateInfo()
    {
        if (scoreText != null && _score != null)
        {
            scoreText.text = _score.ToString();
        }
        if (highScoreText != null && _highScore != null)
        {
            highScoreText.text = _highScore.ToString();
        }
        if (longestStreakText != null && _longestStreak != null)
        {
            longestStreakText.text = _longestStreak.ToString();
        }
        if (accuracyText != null && _accuracy != null)
        {
            accuracyText.text = _accuracy.ToString();
        }
        if (missesText != null && _misses != null)
        {
            missesText.text = _misses.ToString();
        }
        
    }
}
