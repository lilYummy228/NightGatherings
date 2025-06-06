using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSoundPlayer : MonoBehaviour
{
    [SerializeField] private SoundPlayer _soundPlayer;
    [SerializeField] private AudioClip _audioClip;

    private Button _button;

    private void Awake() =>
        _button = gameObject.GetComponent<Button>();

    private void OnEnable() =>
        _button.onClick.AddListener(PlaySound);

    private void OnDisable() =>
        _button.onClick.RemoveListener(PlaySound);

    private void PlaySound() =>
        _soundPlayer.PlaySound(_audioClip);
}
