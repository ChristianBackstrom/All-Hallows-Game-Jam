using UnityEngine;
using System.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BossShooter : Stats
{
	[Header("References")]
	[SerializeField] private GameObject _shotPrefab;
	[SerializeField] private Transform _shotSpawnPoint;

	[Header("Settings")]
	[SerializeField] private BulletData _LineShootData;
	[SerializeField] private BulletData _CircleShootData;


	private ShotStyle _currentShotStyle;
	private float _shootTimer;
	private bool _isShooting;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	[ContextMenu("Shoot")]
	public void Shoot()
	{
		_isShooting = true;
		_shootTimer = 0;

		int shotStyle = Random.Range(0, 2);

		if (shotStyle == 0)
		{
			RadiusShoot();
			_currentShotStyle = ShotStyle.Radius;
		}
		else if (shotStyle == 1)
		{
			LineShoot();
			_currentShotStyle = ShotStyle.Line;
		}
	}

	public async void RadiusShoot()
	{
		float startRadius = -_CircleShootData._shootRadius / 2;

		for (int i = 0; i < _CircleShootData._amountOfShots; i++)
		{
			await Task.Delay(_CircleShootData._shootDelay);

			Quaternion rotation = transform.rotation * Quaternion.Euler(0, startRadius + (_CircleShootData._shootRadius / _CircleShootData._amountOfShots * i), 0);

			GameObject shot = Instantiate(_shotPrefab, _shotSpawnPoint.position, rotation, _shotSpawnPoint);
		}
	}

	private async void LineShoot()
	{
		_animator.SetTrigger("LineShoot");

		for (int i = 0; i < _LineShootData._amountOfShots; i++)
		{
			await Task.Delay(_LineShootData._shootDelay);

			GameObject shot = Instantiate(_shotPrefab, _shotSpawnPoint.position, transform.rotation, this.transform);

		}
	}

	private void Update()
	{
		if (!_isShooting) return;

		_shootTimer += Time.deltaTime;

		float attackLength = _currentShotStyle == ShotStyle.Radius ? _CircleShootData._attackLength : _LineShootData._attackLength;

		if (_shootTimer >= attackLength)
		{
			_isShooting = false;
			BossController.Instance.EndAttack();
		}
	}

	private enum ShotStyle
	{
		Line,
		Radius
	}
}