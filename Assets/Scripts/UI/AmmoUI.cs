using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    public IntVariable PlayerAmmo;
    public IntVariable PlayerMaxAmmo;
    
    private List<BulletShellUI> bulletShells = new List<BulletShellUI>();
}