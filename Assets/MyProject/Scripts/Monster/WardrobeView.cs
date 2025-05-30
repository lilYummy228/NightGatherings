using UnityEngine;

public class WardrobeView : MonoBehaviour
{
    public readonly int IsOpened = Animator.StringToHash(nameof(IsOpened));

    [SerializeField] private Animator _animator;
    [SerializeField] private Wardrobe _wardrobe;

    private void OnEnable() => 
        _wardrobe.IsDoorOpened += Animate;

    private void OnDisable() => 
        _wardrobe.IsDoorOpened -= Animate;

    private void Animate(bool isOpened) => 
        _animator.SetBool(IsOpened, isOpened);

    public void OnDoorOpened() =>
        _wardrobe.OpenDoor();
}