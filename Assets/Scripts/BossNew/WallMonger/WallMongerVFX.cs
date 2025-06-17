using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMongerVFX : MonoBehaviour
{
    public Animator eyeAnim;
    public Animator tongueAnim;

    public Transform eye;
	public Transform tongue;

	public void PlayVFXAttack()
    {
        // Eye
        eyeAnim.SetBool("Idle", false);
		eyeAnim.SetTrigger("Attack");

		// Tongue
		tongueAnim.SetBool("Idle", false);
		tongueAnim.SetTrigger("Attack");
	}

	public void ExitVFXAttack()
	{
		// Eye
		eyeAnim.SetBool("Attack", false);
		eyeAnim.SetBool("Idle", true);

		// Tongue
		tongueAnim.SetBool("Attack", false);
		tongueAnim.SetBool("Idle", true);
	}

	public void PlayVFXDie()
	{
		//Eye
		eyeAnim.SetBool("Idle", false);
		eyeAnim.SetBool("Die", true);
		eye.transform.localPosition = new Vector3(0, -0.5f, 0);
	}

	public void ExitVFXDie()
	{
		// Eye
		eyeAnim.SetBool("Die", false);
		eyeAnim.SetBool("Idle", true);
		eye.transform.localPosition = new Vector3(0, -0.25f, 0);
	}
}
