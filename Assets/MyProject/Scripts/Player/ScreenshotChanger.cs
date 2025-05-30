using UnityEngine;
using UnityEngine.UI;

public class ScreenshotChanger : MonoBehaviour
{
    [SerializeField] private Image _phoneScreen;
    [SerializeField] private Sprite[] _screenshots;

    private int _index;

    public void Swap()
    {
        _index++;

        if (_index > _screenshots.Length - 1)
            _index = 0;

        _phoneScreen.sprite = _screenshots[_index];
    }
}
