using UnityEngine;

public class BossController : Stats
{

	public static BossController Instance { get; private set; }

	[SerializeField] private bool _isAttacking = false;
	[SerializeField] private BossAttack _currentAttack = BossAttack.Attack1;

	[SerializeField] private float _attackCooldown = 15f;

	private float _attackCooldownTimer = 0;
	private int[] _attacks = new int[3] { 1, 1, 1 };


	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	private void Update()
	{
		if (Health <= 0)
		{
			Death();
		}

		_attackCooldownTimer += Time.deltaTime;

		if (_attackCooldownTimer <= _attackCooldown || _isAttacking) return;


		_attackCooldownTimer = 0;
		_currentAttack = (BossAttack)Random.Range(0, 3);
		_isAttacking = true;

		int attack = GetRandomAttack();

		if (attack == 0)
		{
			_currentAttack = BossAttack.Attack1;
		}
		else if (attack == 1)
		{
			_currentAttack = BossAttack.Attack2;
		}
		else if (attack == 2)
		{
			_currentAttack = BossAttack.Attack3;
		}

		Attack(_currentAttack);
	}

	private void Attack(BossAttack attack)
	{
		switch (attack)
		{
			case BossAttack.Attack1:

				GetComponent<BossShooter>().Shoot();

				break;
			case BossAttack.Attack2:

				GetComponent<BossAnimationAttacks>().Charge();

				break;
			case BossAttack.Attack3:

				GetComponent<BossAnimationAttacks>().Swipe();

				break;
		}
	}

	//weighted random
	private int GetRandomAttack()
	{
		int total = 0;
		for (int i = 0; i < _attacks.Length; i++)
		{
			total += _attacks[i];
		}

		int randomPoint = Random.Range(0, total);

		for (int i = 0; i < _attacks.Length; i++)
		{
			if (randomPoint < _attacks[i])
			{
				_attacks[i] = 1;
				return i;
			}
			else
			{
				randomPoint -= _attacks[i];
				_attacks[i]++;
			}
		}
		return 0;
	}

	public void EndAttack()
	{
		_isAttacking = false;
		_attackCooldownTimer = 0;
	}

	private void Death()
	{
		print("Boss is dead");
	}
}
public enum BossAttack
{
	Attack1,
	Attack2,
	Attack3
}