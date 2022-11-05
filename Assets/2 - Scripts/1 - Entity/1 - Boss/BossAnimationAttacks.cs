using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationAttacks : MonoBehaviour
{
	[SerializeField] private float _attackCooldown = 5f;
	private Animator anim;

	private float _attackCooldownTimer = 0;
	private bool _isAttacking = false;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public void Charge()
	{
		_isAttacking = true;
		_attackCooldownTimer = 0;
		anim.SetTrigger("Charge");
	}

	public void Swipe()
	{
		_isAttacking = true;
		_attackCooldownTimer = 0;
		anim.SetTrigger("Swipe");
	}

	private void Update()
	{
		if (!_isAttacking) return;

		_attackCooldownTimer += Time.deltaTime;

		if (_attackCooldownTimer >= _attackCooldown)
		{
			_isAttacking = false;
			BossController.Instance.EndAttack();
		}
	}
}
