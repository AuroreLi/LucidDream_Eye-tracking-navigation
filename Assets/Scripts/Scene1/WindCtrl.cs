using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindCtrl : MonoBehaviour
{
    public float backSpeed;
    public Move moveCtrl;

    public AudioSource windSource;
    private bool change;
    private bool isadd;
    private float changeVolume;//change the volume level
    void Start()
    {
        StartCoroutine(ChangWind());
    }

    void Update()
    {
        if (change)
        {
            if (isadd)
            {
                if (windSource.volume < changeVolume)
                {
                    windSource.volume += Time.deltaTime/10;
                }
                else
                {
                    change = false;
                    StartCoroutine(ChangWind());
                }
            }
            else
            {
                if (windSource.volume > changeVolume)
                {
                    windSource.volume -= Time.deltaTime/10;
                }
                else
                {
                    change = false;
                    StartCoroutine(ChangWind());
                }
            }
        }

        if (windSource.volume > 0.5f)
        {
            moveCtrl.SetOffsetSpeed(-backSpeed * windSource.volume);
        }
        else
        {
            moveCtrl.SetOffsetSpeed(0);
        }
    }

    private IEnumerator ChangWind()
    {
        float waitTime = Random.Range(1f, 3f);//随机3-10秒
        yield return new WaitForSeconds(waitTime);
        changeVolume = Random.Range(0f, 1f);//随机0-1的值
        isadd = changeVolume > windSource.volume;
        change = true;
    }
}
