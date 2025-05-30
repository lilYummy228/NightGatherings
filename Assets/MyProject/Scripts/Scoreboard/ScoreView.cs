using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable() => 
        _scoreCounter.ScoreChanged += ViewScore;

    private void OnDisable() => 
        _scoreCounter.ScoreChanged -= ViewScore;

    private void ViewScore(int score) => 
        _text.text = score.ToString();
}
