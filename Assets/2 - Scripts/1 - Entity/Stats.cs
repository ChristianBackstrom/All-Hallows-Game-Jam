using UnityEngine;

public class Stats : MonoBehaviour
{
	[SerializeField] private float _health;

	public float Health
	{
		get { return _health; }
		set { _health = value; }
	}

	public void TakeDamage(float damage)
	{
		_health -= damage;
	}

	public void Heal(float amount)
	{
		_health += amount;
	}
}
