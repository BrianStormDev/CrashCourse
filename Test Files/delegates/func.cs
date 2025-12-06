using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEvents : MonoBehaviour
{
    private Func<int, int, int> add;

    private void Start()
    {
        add = (a, b) => a + b;
    }
}