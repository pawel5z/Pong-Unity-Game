using System.Collections;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    public float ShakeIntensity;
    private float _noiseScale = 10;
    private Vector3 _defaultCamPos;

    /// <summary>
    /// Currently running shake coroutine.
    /// </summary>
    private Coroutine _shakeCoroutine;

    public void StartShake(float time)
    {
        StopCoroutine(_shakeCoroutine);
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
            const int decoupling = 17;
            var offset = new Vector2(
                Mathf.Lerp(-1, 1, Mathf.PerlinNoise1D(Time.time * _noiseScale)),
                Mathf.Lerp(-1, 1, Mathf.PerlinNoise1D((Time.time + decoupling) * _noiseScale))
            ) * ShakeIntensity;
            var tempPos = _defaultCamPos + (Vector3)offset;
            transform.position = new Vector3(tempPos.x, tempPos.y, _defaultCamPos.z);
            time -= Time.deltaTime;
            yield return null;
        }

        transform.position = _defaultCamPos;
        yield return null;
    }
}
