using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public bool isDestroyable;
    [SerializeField] protected Rigidbody bulletRigidBody;

	public virtual void OnTriggerEnter(Collider other)
	{
		if(isDestroyable)
			gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	public virtual void InitializeBullet(float velocity, Vector3 position, Quaternion rotation, float lifeTime)
	{
		transform.position = position;
		transform.rotation = rotation;
		gameObject.SetActive(true);
		bulletRigidBody.velocity = transform.forward * velocity;

		if(lifeTime > 0.0f)
			StartCoroutine(LifeTime(lifeTime));
	}

	IEnumerator LifeTime(float time)
	{
		yield return new WaitForSeconds(time);
		gameObject.SetActive(false);
	}
}
