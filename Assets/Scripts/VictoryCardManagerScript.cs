using UnityEngine;
using TMPro;

public class VictoryCardManagerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI loseText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI longestStreakText;
    [SerializeField] TextMeshProUGUI accuracyText;
    [SerializeField] TextMeshProUGUI missesText;

    private bool _win = false;
    private int _score = 0;
    private int _highScore = 0;
    private int _longestStreak = 0;
    private float _accuracy = 0f;
    private int _misses = 0;


    public void SendData(bool win, int score, int longestStreak, float accuracy, int misses)
    {
        _win = win;
        _score = score;
        _longestStreak = longestStreak;
        _accuracy = accuracy;
        _misses = misses;
        UpdateInfo();
    }

    void UpdateInfo()
    {
        if (winText != null && loseText != null && _win != null)
        {
            if (_win) {
                winText.gameObject.SetActive(true);
                loseText.gameObject.SetActive(false);
            }
            else {
                winText.gameObject.SetActive(false);
                loseText.gameObject.SetActive(true);
            }
        }
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
