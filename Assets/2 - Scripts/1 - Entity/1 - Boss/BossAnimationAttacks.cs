using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationAttacks : MonoBehaviour
{
	[SerializeField] private float _attackCooldown = 5f;

	private float _attackCooldownTimer = 0;
	private bool _isAttacking = false;


	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	public void Charge()
	{
		_isAttacking = true;
		_attackCooldownTimer = 0;
		_animator.SetTrigger("Charge");
	}

	public void Swipe()
	{
		_isAttacking = true;
		_attackCooldownTimer = 0;
		_animator.SetTrigger("Swipe");
		print("Swipe");
	}

	private void Update()
	{
		if (!_isAttacking) return;

		_attackCooldownTimer += Time.deltaTime;

		if (_attackCooldownTimer >= _attackCooldown)
		{
			_isAttacking = false;
			BossController.Instance.EndAttack();
			_animator.SetTrigger("Idle");
		}
	}
}
