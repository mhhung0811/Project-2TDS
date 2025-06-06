﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "Data Object/NewGunDataSO")]
public class GunData : ScriptableObject
{
	[Header("Basic Info")]
	public string gunName;

	[Header("Ammo Settings")]
	public bool isInfiniteAmmo = false; // Số đạn vô hạn
	public int maxAmmoPerMag;  // Số đạn trong 1 băng
	public int currentAmmo;    // Số đạn hiện tại
	public int totalAmmo;      // Tổng số đạn còn lại

	[Header("ManaCost ( n mana / ammo )")]
	public int manaCost;      // Số năng lượng tiêu hao khi bắn 1 viên đạn

	[Header("Reload Settings")]
	public float reloadTime;   // Thời gian nạp đạn

	[Header("Shooting Settings")]
	public float fireRate;     // Tốc độ bắn 

	[Header("Position Hold Gun")]
	public Vector3 posHoldGun;

	[Header("Position Gun")]
	public Vector3 posGun;
}
