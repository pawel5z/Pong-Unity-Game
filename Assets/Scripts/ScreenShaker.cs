using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ScreenShaker : MonoBehaviour
{
    public float ShakeIntensity;
    private float _noisePeriodDiv = .1f;
    private Vector3 _defaultCamPos;

    private Coroutine _shakeCoroutine;

    public void StartShake(float time)
    {
        if (_shakeCoroutine != null)
        {
            StopCoroutine(_shakeCoroutine);
        }

        _shakeCoroutine = StartCoroutine(Shake(time));
    }

    private void Start()
    {
        _defaultCamPos = transform.position;
    }

    private IEnumerator Shake(float time)
    {
        while (time > 0)
        {
            var offset = new Vector2(
                Mathf.Lerp(-1, 1, Mathf.PerlinNoise1D(Time.realtimeSinceStartup / _noisePeriodDiv)),
                Mathf.Lerp(-1, 1, Mathf.PerlinNoise1D((Time.realtimeSinceStartup + 17) / _noisePeriodDiv))
            ) * ShakeIntensity;
            Vector3 tempPos = _defaultCamPos + new Vector3(offset.x, offset.y, 0);
            transform.position = new Vector3(tempPos.x, tempPos.y, _defaultCamPos.z);
            time -= Time.deltaTime;
            yield return null;
        }
        transform.position = _defaultCamPos;
        yield return null;
    }
}
