using UnityEngine;

public class HitboxController : MonoBehaviour
{
	[SerializeField] private int _damage = 3;
	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player")) return;

		other.GetComponent<Stats>().TakeDamage(_damage);
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!other.gameObject.CompareTag("Player")) return;

		other.gameObject.GetComponent<Stats>().TakeDamage(_damage);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.5f);
	}
}
