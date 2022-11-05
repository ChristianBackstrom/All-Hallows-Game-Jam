using UnityEngine;
using System.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BossShooter : Stats
{
	[Header("References")]
	[SerializeField] private GameObject _shotPrefab;

	[Header("Settings")]
	[SerializeField] private float _shootRadius;
	[SerializeField] private float _shootPointOffset;
	[SerializeField] private int _shootDelay;
	[SerializeField] private float _amountOfShots;
	[SerializeField] private float _attackLength;

	private float _shootTimer;
	private bool _isShooting;

	[ContextMenu("Shoot")]
	public async void Shoot()
	{
		_isShooting = true;
		_shootTimer = 0;


		float startRadius = -_shootRadius / 2;
		for (int i = 0; i < _amountOfShots; i++)
		{
			await Task.Delay(_shootDelay);
			Quaternion rotation = transform.rotation * Quaternion.Euler(0, startRadius + (_shootRadius / _amountOfShots * i), 0);
			GameObject shot = Instantiate(_shotPrefab, transform.position + transform.forward * _shootPointOffset, rotation, this.transform);
		}
	}

	private void Update()
	{
		if (!_isShooting) return;

		_shootTimer += Time.deltaTime;

		if (_shootTimer >= _attackLength)
		{
			_isShooting = false;
			BossController.Instance.EndAttack();
		}
	}
}