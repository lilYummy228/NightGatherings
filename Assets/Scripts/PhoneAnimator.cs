using UnityEngine;

public class PhoneAnimator : MonoBehaviour
{
    private const string ClickTriggerName = "Clicked";
    private const string ZoomTriggerName = "IsClicked";

    [SerializeField] private Animator _fingersAnimator;
    [SerializeField] private Animator _zoomAnimator;

    public bool IsZoomed => 
        _zoomAnimator.GetBool(ZoomTriggerName);

    public void ZoomIn()
    {
        _fingersAnimator.SetTrigger(ClickTriggerName);

        _zoomAnimator.SetBool(ZoomTriggerName, true);        
    }

    public void ZoomOut() => 
        _zoomAnimator.SetBool(ZoomTriggerName, false);
}
