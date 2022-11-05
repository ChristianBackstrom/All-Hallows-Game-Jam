using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 5f;
	[SerializeField] private Bullet bullet = null;
	[SerializeField] private bool canMove = true;
	[SerializeField] private bool canShoot = true;
	private CharacterController controller;

	[SerializeField] private Transform _bulletSpawn = null;

	// Start is called before the first frame update
	void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (canMove)
		{
			UpdateMovement();
		}

		RotateLowerBody();

		if (Input.GetMouseButtonDown(0) && canShoot)
		{
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

		var bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);

		Vector3 bulletTarget = cursorPosition.Value;
		bulletTarget.y = transform.position.y;
		bulletInstance.transform.LookAt(bulletTarget);
	}
}
