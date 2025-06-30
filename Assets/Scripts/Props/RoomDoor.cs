using UnityEngine;

namespace Props
{
    public class RoomDoor : MonoBehaviour
    {
        private bool _isClose = false;
        
        public bool IsClose
        {
            get => _isClose;
            set
            {
                if (_isClose != value)
                {
                    _isClose = value;
                    _animator.Play(value ? "close" : "open");
                    _boxCollider2D.enabled = value;

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
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }
    }
}