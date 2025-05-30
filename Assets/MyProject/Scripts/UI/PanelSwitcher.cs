using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    private Transform CurrentPanel => transform;

    public void Open(Transform panel)
    {
        CurrentPanel.gameObject.SetActive(false);
        panel.gameObject.SetActive(true);
    }
}
