using UnityEngine;

public class GunUI : MonoBehaviour
{
    public int gunId { get; private set; }

    public void SetGunId(int id)
    {
        gunId = id;
    }
}
