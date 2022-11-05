using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationAttacks : MonoBehaviour
{
	private Animator anim;

	private void Awake() 
	{
		anim = GetComponent<Animator>();
	}

	public void Charge()
	{
		anim.SetTrigger("Charge");
	}

	public void Swipe()
	{
		anim.SetTrigger("Swipe");
	}
}
