using UnityEngine;

public class WardrobeAnimator : MonoBehaviour
{
    private const string DoorAnimationName = "IsOpened";

    [SerializeField] private Animator _animator;
    [SerializeField] private Wardrobe _wardrobe;

    private void OnEnable() => 
        _wardrobe.IsDoorOpened += Animate;

    private void OnDisable() => 
        _wardrobe.IsDoorOpened -= Animate;

    private void Animate(bool isOpened) => 
        _animator.SetBool(DoorAnimationName, isOpened);

    public void OnDoorOpened() =>
        _wardrobe.OpenDoor();
}