using System.Collections;
using UnityEngine;

public class PaymentProgressTimer : MonoBehaviour
{
    Coroutine coroutine;

    [SerializeField] GameObject layer;

    private void OnEnable()
    {
        StartCoroutine(Timer());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3f);
        layer.SetActive(false);
        OnDisable();
    }
}
