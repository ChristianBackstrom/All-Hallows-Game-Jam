using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "All-Hallows-Game-Jam/BulletData", order = 0)]
public class BulletData : ScriptableObject
{
	[Header("Settings")]
	[Space(20)]

	public float _shootRadius;
	public int _shootDelay;
	public float _amountOfShots;
	public float _attackLength;
}
