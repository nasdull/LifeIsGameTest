using System.Collections.Generic;
using UnityEngine;

public class AttractorGun : Weapon
{
	public float gravityForce = 0.0f;
	public float pushForce = 0.0f;

	[SerializeField] private Attractor gravityBullet;

	private bool isLoading;

	public override void Awake()
	{
		base.Awake();
		gravityBullet.mainBullet.gravityPull = gravityForce;
	}

	public override void FireWeapon()
	{
		if(!isLoading)
		{
			bulletPrefab[0].gameObject.SetActive(true);
			isLoading = true;
		}
		else
		{
			ShootOrbitingObjects();
			bulletPrefab[0].gameObject.SetActive(false);
			isLoading = false;
		}
	}

	void ShootOrbitingObjects()
	{
		List<Rigidbody> grabedObjects = gravityBullet.GetOrbitingObjects();

		foreach(Rigidbody body in grabedObjects)
		{
			body.useGravity = true;
			body.velocity = bulletSpawnPoints[0].forward * pushForce;
		}
	}
}
