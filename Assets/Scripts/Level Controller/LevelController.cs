using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Collider2D roomBound;
    [SerializeField] private Transform roomCenter;
    [SerializeField] private Transform enemyHolder;
    
    [Header("SO Events")]
    [SerializeField] private Collider2DEvent changeRoomBound;
    [SerializeField] private LevelControllerEvent changeRoomIndex;
    
    private List<IRoomProp> _roomProps;
    
    private void Awake()
    {
        _roomProps = new List<IRoomProp>(enemyHolder.GetComponentsInChildren<IRoomProp>());
        // Disable enemy holder at the start
        enemyHolder.gameObject.SetActive(false);
    }

    public void Entry()
    {
        // Update A*
        PathRequestManager.Instance.UpdatePos(roomCenter.position);
        
        changeRoomBound.Raise(roomBound);
        changeRoomIndex.Raise(this);
        
        enemyHolder.gameObject.SetActive(true);
        
        foreach(var prop in _roomProps)
        {
            prop.OnRoomEntry();
        }
    }

    public void Exit()
    {
        enemyHolder.gameObject.SetActive(false);
    }

    public void Refresh()
    {
        foreach (var prop in _roomProps)
        {
            prop.OnRoomRefresh();
        }
    }
}