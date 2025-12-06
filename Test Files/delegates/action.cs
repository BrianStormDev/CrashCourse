using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEvents : MonoBehaviour
{
    private Action<int, string> printInfo;

    private void Start()
    {
        printInfo = (age, name) => Debug.Log(name + " is " + age + " years old");
    }
}