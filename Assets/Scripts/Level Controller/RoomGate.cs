using System.Collections;
using UnityEngine;

public class RoomGate : MonoBehaviour
{
    [SerializeField] private VoidGameObjectFuncProvider onEnterOtherGate;
    [SerializeField] private VoidEvent onFadeOut;
    [SerializeField] private Transform enterPoint;
    
    private RoomController _room;

    private void Awake()
    {
        _room = transform.parent.parent.gameObject.GetComponent<RoomController>();
    }
    
    // Func
    public object EnterGate(GameObject obj)
    {
        obj.transform.position = enterPoint.position;
        obj.GetComponent<Player>().ToIdleState();
        _room.Entry();
        return null;
    }
    
    public void ExitGate(GameObject obj)
    {
        onEnterOtherGate.GetFunction().Invoke(obj);
        onFadeOut?.Raise(new Void());
        _room.Exit();
    }
}