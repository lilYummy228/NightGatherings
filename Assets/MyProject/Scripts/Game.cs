using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using YG;

public class Game : MonoBehaviour
{
    private const string NameLB = "Leaderboard";

    [SerializeField] private Monster _monster;
    [SerializeField] private Player _player;
    [SerializeField] private Wardrobe _wardrobe;
    [SerializeField] private AmbientSoundPlayer _ambientSoundPlayer;
    [SerializeField] private Transform _gameFinishedPanel;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private CanvasGroup _tutorial;

    private float _showStatsDelay = 2f;
    private float _fadeTime = 1.5f;

    private void OnEnable()
    {
        _player.IsClicking += SetMonsterStatus;

        if (YG2.saves.isFirstPlay == true)
            StartCoroutine(ShowTutorial());
        else
            StartGame();
    }

    private void OnDisable()
    {
        _player.IsClicking -= SetMonsterStatus;
        _monster.Jumpedscare -= FinishGame;
    }

    private IEnumerator ShowTutorial()
    {
        _tutorial.gameObject.SetActive(true);

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        _tutorial.DOFade(default, _fadeTime);

#if UNITY_EDITOR == false
        YG2.saves.isFirstPlay = false;
        YG2.SaveProgress();
#endif

        StartGame();
    }

    private void StartGame()
    {
        _monster.gameObject.SetActive(true);
        _monster.Jumpedscare += FinishGame;
    }

    private void FinishGame()
    {
        _player.gameObject.SetActive(false);
        _ambientSoundPlayer.gameObject.SetActive(false);
        _wardrobe.gameObject.SetActive(false);

        Invoke(nameof(ShowGameStatistics), _showStatsDelay);

        SaveProgress();
    }

    private void SaveProgress()
    {
        if (YG2.saves.points < _scoreCounter.Score)
        {
            YG2.saves.points = _scoreCounter.Score;
            YG2.SetLeaderboard(NameLB, _scoreCounter.Score);
        }

        YG2.SaveProgress();
    }

    private void ShowGameStatistics() =>
        _gameFinishedPanel.gameObject.SetActive(true);

    private void SetMonsterStatus(bool isAbleToSpook) =>
        _monster.SetAbleStatus(isAbleToSpook);
}
