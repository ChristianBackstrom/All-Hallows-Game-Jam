using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private GameObject bullet = null;
	[SerializeField] private bool canMove = true;
	[SerializeField] private bool canShoot = true;
	private CharacterController controller;

	private float shotCooldown = 6f / 13f;
	private float shotTimer = 0f;

	// Start is called before the first frame update
	void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		shotTimer += Time.deltaTime;

		if (canMove)
		{
			UpdateMovement();
		}

		RotateLowerBody();

		if (Input.GetMouseButton(0) && canShoot && shotTimer >= shotCooldown)
		{
			shotTimer = 0;
			Shoot();
		}
	}

	void UpdateMovement()
	{
		var moveDirection = GetMoveDirection();
		controller.Move(moveDirection * Time.deltaTime);
	}

	void RotateLowerBody()
	{
		var moveDirection = GetMoveDirection();
		var rotation = transform.localEulerAngles;
		rotation.y = Mathf.Rad2Deg * Mathf.Atan2(moveDirection.x, moveDirection.z);

		transform.localEulerAngles = rotation;
	}

	Vector3 GetMoveDirection()
	{
		var inputHorizontal = Input.GetAxisRaw("Horizontal");
		var inputVertical = Input.GetAxisRaw("Vertical");
		
		return new Vector3(inputHorizontal, 0, inputVertical).normalized * moveSpeed;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);

		var cursorPosition = GetCursorPosition();
		if (Application.isPlaying && cursorPosition.HasValue)
		{
			Gizmos.DrawWireSphere(cursorPosition.Value, 0.5f);
		}
	}

	Vector3? GetCursorPosition()
	{
		var ray = Camera.main.ScreenPointToRay(new(Input.mousePosition.x, Input.mousePosition.y, 0));
		return Utils.LinePlaneIntersect(new Vector3(0, 0, 0), Vector3.up, ray.origin, ray.direction);
	}

	void Shoot()
	{
		var cursorPosition = GetCursorPosition();
		if (!cursorPosition.HasValue) return;
		if (Time.timeSinceLevelLoad % shotCooldown < .2f && Time.timeSinceLevelLoad % shotCooldown > 0) return;

		AudioManager.Instance.Play("Shoot");

		var direction = cursorPosition.Value - transform.position;
		direction.y = transform.position.y;

		var bulletInstance = Instantiate(bullet, transform.position + direction.normalized, Quaternion.identity);

		bulletInstance.transform.rotation = Quaternion.LookRotation(direction.normalized);
	}
}
