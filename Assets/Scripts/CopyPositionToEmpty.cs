using UnityEngine;
using UnityEditor;

public class CopyPositionsToEmpty : MonoBehaviour
{
    [MenuItem("Tools/Create Empties at Selected Transforms")]
    static void CreateEmptiesAtSelected()
    {
        foreach (Transform selected in Selection.transforms)
        {
            GameObject empty = new GameObject(selected.name + "_Empty");
            empty.transform.position = selected.position;
            empty.transform.rotation = selected.rotation;
            empty.transform.localScale = selected.localScale;

            // Optional: place empty next to the original in the hierarchy
            if (selected.parent != null)
                empty.transform.SetParent(selected.parent);
        }
    }
}