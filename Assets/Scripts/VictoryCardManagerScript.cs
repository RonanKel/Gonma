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
        if (missesText != null)
        {
            missesText.text = _misses.ToString();
        }
    }
}
