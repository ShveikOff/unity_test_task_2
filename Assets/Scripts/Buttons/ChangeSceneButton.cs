using UnityEngine;

public class ChangeSceneButton : ButtonHandler
{
    [SerializeField] private GameObject targetWindowPrefab;
    [SerializeField] private SceneReference targetSceneReference;

    public override void HandleClick()
    {
        WindowBase windowBase = targetWindowPrefab.GetComponent<WindowBase>();
        if (windowBase == null)
        {
            Debug.LogError("[ChangeSceneButton] Префаб окна не содержит компонент WindowBase.");
            return;
        }
        string windowId = windowBase.WindowId;

        if (targetSceneReference == null || string.IsNullOrEmpty(targetSceneReference.sceneName))
        {
            Debug.LogError("[ChangeSceneButton] SceneReference не указана или содержит пустое имя.");
            return;
        }
        string sceneName = targetSceneReference.sceneName;

        SceneControllerService sceneService = Resources.Load<SceneControllerService>("SceneControllerServiceAsset");
        if (sceneService != null)
        {
            sceneService.LoadSceneWithWindow(sceneName, windowId);
        }
        else
        {
            Debug.LogError("[ChangeSceneButton] SceneControllerServiceAsset не найден в Resources.");
        }
    }
}
