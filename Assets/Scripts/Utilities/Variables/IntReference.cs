using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IntReference
{
    public bool UseConstant = true;
    public int ConstantValue;
    public IntVariable Variable;
    
    public IntReference() {}
    
    public IntReference(int value)
    {
        UseConstant = true;
        ConstantValue = value;
    }
    
    public int Value
    {
        get { return UseConstant ? ConstantValue : Variable.CurrentValue; }
    }
    
    public static implicit operator int(IntReference reference)
    {
        return reference.Value;
    }
}
