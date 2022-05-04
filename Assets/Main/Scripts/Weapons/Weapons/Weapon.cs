using System.Collections;
using UnityEngine;

public enum TypeOfWeapon
{
    unlimited,
    limited
}

public class Weapon : MonoBehaviour
{
    public delegate void WeaponEventsDelegate();
    public event WeaponEventsDelegate CanFireAgain;
    public event WeaponEventsDelegate OnFire;

    public string weaponName;
    public TypeOfWeapon type;
    public int damage;
    public float bulletVelocity;
    public float fireRate;
    public int startingBulletAmount;
    public float bulletLifetime;
    public GameObject fireEffectPrefab;
    public Transform bulletsClip;
    public Bullet[] bulletPrefab;
    public Transform[] bulletSpawnPoints;

    private int currentBulletAmount;
    public int CurrentBullets { get { return currentBulletAmount; } }

    protected bool canFire = true;    

    public virtual void Awake()
    {
        currentBulletAmount = startingBulletAmount;
        if(bulletsClip != null)
            bulletsClip.SetParent(null);
    }

    private void OnEnable()
    {
        canFire = true;
    }

	private void OnDisable()
	{
        if(bulletsClip != null)
            Destroy(bulletsClip.gameObject);
	}

	private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }

    public virtual void FireWeapon()
    {
        if (canFire)
        {
            canFire = false;
			for (int i = 0; i < bulletSpawnPoints.Length; i++)
			{
				foreach (Bullet bullet in bulletPrefab)
				{
					if (!bullet.gameObject.activeSelf)
					{
						bullet.InitializeBullet(bulletVelocity, bulletSpawnPoints[i].position, bulletSpawnPoints[i].rotation, bulletLifetime);
						break;
					}
                }                

                if (fireEffectPrefab)
                {
                    Transform effect = Instantiate(fireEffectPrefab, bulletSpawnPoints[i].position, bulletSpawnPoints[i].rotation).transform;
                    effect.SetParent(transform);
                }
            }

            if (type == TypeOfWeapon.limited)
                currentBulletAmount -= 1;

            if (fireRate > 0.0f)
                StartCoroutine(CheckFireRate());
            else
                canFire = true;
            
            OnFire?.Invoke();
        }
    }

    protected IEnumerator CheckFireRate()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
        CanFireAgain?.Invoke();
    }
}
