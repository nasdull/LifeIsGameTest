using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    public Animator characterAnimator;

	[SerializeField] Button nextButton;
	[SerializeField] TextMeshProUGUI animationNameText;

	private string currentAnimation;

	private void Start()
	{
		animationNameText.text = "";
		nextButton.interactable = false;
	}

	public void SelectAnimation(string animationName)
	{
		if(currentAnimation != animationName)
		{
			characterAnimator.SetTrigger(animationName);
			currentAnimation = animationName;
			nextButton.interactable = true;
			animationNameText.text = animationName;
		}
	}

	public void SendSelectedAnimation()
	{
		TestManager.Instanse.selectedAnimationName = currentAnimation;
		TestManager.Instanse.LoadNextScene("FirstPerson");
	}
}
