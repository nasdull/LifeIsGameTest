using UnityEngine;

public class PlayerControll : MonoBehaviour
{
	public float walkSpeed = 0.0f;
	public float lookSpeed = 0.0f;
	[SerializeField] private Camera mainCam;
	[SerializeField] private CharacterController charControll;
	[SerializeField] private Transform gunPosition;

	private float vertRotation;
	private Vector3 currentMovement;
	private Weapon currentWeapon;

	private void OnTriggerEnter(Collider other)
	{
		WeaponItem pickedItem = other.GetComponent<WeaponItem>();

		if(pickedItem != null)
		{
			EquipWeapon(pickedItem.PickUpWeapon());
		}
	}

	private void EquipWeapon(Weapon selectedWeapon)
	{
		if (currentWeapon != null)
			Destroy(currentWeapon.gameObject);

		Weapon newWeapon = Instantiate(selectedWeapon, gunPosition.position, gunPosition.rotation, gunPosition);
		currentWeapon = newWeapon;
	}

	private void Look()
	{
		float xRotate = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
		vertRotation -= Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
		vertRotation = Mathf.Clamp(vertRotation, -90.0f, 90.0f);

		mainCam.transform.localRotation = Quaternion.Euler(vertRotation, 0.0f, 0.0f);
		transform.Rotate(Vector3.up * xRotate);
	}

	private void Movement()
	{
		Vector3 forwardMove = Input.GetAxis("Vertical") * transform.forward;
		Vector3 sideMove = Input.GetAxis("Horizontal") * transform.right;

		currentMovement = forwardMove + sideMove;
		currentMovement += new Vector3(0.0f, -9.81f, 0.0f) * Time.deltaTime;

		charControll.Move(currentMovement * walkSpeed * Time.deltaTime);
	}

	private void Update()
	{
		Look();
		Movement();

		if(Input.GetMouseButtonDown(0))
		{
			currentWeapon?.FireWeapon();
		}
	}
}