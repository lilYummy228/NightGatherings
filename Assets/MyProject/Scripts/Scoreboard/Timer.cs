using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private const int SecondsPerMinute = 60;
    private const int Second = 1;

    private WaitForSeconds _secondStep;
    private Coroutine _timeCoroutine;

    public int Seconds {  get; private set; }
    public int Minutes { get; private set; }

    private void Awake() =>
        _secondStep = new WaitForSeconds(Second);

    public void StartCount() =>
        _timeCoroutine = StartCoroutine(CountTime());

    public void StopCount()
    {
        if (_timeCoroutine != null)
            StopCoroutine(_timeCoroutine);
    }

    public IEnumerator CountTime()
    {
        while (enabled)
        {
            if (Seconds == SecondsPerMinute)
            {
                Minutes++;
                Seconds = default;
            }
            else
            {
                Seconds++;
            }

            yield return _secondStep;
        }
    }
}
