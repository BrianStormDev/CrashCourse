using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEvents : MonoBehaviour
{
    public event EventHandler OnSpacePressed;

    private void Start()
    {
        OnSpacePressed += Testing_OnSpacePressed;
    }

    private void Testing_OnSpacePressed(object sender, EventArgs e)
    {
        Debug.Log("Space!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Makes sure to invoke the function only if it's not null
            OnSpacePressed?.Invoke(this, EventArgs.empty);
        }
    }
}