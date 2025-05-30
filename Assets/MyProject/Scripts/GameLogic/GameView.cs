using System.Collections;
using UnityEngine;
using DG.Tweening;
using YG;
using TMPro;

public class GameView : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private CanvasGroup _tutorial;
    [SerializeField] private AmbientSoundPlayer _ambientSoundPlayer;
    [SerializeField] private Transform _gameFinishedPanel;
    [SerializeField] private TextMeshProUGUI _timer;

    private float _showStatsDelay = 2f;
    private float _fadeTime = 1.5f;
    private WaitUntil _waitUntil;

    private void OnEnable()
    {
        _waitUntil = new WaitUntil(() => Input.GetMouseButtonDown(0));

        _game.GameStarted += Count;
        _game.GameFinished += FinishGame;
        _game.TutorialStarted += StartTutorial;
    }    

    private void OnDisable()
    {
        _game.GameStarted -= Count;
        _game.GameFinished -= FinishGame;
        _game.TutorialStarted -= StartTutorial;
    }

    private void Count()
    {
        //timer
    }

    private void StartTutorial() =>
        StartCoroutine(ShowTutorial());

    private IEnumerator ShowTutorial()
    {
        _tutorial.gameObject.SetActive(true);

        yield return _waitUntil;

        _tutorial.DOFade(default, _fadeTime);

        YG2.saves.isFirstPlay = false;
        YG2.SaveProgress();

        _game.StartGame();
    }

    private void FinishGame()
    {
        _ambientSoundPlayer.gameObject.SetActive(false);

        Invoke(nameof(ShowGameStatistics), _showStatsDelay);
    }

    private void ShowGameStatistics() =>
        _gameFinishedPanel.gameObject.SetActive(true);
}
