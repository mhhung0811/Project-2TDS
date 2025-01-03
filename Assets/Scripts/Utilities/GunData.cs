﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "NewGunDataSO")]
public class GunData : ScriptableObject
{
	[Header("Basic Info")]
	public string gunName;

	[Header("Ammo Settings")]
	public int maxAmmoPerMag;  // Số đạn trong 1 băng
	public int currentAmmo;    // Số đạn hiện tại
	public int totalAmmo;      // Tổng số đạn còn lại

	[Header("Reload Settings")]
	public float reloadTime;   // Thời gian nạp đạn

	[Header("Shooting Settings")]
	public float fireRate;     // Tốc độ bắn 
	public float damage;       // Sát thương mỗi viên đạn
	public float bulletSpeed;  // Tốc độ đạn
}
