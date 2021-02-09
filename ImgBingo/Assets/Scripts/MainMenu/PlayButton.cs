using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField]
    private LoadSceneManager loadSceneManager;

    public void PlayGame()
    {
        loadSceneManager.LoadNextScene();
    }
}