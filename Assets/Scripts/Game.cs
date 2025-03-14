using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.IsClicking += SetStatus;
        _monster.Spooked += FinishGame;
    }

    private void OnDisable()
    {
        _player.IsClicking -= SetStatus;
        _monster.Spooked -= FinishGame;
    }

    private void FinishGame()
    {
        Time.timeScale = 0f;
    }

    private void SetStatus(bool isAbleToSpook) =>
        _monster.SetSpookStatus(isAbleToSpook);
}
