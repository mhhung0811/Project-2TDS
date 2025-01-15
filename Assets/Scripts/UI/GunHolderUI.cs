using UnityEngine;

public class GunHolderUI : MonoBehaviour
{
    [SerializeField] private InUseGun inUseGun;
    private Animator _animator;
    
    public void ChangeGun((int id, int maxAmmo) parameters)
    {
        // Destroy all children
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        // Create new gun
        var gunUI = Instantiate(inUseGun.GetGunUIById(parameters.id), transform);
        _animator = gunUI.GetComponent<Animator>();
    }
    
    public void SetAnimation((string name, bool state) parameters)
    {
        _animator.SetBool(parameters.name, parameters.state);
    }
}