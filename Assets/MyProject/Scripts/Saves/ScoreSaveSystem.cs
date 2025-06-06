using UnityEngine;
using YG;
using PlayerPrefs = RedefineYG.PlayerPrefs;

public class ScoreSaveSystem : MonoBehaviour
{
    private const string NameLB = "Leaderboard";

    [SerializeField] private ScoreCounter _scoreCounter;

    public bool IsNewRecord { get; private set; } = false;
    public ScoreCounter ScoreCounter => _scoreCounter;

    public void SaveCloudProgress()
    {
        if (YG2.saves.Points < _scoreCounter.Score)
        {
            YG2.saves.Points = _scoreCounter.Score;

            YG2.SetLeaderboard(NameLB, _scoreCounter.Score);

            IsNewRecord = true;
        }

        YG2.SaveProgress();
    }

    public void SaveLocalProgress()
    {
        if (PlayerPrefs.GetInt(NameLB) < _scoreCounter.Score)
        {
            PlayerPrefs.SetInt(NameLB, _scoreCounter.Score);

            YG2.SetLeaderboard(NameLB, _scoreCounter.Score);

            IsNewRecord = true;
        }

        PlayerPrefs.Save();
    }
}
