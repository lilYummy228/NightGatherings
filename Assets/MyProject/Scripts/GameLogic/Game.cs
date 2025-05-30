using System;
using UnityEngine;
using YG;

public class Game : MonoBehaviour
{
    private const string NameLB = "Leaderboard";

    [SerializeField] private Monster _monster;
    [SerializeField] private Player _player;
    [SerializeField] private Wardrobe _wardrobe;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private Timer _timer;

    public event Action GameStarted;
    public event Action GameFinished;
    public event Action TutorialStarted;

    public ScoreCounter ScoreCounter => _scoreCounter;
    public bool IsNewRecord {  get; private set; } = false;

    private void OnEnable()
    {
        _player.IsClicking += SetMonsterStatus;

        if (YG2.saves.isFirstPlay == true)
            TutorialStarted?.Invoke();
        else
            StartGame();
    }

    private void OnDisable()
    {
        _player.IsClicking -= SetMonsterStatus;
        _monster.Jumpedscare -= FinishGame;
    }

    public void StartGame()
    {
        _monster.gameObject.SetActive(true);
        _monster.Jumpedscare += FinishGame;
        _timer.StartCount();

        GameStarted?.Invoke();
    }

    private void FinishGame()
    {
        _player.gameObject.SetActive(false);
        _wardrobe.gameObject.SetActive(false);
        _timer.StopCount();

        GameFinished?.Invoke();

        SaveProgress();
    }

    private void SaveProgress()
    {
        if (YG2.saves.points < _scoreCounter.Score)
        {
            YG2.saves.points = _scoreCounter.Score;
            YG2.SetLeaderboard(NameLB, _scoreCounter.Score);

            IsNewRecord = true;
        }

        YG2.SaveProgress();
    }

    private void SetMonsterStatus(bool isAbleToSpook) =>
        _monster.SetAbleStatus(isAbleToSpook);
}
