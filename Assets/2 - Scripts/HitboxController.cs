using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
	[SerializeField] private List<Stats> _insideHitbox = new List<Stats>();
	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player")) return;

		_insideHitbox.Add(other.GetComponent<Stats>());
	}

	private void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Player")) return;

		_insideHitbox.Remove(other.GetComponent<Stats>());
	}

	public void DealDamage(float damage)
	{
		foreach (Stats stats in _insideHitbox)
		{
			stats.TakeDamage(damage);
		}
	}
}
