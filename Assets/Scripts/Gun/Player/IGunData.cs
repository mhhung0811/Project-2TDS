public interface IGunData
{
    public string gunName { get; set; }
    
    //Ammo Settings
    public int maxAmmoPerMag { get; set; } // So dang trong 1 bang dan
    public int currentAmmo { get; set; } // So dan hien tai trong bang dan
    public int totalAmmo { get; set; } // So dan con lai

	// Reload Settings
	public float reloadTime { get; set; } // Thoi gian nap dan

	// Shooting Settings
	public float fireRate { get; set; } // Toc do ban dan (dan/giay)
    public float damage { get; set; } // Sat thuong cua dan
    public float bulletSpeed { get; set; } // Toc do cua dan
}
