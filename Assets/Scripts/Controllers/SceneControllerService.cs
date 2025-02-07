using UnityEngine;

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

    public void SubscribeToProgressChanged(System.Action<float> callback)
    {
        loadSceneController.OnProgressChanged += callback;
    }

    public void UnsubscribeFromProgressChanged(System.Action<float> callback)
    {
        loadSceneController.OnProgressChanged -= callback;
    }
}
