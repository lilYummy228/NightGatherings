using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private Player _player;
    [SerializeField] private Wardrobe _wardrobe;
    [SerializeField] private AmbientSoundPlayer _ambientSoundPlayer;
    [SerializeField] private Transform _gameFinishedPanel;

    private float _showStatsDelay = 2f;

    private void OnEnable()
    {
        _player.IsClicking += SetMonsterStatus;
        _monster.Jumpedscare += FinishGame;
    }

    private void OnDisable()
    {
        _player.IsClicking -= SetMonsterStatus;
        _monster.Jumpedscare -= FinishGame;
    }

    private void FinishGame()
    {
        _player.gameObject.SetActive(false);
        _ambientSoundPlayer.gameObject.SetActive(false);
        _wardrobe.gameObject.SetActive(false);

        Invoke(nameof(ShowGameStatistics), _showStatsDelay);
    }

    private void ShowGameStatistics() => 
        _gameFinishedPanel.gameObject.SetActive(true);

    private void SetMonsterStatus(bool isAbleToSpook) =>
        _monster.SetAbleStatus(isAbleToSpook);
}
