using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VictoryCardManagerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI loseText;
    [SerializeField] Image trinketUI;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI longestStreakText;
    [SerializeField] TextMeshProUGUI accuracyText;
    [SerializeField] TextMeshProUGUI missesText;

    [SerializeField] Image badge1;
    [SerializeField] Image badge2;
    [SerializeField] Image badge3;

    private bool _win = false;
    private Sprite _trinketSprite;
    private int _score = 0;
    private int _highScore = 0;
    private int _longestStreak = 0;
    private float _accuracy = 0f;
    private int _misses = 0;
    private bool _hasPendingResult = false;


    public void SendData(bool win, Sprite trinketSprite, int score, int highScore, int longestStreak, float accuracy, int misses)
    {
        _win = win;
        _score = score;
        _trinketSprite = trinketSprite;
        _highScore = highScore;
        _longestStreak = longestStreak;
        _accuracy = accuracy;
        _misses = misses;
        UpdateInfo();
    }

    void OnDisable()
    {
        MusicManagerScript musicManager = FindObjectOfType<MusicManagerScript>();
        if (musicManager != null)
        {
            musicManager.OnVictoryCardClosed();
        }
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
        if (trinketUI != null && _trinketSprite != null && _win)
        {
            trinketUI.gameObject.SetActive(true);
            trinketUI.sprite = _trinketSprite;
        }
        else
        {
            trinketUI.gameObject.SetActive(false);
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
            accuracyText.text = ((int)(_accuracy * 100)).ToString() + "%";
        }
        if (missesText != null && _misses != null)
        {
            missesText.text = _misses.ToString();
        }
        if (badge1 != null) {
            if (_win)
            {
                badge1.gameObject.SetActive(true);
            }
            else
            {
                badge1.gameObject.SetActive(false);
            }
        }
        if (badge2 != null) {
            if (_misses <= 0 && _win)
            {
                badge2.gameObject.SetActive(true);
            }
            else
            {
                badge2.gameObject.SetActive(false);
            }
        }
        if (badge3 != null) {
            if (_accuracy >= 1 && _win)
            {
                badge3.gameObject.SetActive(true);
            }
            else
            {
                badge3.gameObject.SetActive(false);
            }
        }
    }
}
