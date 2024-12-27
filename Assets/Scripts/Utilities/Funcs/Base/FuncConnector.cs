using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Serialization;

public class FuncConnector<TResult, TParams> : MonoBehaviour
{
    public string description;
    
    [Tooltip("Reference to the FuncProvider where the function will be assigned.")]
    public FuncProvider<TResult, TParams> funcProvider;

    [Tooltip("MonoBehaviour containing the method to assign.")]
    public MonoBehaviour func;

    [Tooltip("Name of the selected method.")]
    public string selectedMethod;

    private void Awake()
    {
        // if (funcProvider == null || func == null || string.IsNullOrEmpty(selectedMethod))
        // {
        //     Debug.LogError("FuncProvider, Func, or SelectedMethod is not assigned!");
        //     return;
        // }
        if (funcProvider == null)
        {
            Debug.LogError("FuncProvider is not assigned!");
            return;
        }

        if (func == null)
        {
            Debug.LogError("Func is not assigned!");
        }

        if (string.IsNullOrEmpty(selectedMethod))
        {
            Debug.LogError("Selected method is not assigned!");
        }

        // Find and assign the selected method
        var methodInfo = func.GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .FirstOrDefault(m => m.Name == selectedMethod &&
                                 m.ReturnType == typeof(TResult) &&
                                 m.GetParameters().Length == 1 &&
                                 m.GetParameters()[0].ParameterType == typeof(TParams));

        if (methodInfo == null)
        {
            Debug.LogError($"No matching method named '{selectedMethod}' found on {func.name}!");
            return;
        }

        var functionDelegate = (Func<TParams, TResult>)Delegate.CreateDelegate(typeof(Func<TParams, TResult>), func, methodInfo);
        funcProvider.SetFunction(functionDelegate);
    }
}