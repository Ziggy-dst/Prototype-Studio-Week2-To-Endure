using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DayCounter.OnDaysReset += DestroySelf;
    }

    private void OnDestroy()
    {
        DayCounter.OnDaysReset -= DestroySelf;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
