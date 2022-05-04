using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    public string startingScene;
	[SerializeField] private CurtainControll curtain;

	[HideInInspector] public string selectedAnimationName;

	private string sceneToLoad;
	private string currentScene;

	public static TestManager Instanse;

	private void Awake()
	{
		if (Instanse == null)
			Instanse = this;
		else
			Destroy(gameObject);
	}

	private void Start()
	{

#if UNITY_EDITOR
		int sceneCount = SceneManager.sceneCount;
		for (int i = 0; i < sceneCount; i++)
		{
			Scene currentScene = SceneManager.GetSceneAt(i);

			if (currentScene.name != "GameManager")
				SceneManager.UnloadSceneAsync(currentScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
		}
#endif
		LoadNextScene(startingScene);
	}

	public void LoadNextScene(string sceneName)
	{
		sceneToLoad = sceneName;
		curtain.CloseCurtain(LoadScene);
	}

	void LoadScene()
	{
		SceneManager.sceneLoaded += LoadedScene;

		if(SceneManager.sceneCount > 1)
			SceneManager.UnloadSceneAsync(currentScene);

		SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
	}

	void LoadedScene(Scene loadedScene, LoadSceneMode loadMode)
	{
		SceneManager.sceneLoaded -= LoadedScene;
		SceneManager.SetActiveScene(loadedScene);
		currentScene = loadedScene.name;
		curtain.OpenCurtain();
	}
}
