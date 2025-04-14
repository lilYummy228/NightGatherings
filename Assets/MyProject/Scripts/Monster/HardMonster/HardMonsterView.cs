using UnityEngine;
using UnityEngine.UI;

public class HardMonsterView : MonoBehaviour
{
    public readonly int Jumpscare = Animator.StringToHash(nameof(Jumpscare));

    [SerializeField] private Image _jumpscareImage;
    [SerializeField] protected Monster _monster;
    [SerializeField] protected Animator _animator;

    protected virtual void OnEnable() =>
        _monster.Jumpedscare += Show;

    protected virtual void OnDisable() => 
        _monster.Jumpedscare -= Show;

    private void Show()
    {
        _jumpscareImage.enabled = true;

        _animator.SetTrigger(Jumpscare);
    }
}