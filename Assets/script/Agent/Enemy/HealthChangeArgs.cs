using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


public struct HealthChangeArgs
{
    public int newValue;
    public int oldValue;
    public int attemptedChange;
    public int ActualChange => newValue - oldValue;        
}