using UnityEngine;

public class GunHolderUI : MonoBehaviour
{
    [SerializeField] private InUseGun inUseGun;
    private Animator _animator;
    
    // Event listener
    public void ChangeGun((GunType type, int maxAmmo) parameters)
    {
        // Destroy all children
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        // Create new gun
        var gunUI = Instantiate(inUseGun.GetGunUIByType(parameters.type), transform);
        _animator = gunUI.GetComponent<Animator>();
    }
    
    // Event listener
    public void SetAnimation((string name, bool state) parameters)
    {
        _animator.SetBool(parameters.name, parameters.state);
    }
}