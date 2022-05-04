using UnityEngine;

public class FirstPersonManager : MonoBehaviour
{
    [SerializeField] Animator animatedCharacter;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;

		if(TestManager.Instanse != null)
			animatedCharacter.SetTrigger(TestManager.Instanse.selectedAnimationName);
	}

	private void OnDestroy()
	{
		Cursor.lockState = CursorLockMode.None;
	}
}