using UnityEngine;
using UnityEngine.UI;

public class MonsterView : MonoBehaviour
{
    public readonly int Jumpscare = Animator.StringToHash(nameof(Jumpscare));

    [SerializeField] private Monster _monster;
    [SerializeField] private Image _image;
    [SerializeField] private Animator _animator;

    private void OnEnable() =>
        _monster.Spooked += Show;

    private void OnDisable() =>
        _monster.Spooked -= Show;

    private void Show()
    {
        _image.enabled = true;

        _animator.SetTrigger(Jumpscare);
    }
}
