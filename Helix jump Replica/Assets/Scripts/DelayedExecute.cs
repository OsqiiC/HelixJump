using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedExecute : MonoBehaviour
{
    public static DelayedExecute instance;

    private void Awake()
    {
        if (DelayedExecute.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DelayedExecute.instance = this;
    }

    /// <summary>
    /// Executes method after int frames
    /// </summary>
    /// <param name="metho1d"></param>
    /// <param name="delayInFrames"></param>
    public void ExecuteMethod(Action metho1d, int delayInFrames)
    {
        IEnumerator SetDelay(int frames)
        {
            int counter = 0;
            while (counter < frames)
            {
                counter += 1;
                yield return new WaitForEndOfFrame();
            }
            metho1d();
        }
        StartCoroutine(SetDelay(delayInFrames));
    }

    /// <summary>
    /// Executes method after float seconds
    /// </summary>
    /// <param name="metho1d"></param>
    /// <param name="delayInSeconds"></param>
    public void ExecuteMethod(Action metho1d, float delayInSeconds)
    {           
        IEnumerator SetDelay(float seconds)
        {
            float counter = 0;
            while (counter < seconds)
            {             
                counter += Time.deltaTime;
                yield return new WaitForSecondsRealtime(Time.deltaTime);
            }
            metho1d();
        }
        StartCoroutine(SetDelay(delayInSeconds));
    }
}
