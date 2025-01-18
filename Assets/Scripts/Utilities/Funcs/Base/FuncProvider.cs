using System;
using UnityEngine;

public class FuncProvider<TResult, TParams> : ScriptableObject
{
    private Func<TParams, TResult> _cachedFunc;
    
    /// <summary>
    /// Assigns a function to the provider by specifying the target and method name.
    /// </summary>
    public void SetFunction(MonoBehaviour target, string methodName)
    {
        if (target == null || string.IsNullOrEmpty(methodName))
        {
            Debug.LogError("FuncProvider: Target or method name is null!");
            _cachedFunc = null;
            return;
        }

        if (_cachedFunc != null)
        {
            Debug.LogWarning("FuncProvider: Function is being reassigned! Previous function will be overwritten.");
        }

        var methodInfo = target.GetType().GetMethod(methodName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
        if (methodInfo == null)
        {
            Debug.LogError($"FuncProvider: Method '{methodName}' not found on target '{target.name}'!");
            return;
        }

        _cachedFunc = (Func<TParams, TResult>)Delegate.CreateDelegate(typeof(Func<TParams, TResult>), target, methodInfo);
    }

    /// <summary>
    /// Assigns a delegate directly to the provider.
    /// </summary>
    public void SetFunction(Func<TParams, TResult> functionDelegate)
    {
        if (_cachedFunc != null)
        {
            Debug.LogWarning("FuncProvider: Function is being reassigned! Previous function will be overwritten.");
        }

        _cachedFunc = functionDelegate;
    }

    /// <summary>
    /// Retrieves the assigned function.
    /// </summary>
    public Func<TParams, TResult> GetFunction()
    {
        if (_cachedFunc == null)
        {
            Debug.LogError("FuncProvider: Function is not assigned!");
        }
        return _cachedFunc;
    }
}