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
        // Закрываем все окна на текущей активной сцене
        Scene currentScene = SceneManager.GetActiveScene();
        yield return CloseAllWindowsInSceneCoroutine(currentScene.name);

        // Загружаем сцену LoadingScene
        Debug.Log("[SceneControllerService] Загружается LoadingScene...");
        Scene loadingScene = SceneManager.GetSceneByName("LoadingScene");
        if (!loadingScene.isLoaded)
        {
            AsyncOperation loadingSceneOp = SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
            yield return new WaitUntil(() => loadingSceneOp.isDone);
            Debug.Log("[SceneControllerService] LoadingScene загружена.");
        }
        else
        {
            Debug.Log("[SceneControllerService] LoadingScene уже загружена.");
        }

        // Находим WindowController в LoadingScene
        WindowController loadingWC = FindWindowControllerInScene("LoadingScene");
        if (loadingWC == null)
        {
            Debug.LogError("[SceneControllerService] WindowController не найден в LoadingScene.");
            yield break;
        }

        loadingWC.OpenWindow("LoadingWindow");
        Debug.Log("[SceneControllerService] LoadingWindow открыт.");

        yield return null;

        // Проверяем, загружена ли целевая сцена
        Scene targetScene = SceneManager.GetSceneByName(targetSceneId);
        if (!targetScene.isLoaded)
        {
            Debug.Log($"[SceneControllerService] Начинается загрузка целевой сцены: {targetSceneId}...");
            AsyncOperation targetSceneOp = SceneManager.LoadSceneAsync(targetSceneId, LoadSceneMode.Additive);
            targetSceneOp.allowSceneActivation = false;

            while (targetSceneOp.progress < 0.9f)
            {
                float progress = targetSceneOp.progress;
                Debug.Log($"[SceneControllerService] Прогресс загрузки целевой сцены: {progress}");
                loadSceneController.ReportProgress(progress);
                yield return null;
            }

            targetSceneOp.allowSceneActivation = true;
            yield return new WaitUntil(() => targetSceneOp.isDone);
            Debug.Log("[SceneControllerService] Целевая сцена загружена.");
        }
        else
        {
            Debug.Log($"[SceneControllerService] Сцена {targetSceneId} уже загружена.");
        }

        loadSceneController.ReportProgress(1f);

        // Открываем нужное окно в целевой сцене
        WindowController targetWC = FindWindowControllerInScene(targetSceneId);
        if (targetWC == null)
        {
            Debug.LogError("[SceneControllerService] WindowController не найден в целевой сцене.");
            yield break;
        }

        targetWC.OpenWindow(targetWindowId);
        Debug.Log($"[SceneControllerService] Открыто окно {targetWindowId} в целевой сцене.");

        // Закрываем окно загрузки в LoadingScene
        loadingWC.CloseWindow("LoadingWindow");
        Debug.Log("[SceneControllerService] LoadingWindow закрыт.");

        // Выгружаем LoadingScene
        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync("LoadingScene");
        yield return new WaitUntil(() => unloadOp.isDone);
        Debug.Log("[SceneControllerService] LoadingScene выгружена.");
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

    /// <summary>
    /// Закрывает все окна на сцене с указанным именем.
    /// </summary>
    private void CloseAllWindowsInScene(string sceneName)
    {
        Debug.Log($"[SceneControllerService] Закрытие всех окон на сцене: {sceneName}");
        WindowController[] controllers = GameObject.FindObjectsOfType<WindowController>();

        foreach (var controller in controllers)
        {
            if (controller.gameObject.scene.name == sceneName)
            {
                controller.CloseAllWindows();
                Debug.Log($"[SceneControllerService] Все окна на сцене {sceneName} закрыты.");
            }
        }
    }

    private IEnumerator CloseAllWindowsInSceneCoroutine(string sceneName)
    {
        Debug.Log($"[SceneControllerService] Закрытие всех окон на сцене: {sceneName}");

        bool anyWindowClosed = false;
        WindowController[] controllers = GameObject.FindObjectsOfType<WindowController>();

        foreach (var controller in controllers)
        {
            if (controller.gameObject.scene.name == sceneName)
            {
                controller.CloseAllWindows();
                Debug.Log($"[SceneControllerService] Закрыто все окна для {controller.name} на сцене {sceneName}.");
                anyWindowClosed = true;
            }
        }

        if (anyWindowClosed)
        {
            // Даем время всем окнам полностью закрыться
            yield return new WaitForSeconds(0.5f);  // Можно настроить это время по необходимости
        }
        else
        {
            Debug.LogWarning($"[SceneControllerService] Окна для закрытия на сцене {sceneName} не найдены.");
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
