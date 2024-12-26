using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

[CustomEditor(typeof(FuncConnector<,>), true)]
public class FuncConnectorEditor : Editor
{
    private SerializedProperty funcProviderProp;
    private SerializedProperty funcProp;
    private SerializedProperty selectedMethodProp;

    private Type resultType;
    private Type paramsType;

    private void OnEnable()
    {
        funcProviderProp = serializedObject.FindProperty("funcProvider");
        funcProp = serializedObject.FindProperty("func");
        selectedMethodProp = serializedObject.FindProperty("selectedMethod");

        // Resolve the generic arguments for the base class
        var baseType = target.GetType().BaseType;
        if (baseType != null && baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(FuncConnector<,>))
        {
            var genericArguments = baseType.GetGenericArguments();
            resultType = genericArguments[0];
            paramsType = genericArguments[1];
        }
        // Debug.Log($"Resolved generic types: TResult = {resultType}, TParams = {paramsType}");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (resultType == null || paramsType == null)
        {
            EditorGUILayout.HelpBox("Unable to resolve generic types. Ensure the script inherits FuncConnector<TResult, TParams>.", MessageType.Error);
            return;
        }

        EditorGUILayout.PropertyField(funcProviderProp);
        EditorGUILayout.PropertyField(funcProp);

        MonoBehaviour funcTarget = (MonoBehaviour)funcProp.objectReferenceValue;

        if (funcTarget != null)
        {
            // Find compatible methods
            var compatibleMethods = funcTarget.GetType()
                .GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)
                .Where(m => m.ReturnType == resultType &&
                            m.GetParameters().Length == 1 &&
                            m.GetParameters()[0].ParameterType == paramsType)
                .Select(m => m.Name)
                .ToArray();
            
            // Debug.Log($"Found {compatibleMethods.Length} compatible methods in {funcTarget.name}");
            // Debug.Log($"Fond method 1 {compatibleMethods[0]}");
            
            if (compatibleMethods.Length > 0)
            {
                // Automatically assign the first method if none is selected
                if (string.IsNullOrEmpty(selectedMethodProp.stringValue))
                {
                    selectedMethodProp.stringValue = compatibleMethods[0];
                    // Debug.Log($"Automatically assigning selectedMethod to: {selectedMethodProp.stringValue}");
                }
                
                // Show the dropdown
                int currentIndex = Mathf.Max(0, Array.IndexOf(compatibleMethods, selectedMethodProp.stringValue));
                int newIndex = EditorGUILayout.Popup("Select Method", currentIndex, compatibleMethods);

                if (newIndex != currentIndex)
                {
                    selectedMethodProp.stringValue = compatibleMethods[newIndex];
                    // Debug.Log($"Selected method: {selectedMethodProp.stringValue}");
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No compatible methods found in the selected MonoBehaviour.", MessageType.Warning);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Select a MonoBehaviour to populate methods.", MessageType.Info);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
