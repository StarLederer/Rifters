﻿using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ExploderSpell : Spell
{
	//
	// Other components
	#region Editor variables
	[Header("Exploder parameters")]
	private AudioSource audioSource = null;
	#endregion

	//
	// Editor variables
	#region Editor variables
	[Header("Projectile")]
	public GameObject exploderProjectilePrefab = null;
	public Transform exploderSpawnpoint = null;
	[Header("Sounds")]
	public AudioClip exploderCrarge = null;
	public AudioClip exploderCrargeVoc = null;
	public AudioClip exploderThrow = null;
	public AudioClip exploderThrowDrum = null;
	public AudioClip exploderThrowVoc = null;
	#endregion

	//
	// Private variables
	#region Editor variables
	//[Header("Exploder parameters")]
	//private GameObject lastFireball = null;
	#endregion

	//--------------------------
	// MonoBehaviour events
	//--------------------------
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	//--------------------------
	// Spell methods
	//--------------------------
	public override void OnFullyRecharged()
	{
		// SFX
		//audioSource.PlayOneShot(fireballCrarge);
		//AudioManager.instance.PlayTribeVoc(fireballCrargeVoc);
	}

	public override bool Trigger()
	{
		if (!base.Trigger()) return false; // does cooldown

		// Get the direction vector
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 150f))
		{
			// projectile
			Vector3 direction = hit.point - exploderSpawnpoint.position;
			Vector3 p = exploderSpawnpoint.position;
			Quaternion r = Quaternion.LookRotation(direction);
			player.CmdSpawnObject(p.x, p.y, p.z, r.x, r.y, r.z, r.w);

			// SFX
			audioSource.PlayOneShot(exploderThrow);
			AudioManager.instance.PlayDrum(exploderThrowDrum);
			AudioManager.instance.PlayTribeVoc(exploderThrowVoc);
		}

		return true;
	}
}
