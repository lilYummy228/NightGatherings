using UnityEngine;
using UnityEngine.UI;

public class MediumMonsterView : HardMonsterView
{
    public readonly int IsScratching = Animator.StringToHash(nameof(IsScratching));

    [SerializeField] private Image _monsterGotOutImage;

    protected override void OnEnable()
    {
        base.OnEnable();

        var monster = _monster as MediumMonster;

        if (monster != null)
            monster.IsScratching += GetOut;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        var monster = _monster as MediumMonster;

        if (monster != null)
            monster.IsScratching -= GetOut;
    }


    private void GetOut(bool isScratching) => 
        _animator.SetBool(IsScratching, isScratching);


    public void OnGotOut() =>
        _monster.TryToJumpscare();
}
