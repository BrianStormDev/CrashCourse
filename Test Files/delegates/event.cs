using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEvents : MonoBehaviour
{
    public event Action OnJump;

    public void Jump()
    {
        OnJump?.Invoke();
    }
}