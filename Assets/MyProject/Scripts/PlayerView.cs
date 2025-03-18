using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public readonly int Clicked = Animator.StringToHash(nameof(Clicked));
    public readonly int IsClicked = Animator.StringToHash(nameof(IsClicked));

    [SerializeField] private Animator _fingersAnimator;
    [SerializeField] private Animator _zoomAnimator;

    public bool IsZoomed => 
        _zoomAnimator.GetBool(IsClicked);

    public void ZoomIn()
    {
        _fingersAnimator.SetTrigger(Clicked);

        _zoomAnimator.SetBool(IsClicked, true);        
    }

    public void ZoomOut() => 
        _zoomAnimator.SetBool(IsClicked, false);
}
