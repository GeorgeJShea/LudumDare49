using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    void Awake()
    {

        StartCoroutine(DoEveryFiveSeconds());
    }

    IEnumerator DoEveryFiveSeconds()
    {

        yield return new WaitForSeconds(2);
        DoSomething();
        StartCoroutine(DoEveryFiveSeconds());
    }

    // happens every 0.5 seconds
    void DoSomething()
    {
        Destroy(gameObject);
    }
}
