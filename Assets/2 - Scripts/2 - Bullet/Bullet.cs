using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float _speed = 10;
	[SerializeField] private float _lifeTime = 3;
	[SerializeField] private float _damage = 10;

	private float _lifeTimer = 0;
	private void Update()
	{
		_lifeTimer += Time.deltaTime;

		if (_lifeTimer >= _lifeTime)
		{
			Destroy(gameObject);
			return;
		}
		transform.position += transform.forward * _speed * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Stats>(out Stats stats))
		{
			stats.TakeDamage(_damage);
			Destroy(gameObject);
		}
	}
}
