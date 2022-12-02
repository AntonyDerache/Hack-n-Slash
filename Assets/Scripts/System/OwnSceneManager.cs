using UnityEngine;
using UnityEngine.SceneManagement;

public class OwnSceneManager : Singleton<OwnSceneManager>
{
    public AsyncOperation LoadAsyncSceneAdditive(string sceneName)
    {
        return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public AsyncOperation LoadAsyncSceneAdditive(int sceneIndex)
    {
        return SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
    }

    public AsyncOperation UnloadAsyncSceneAdditive(string sceneName)
    {
        return SceneManager.UnloadSceneAsync(sceneName);
    }

    public AsyncOperation UnloadAsyncSceneAdditive(int sceneIndex)
    {
        return SceneManager.UnloadSceneAsync(sceneIndex);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // public void LoadSceneWithTransition(string sceneName)
    // {
    //     _sceneTransi.PerformTransition(sceneName);
    // }

    // public void LoadSceneWithTransition(int sceneIndex)
    // {
    //     // todo: implement
    //     SceneManager.LoadScene(sceneIndex);
    // }
}