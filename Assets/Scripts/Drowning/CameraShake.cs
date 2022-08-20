using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    private Vector3 _originalPosition;
    public IEnumerator Shake(float duration, float magnitude)
    {
        _originalPosition = transform.position;

        float time = 0f;

        while (time < duration)
        {

            Vector2 Offset = new Vector2(Random.Range(-0.5f, 0.5f) * magnitude, Random.Range(-0.5f, 0.5f) * magnitude);

            transform.localPosition = new Vector3(Offset.x, Offset.y, _originalPosition.z);

            time += Time.deltaTime;

            yield return null;
        }

        transform.position = _originalPosition;
    }
}
