using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ImageSwitcher : MonoBehaviour
{
    private const string Russian = "ru";
    private const string English = "en";
    private const string Turkey = "tr";

    [SerializeField] private Image _logo;

    [SerializeField] private Sprite _ruLogo;
    [SerializeField] private Sprite _enLogo;
    [SerializeField] private Sprite _trLogo;

    private Dictionary<string, Sprite> _logoTranslationDictionary = new Dictionary<string, Sprite>();

    private void Awake()
    {
        if (_logoTranslationDictionary.Count == 0)
        {
            _logoTranslationDictionary.Add(Russian, _ruLogo);
            _logoTranslationDictionary.Add(English, _enLogo);
            _logoTranslationDictionary.Add(Turkey, _trLogo);
        }

        _logo.sprite = _logoTranslationDictionary[YG2.lang];
    }
}
