using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEvents : MonoBehaviour
{
    // public delegate void EventHandler(object sender, EventArgs e);
    public class DamageEventArgs : EventArgs
    {
        public int DamageAmount { get; }
        public DamageEventArgs(int damage) => DamageAmount = damage;
    }
    public event EventHandler<DamageEventArgs> Damaged;
}