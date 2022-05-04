using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurtainControll : MonoBehaviour
{
	public delegate void CurtainEventDelegate();
	public event CurtainEventDelegate OnClosed;
	public event CurtainEventDelegate OnOpened;

	public float fadeSpeed = 0.0f;

	[SerializeField] Image curtainImage;

	private Action callback;

	private void Start()
	{
		curtainImage.color = new Color(curtainImage.color.r, curtainImage.color.g, curtainImage.color.b, 1.0f);
		curtainImage.raycastTarget = true;
	}

	public void OpenCurtain(Action action = null)
	{
		callback = action;
		StopAllCoroutines();
		StartCoroutine(TriggerCurtain(false));
	}

	public void CloseCurtain(Action action = null)
	{
		callback = action;
		StopAllCoroutines();
		StartCoroutine(TriggerCurtain(true));
	}

	IEnumerator TriggerCurtain(bool close)
	{
		Color currentColor;

		if (close)
		{
			curtainImage.raycastTarget = true;
			while (true)
			{
				currentColor = curtainImage.color;
				currentColor.a += fadeSpeed * Time.deltaTime;
				curtainImage.color = currentColor;

				if(curtainImage.color.a >= 1.0f)
				{
					currentColor = curtainImage.color;
					currentColor.a = 1.0f;
					curtainImage.color = currentColor;

					OnClosed?.Invoke();
					callback?.Invoke();
					break;
				}

				yield return null;
			}
		}
		else
		{
			while (true)
			{
				currentColor = curtainImage.color;
				currentColor.a -= fadeSpeed * Time.deltaTime;
				curtainImage.color = currentColor;

				if (curtainImage.color.a <= 0.0f)
				{
					currentColor = curtainImage.color;
					currentColor.a = 0.0f;
					curtainImage.color = currentColor;
					curtainImage.raycastTarget = false;

					OnOpened?.Invoke();
					callback?.Invoke();
					break;
				}

				yield return null;
			}
		}
	}
}
