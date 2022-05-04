using UnityEngine;

public class GravityBullet : Bullet
{
	public float gravityPull;

	public override void OnTriggerEnter(Collider other)
	{
		if (!other.GetComponent<Rigidbody>())
			bulletRigidBody.velocity = Vector3.zero;
	}
}
