using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public delegate void TestDelegate(); 
    public delegate bool TestBoolDelegate(int i);
    private TestDelegate testDelegateFunction;
    private TestBoolDelegate testBoolDelegateFunction;
    private Action testAction;
    private Action<int, float> testIntFloatAction;
    private Func<bool> testFunc;
    private Func<int, bool> testIntBoolFunc;

    private void Start()
    {
        // testDelegateFunction = () => {Debug.Log("Anonymous method");};
        // testDelegateFunction();

        // testDelegateFunction = delegate () {Debug.Log("Anonymous method");};
        // testDelegateFunction();

        // testDelegateFunction = MyTestDelegateFunction;
        // testDelegateFunction += MySecondTestDelegateFunction;
        // testDelegateFunction();
        // testDelegateFunction -= MySecondTestDelegateFunction;

        // testBoolDelegateFunctionFunction = MyTestBoolDelegateFunction;
        // Debug.Log(testBoolDelegateFunction(1));

        testIntFloatAction = (int i, float f) => {Debug.Log("Test int float action!"); };
        testFunc = () => false;

        testIntBoolFunc = (int i) => {return i < 5; };

    }

    private void MyTestDelegateFunction()
    {
        Debug.Log("MyTestDelegateFunction");
    }

    private void MySecondTestDelegateFunction()
    {
        Debug.Log("MySecondTestDelegateFunction");
    }

    private bool MyTestBoolDelegateFunction(int i)
    {
        return i < 5;
    }
}