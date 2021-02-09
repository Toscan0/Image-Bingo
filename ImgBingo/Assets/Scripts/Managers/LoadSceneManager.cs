using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneManager : MonoBehaviour
{
    public void LoadNextScene()
    {
        LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void LoadSceneByIndex(int i)
    {
        SceneManager.LoadScene(i);
    }
}
