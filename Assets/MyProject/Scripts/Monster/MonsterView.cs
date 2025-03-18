using UnityEngine;
using UnityEngine.UI;

public class MonsterView : MonoBehaviour
{
    public readonly int Jumpscare = Animator.StringToHash(nameof(Jumpscare));
    public readonly int Scratch = Animator.StringToHash(nameof(Scratch));

    [SerializeField] private Monster _monster;
    [SerializeField] private Image _jumpscareImage;
    [SerializeField] private Image _monsterGotOutImage;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _monster.Jumpedscare += Show;

        var monster = _monster as MediumMonster;

        if (monster != null)
            monster.IsScratching += GetOut;
    }

    private void OnDisable()
    {
        _monster.Jumpedscare -= Show;

        var monster = _monster as MediumMonster;

        if (monster != null)
            monster.IsScratching -= GetOut;
    }

    private void Show()
    {
        _jumpscareImage.enabled = true;

        _animator.SetTrigger(Jumpscare);
    }

    private void GetOut(bool isScratching)
    {
        _monsterGotOutImage.enabled = isScratching;

        _animator.SetBool(Scratch, isScratching);
    }

    public void OnGotOut() => 
        _monster.GetOut();
}
