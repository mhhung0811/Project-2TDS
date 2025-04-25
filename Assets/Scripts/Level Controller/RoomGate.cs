using System.Collections;
using UnityEngine;

public class RoomGate : MonoBehaviour
{
    [SerializeField] private VoidGameObjectFuncProvider onEnterOtherGate;
    [SerializeField] private VoidEvent onFadeOut;
    [SerializeField] private VoidEvent onFadeIn;
    [SerializeField] private Transform enterPoint;
    
    // Func
    public object EnterGate(GameObject obj)
    {
        obj.transform.position = enterPoint.position;
        return null;
    }
    
    public void ExitGate(GameObject obj)
    {
        onEnterOtherGate.GetFunction().Invoke(obj);
        onFadeOut?.Raise(new Void());
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.25f);
        onFadeIn?.Raise(new Void());
    }
}