using UnityEngine;

public class WardrobeView : MonoBehaviour
{
    public readonly int IsOpened = Animator.StringToHash(nameof(IsOpened));
    public readonly int Speed = Animator.StringToHash(nameof(Speed));

    [SerializeField] private Animator _animator;
    [SerializeField] private Wardrobe _wardrobe;

    private float _speed = 1f;

    private void OnEnable() =>
        _wardrobe.IsDoorOpened += Animate;

    private void OnDisable() =>
        _wardrobe.IsDoorOpened -= Animate;

    private void Animate(bool isOpened)
    {
        if (isOpened)
            _speed = Random.Range(1f, 2f);

        _animator.SetFloat(Speed, _speed);
        _animator.SetBool(IsOpened, isOpened);
    }

    public void OnDoorOpened() =>
        _wardrobe.OpenDoor();
}