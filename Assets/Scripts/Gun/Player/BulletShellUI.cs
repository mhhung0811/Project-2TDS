using System;
using UnityEngine;
using UnityEngine.UI;

public class BulletShellUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _image;
    private bool _isFull = false;

    public bool IsFull
    {
        get => _isFull;
        set
        {
            _isFull = value;
            if (_image != null)
            {
                _image.sprite = value ? fullShell : emptyShell;
            }
            else
            {
                Debug.LogWarning("Image not found");
            }
        }
    }
    
    public int gunId { get; private set; }
    
    public float shellDistant = 5;
    public float shellSize = 30;
    
    [SerializeField] private Sprite emptyShell;
    [SerializeField] private Sprite fullShell;

    private void Awake()
    {
        _rectTransform.sizeDelta = new Vector2(shellSize, shellSize);
    }

    public void SetGunId(int id)
    {
        gunId = id;
    }
}