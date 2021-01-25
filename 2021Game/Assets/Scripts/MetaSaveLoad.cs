using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaSaveLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);		
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown("space"))
        {
			StartCoroutine("UnloadGameLevel");
        }
    }
	
	
	IEnumerator UnloadGameLevel() 
	{
		// Unload the level specific objects.
		AsyncOperation UnloadGameLevel = SceneManager.UnloadSceneAsync(3);
		// Dont unload persistent objects until level specific objects are done,
		// as level specific objects may reference persistent objects.
		while (!UnloadGameLevel.isDone)
		{
			yield return null;
		}
		
		// Unload the level persistent objects.
		AsyncOperation UnloadGameMeta = SceneManager.UnloadSceneAsync(2);
		while (!UnloadGameMeta.isDone)
		{
			yield return null;
		}	
		
		// Only load the main menu once everything else has been cleaned up.
		SceneManager.LoadScene(1, LoadSceneMode.Additive);		
		yield return null;
	}
}

