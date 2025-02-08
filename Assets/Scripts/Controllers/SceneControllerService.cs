using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[CreateAssetMenu(fileName = "SceneControllerService", menuName = "ScriptableObjects/SceneControllerService")]
public class SceneControllerService : ScriptableObject
{
    private LoadSceneController loadSceneController;

    private void OnEnable() 
    {
        loadSceneController = new LoadSceneController();    
    }

    public void LoadScene(string sceneName)
    {
        loadSceneController.LoadScene(sceneName);
    }

    public void LoadSceneWithWindow(string targetSceneId, string targetWindowId)
    {
        CoroutineRunner.Instance.StartCoroutine(LoadSceneWithWindowCoroutine(targetSceneId, targetWindowId));
    }

    private IEnumerator LoadSceneWithWindowCoroutine(string targetSceneId, string targetWindowId)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        yield return CloseAllWindowsInSceneCoroutine(currentScene.name);

        Scene loadingScene = SceneManager.GetSceneByName("LoadingScene");
        if (!loadingScene.isLoaded)
        {
            AsyncOperation loadingSceneOp = SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
            yield return new WaitUntil(() => loadingSceneOp.isDone);
        }

        WindowController loadingWC = FindWindowControllerInScene("LoadingScene");
        if (loadingWC == null)
        {
            yield break;
        }

        loadingWC.OpenWindow("LoadingWindow");

        yield return null;

        Scene targetScene = SceneManager.GetSceneByName(targetSceneId);
        if (!targetScene.isLoaded)
        {
            AsyncOperation targetSceneOp = SceneManager.LoadSceneAsync(targetSceneId, LoadSceneMode.Additive);
            targetSceneOp.allowSceneActivation = false;

            while (targetSceneOp.progress < 0.9f)
            {
                loadSceneController.ReportProgress(targetSceneOp.progress);
                yield return null;
            }

            targetSceneOp.allowSceneActivation = true;
            yield return new WaitUntil(() => targetSceneOp.isDone);
        }

        loadSceneController.ReportProgress(1f);

        WindowController targetWC = FindWindowControllerInScene(targetSceneId);
        if (targetWC == null)
        {
            yield break;
        }

        targetWC.OpenWindow(targetWindowId);

        loadingWC.CloseWindow("LoadingWindow");

        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync("LoadingScene");
        yield return new WaitUntil(() => unloadOp.isDone);
    }

    private WindowController FindWindowControllerInScene(string sceneName)
    {
        WindowController[] controllers = GameObject.FindObjectsOfType<WindowController>();
        foreach (var controller in controllers)
        {
            if (controller.gameObject.scene.name == sceneName)
                return controller;
        }
        return null;
    }

    private void CloseAllWindowsInScene(string sceneName)
    {
        WindowController[] controllers = GameObject.FindObjectsOfType<WindowController>();
        foreach (var controller in controllers)
        {
            if (controller.gameObject.scene.name == sceneName)
            {
                controller.CloseAllWindows();
            }
        }
    }

    private IEnumerator CloseAllWindowsInSceneCoroutine(string sceneName)
    {
        bool anyWindowClosed = false;
        WindowController[] controllers = GameObject.FindObjectsOfType<WindowController>();

        foreach (var controller in controllers)
        {
            if (controller.gameObject.scene.name == sceneName)
            {
                controller.CloseAllWindows();
                anyWindowClosed = true;
            }
        }

        if (anyWindowClosed)
        {
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    public void SubscribeToProgressChanged(System.Action<float> callback)
    {
        loadSceneController.OnProgressChanged += callback;
    }

    public void UnsubscribeFromProgressChanged(System.Action<float> callback)
    {
        loadSceneController.OnProgressChanged -= callback;
    }
}
