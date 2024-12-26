using UnityEngine;

public class GunPref : MonoBehaviour, IInteractable
{
    public int gunId { get; private set; }
    public bool isInteractable { get; set; }
    public VoidGameObjectFuncProvider returnGunPrefFunc;
    public GameObjectIntFuncProvider getGunFunc;
    
    public void SetGunId(int id)
    {
        gunId = id;
    }
    public void Interact(GameObject go)
    {
        Debug.Log("Is interacting with gun pref");
        returnGunPrefFunc.GetFunction()?.Invoke(gameObject);
        // getGunFunc.GetFunction()?.Invoke((gunId));
    }
}