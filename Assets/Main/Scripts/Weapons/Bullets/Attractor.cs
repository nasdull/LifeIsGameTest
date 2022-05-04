using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
	public GravityBullet mainBullet;
	[SerializeField] private List<Rigidbody> bodies;

	private void Awake()
	{
		bodies = new List<Rigidbody>();
	}

	private void OnDisable()
	{
		foreach (Rigidbody body in bodies)
		{
			if(body != null)
				body.useGravity = true;
		}
		bodies.Clear();
	}

	public List<Rigidbody> GetOrbitingObjects()
	{
		return bodies;
	}

	private void OnTriggerEnter(Collider other)
	{
		Rigidbody newBody = other.GetComponent<Rigidbody>();

		if (newBody != null)
		{
			newBody.useGravity = false;
			bodies.Add(newBody); 
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Rigidbody newBody = other.GetComponent<Rigidbody>();

		if (newBody != null)
		{
			newBody.useGravity = true;
			bodies.Remove(newBody);
		}
	}

	private void FixedUpdate()
	{
		foreach(Rigidbody body in bodies)
		{
			Vector3 target = mainBullet.transform.position - body.position;
			body.AddForce(target * mainBullet.gravityPull);
		}
	}
}
