using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    Coroutine coroutine;

    [SerializeField] TextMeshProUGUI timecounter;
    [SerializeField] GameObject layer;
    [SerializeField] int time = 10;


    private float _savedsecs;
    void Start()
    {
        _savedsecs = time;

        if (!layer.activeSelf)
        {
            layer.SetActive(true);
        }
        else
        {
            timecounter.text = time.ToString();
        }
        
    }
    IEnumerator TimerDecrement()
    {
        for (int i = time; i > 0; i--) 
        {
            timecounter.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        OnDisable();
        SettingsManager.instance.LoadSettings();
    }

    private void OnEnable()
    {
        StartCoroutine(TimerDecrement());
    }

    private void OnDisable()
    {
        timecounter.text = _savedsecs.ToString();
        StopAllCoroutines();
        layer.SetActive(false);
    }
}
