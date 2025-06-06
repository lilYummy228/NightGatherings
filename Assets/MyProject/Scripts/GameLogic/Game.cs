using System;
using UnityEngine;
using YG;

public class Game : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private Player _player;
    [SerializeField] private Wardrobe _wardrobe;
    [SerializeField] private Timer _timer;
    [SerializeField] private ScoreSaveSystem _saveSystem;

    public event Action GameStarted;
    public event Action GameFinished;
    public event Action TutorialStarted;

    public ScoreSaveSystem ScoreSaveSystem => _saveSystem;

    private void Start()
    {
        _player.IsClicking += SetMonsterStatus;

        TutorialStarted?.Invoke();
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

        if (YG2.player.auth)
            _saveSystem.SaveCloudProgress();
        else
            _saveSystem.SaveLocalProgress();
    }

    private void SetMonsterStatus(bool isAbleToSpook) =>
        _monster.SetAbleStatus(isAbleToSpook);
}
