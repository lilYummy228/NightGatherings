using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PhoneAnimator _phoneAnimator;
    [SerializeField] private ScoreCounter _scoreCounter;

    public event Action<bool> IsClicking;

    private float _time;
    private float _delay = 0.25f;
    private WaitForFixedUpdate _waitForFixedUpdate;

    private void Awake()
    {
        _waitForFixedUpdate = new WaitForFixedUpdate();

        StartCoroutine(nameof(RemovePhone));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _phoneAnimator.ZoomIn();

            IsClicking?.Invoke(true);

            _time = Time.time + _delay;

            _scoreCounter.Add();
        }
    }

    private IEnumerator RemovePhone()
    {
        while (enabled)
        {
            if (_time < Time.time)
            {
                IsClicking?.Invoke(false);

                if (_phoneAnimator.IsZoomed)
                    _phoneAnimator.ZoomOut();
            }

            yield return _waitForFixedUpdate;
        }
    }
}
