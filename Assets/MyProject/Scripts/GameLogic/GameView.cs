using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameView : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private CanvasGroup _tutorial;
    [SerializeField] private AmbientSoundPlayer _ambientSoundPlayer;
    [SerializeField] private Transform _gameFinishedPanel;
    [SerializeField] private Transform _newRecordPanel;
    [SerializeField] private Timer _timer;
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _score;

    private WaitUntil _waitUntil;
    private Vector3 _punchVector = Vector3.one * 2;
    private float _showStatsDelay = 2f;
    private float _fadeTime = 1.5f;
    private float _punchDuration = 2f;
    private float _punchElasticity = 0f;
    private int _punchVibrato = 6;

    private void OnEnable()
    {
        _waitUntil = new WaitUntil(() => Input.GetMouseButtonDown(0));

        _game.GameFinished += ShowContinueScreen;
        _game.TutorialStarted += StartTutorial;
    }

    private void OnDisable()
    {
        _game.GameFinished -= ShowContinueScreen;
        _game.TutorialStarted -= StartTutorial;
    }

    private void StartTutorial() =>
        StartCoroutine(ShowTutorial());

    private IEnumerator ShowTutorial()
    {
        _tutorial.gameObject.SetActive(true);

        yield return _waitUntil;

        _tutorial.DOFade(default, _fadeTime);

        _game.StartGame();
    }

    private void ShowContinueScreen()
    {
        _ambientSoundPlayer.gameObject.SetActive(false);

        Invoke(nameof(ShowGameStatistics), _showStatsDelay);
    }

    private void ShowGameStatistics()
    {
        _gameFinishedPanel.gameObject.SetActive(true);
        _time.text = $"{_timer.Minutes}:{_timer.Seconds}";
        _score.text = _game.ScoreSaveSystem.ScoreCounter.Score.ToString();

        if (_game.ScoreSaveSystem.IsNewRecord)
            ShowNewRecord();
    }

    private void ShowNewRecord()
    {
        _newRecordPanel.gameObject.SetActive(true);
        _newRecordPanel.DOPunchScale(_punchVector, _punchDuration, _punchVibrato, _punchElasticity);
    }
}
