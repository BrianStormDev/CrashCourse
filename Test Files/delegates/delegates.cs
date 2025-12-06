using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEvents : MonoBehaviour
{
    public delegate int MyDelegate(string s);
    private MyDelegate testDelegateFunction;

    private void Start()
    {
        testDelegateFunction += MyTestDelegateFunction("Hello");
    }

    private int MyTestDelegateFunction(string s)
    {
        return s.length;
    }

}