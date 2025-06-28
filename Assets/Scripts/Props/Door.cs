using UnityEngine;

public class Door : MonoBehaviour
{
    private bool _isClose = true;   // Better set default or else on change won't trigger

    public bool IsClose
    {
        get => _isClose;
        set
        {
            if (_isClose != value)
            {
                _isClose = value;
                _animator.SetBool("isClose", value);
                _boxCollider2D.isTrigger = !value;

                if (value)
                {
                    SoundManager.Instance.PlaySound("DoorClose");
                }
                else
                {
                    SoundManager.Instance.PlaySound("DoorOpen");
                }
            }
        }
    }
    
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;

    // private RoomController _room;
    private BoxCollider2D _interactionZone;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        
        Transform parentOfParent = transform.parent?.parent;
        if (parentOfParent != null)
        {
            // _room = parentOfParent.GetComponent<RoomController>();
        }
        
        // if (_room == null)
        // {
        //     Debug.LogWarning("RoomController parent not found");
        // }
        
        _interactionZone = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    
    void Start()
    {
        IsClose = false;
    }
    
    
    public void DoorTrigger()
    {
        // if (_room != null)
        // {
        //     _room.DelayTriggerdStateToState(_room.roomStateMachine.activeState);
        // }
    }
    public void TurnOnInteractionZone()
    {
        _interactionZone.enabled = true;
    }
    
    public void TurnOffInteractionZone()
    {
        _interactionZone.enabled = false;
    }
}