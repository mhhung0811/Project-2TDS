using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoTextUI : MonoBehaviour
{
    public IntVariable playerAmmo;
    public IntVariable playerTotalAmmo;
    public TextMeshProUGUI ammoText;
    public GameObject infiniteAmmoText;
    private int totalAmmoText;
    private int ammoTextString;
    
    private void Awake()
    {
        playerAmmo.OnChanged += UpdateAmmoText;
        playerTotalAmmo.OnChanged += UpdateTotalAmmoText;
    }
    
    private void UpdateAmmoText(int ammo)
    {
        if (ammo < 0)
        {
            Debug.LogWarning("Ammo is less than 0.");
        }
        else
        {
            ammoText.text = $"{ammo}/{playerTotalAmmo.CurrentValue}";
        }
    }
    
    private void UpdateTotalAmmoText(int totalAmmo)
    {
        if (totalAmmo < 0)
        {
            infiniteAmmoText.SetActive(true);
            ammoText.gameObject.SetActive(false);
        }
        else
        {
            infiniteAmmoText.SetActive(false);
            ammoText.gameObject.SetActive(true);
            ammoText.text = $"{playerAmmo.CurrentValue}/{totalAmmo}";
        }
    }
    void OnDestroy()
    {
        playerAmmo.OnChanged -= UpdateAmmoText;
        playerTotalAmmo.OnChanged -= UpdateTotalAmmoText;
    }
}