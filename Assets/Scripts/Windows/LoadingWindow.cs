using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWindow : WindowBase
{
    public override string WindowId => "LoadingWindow";

    [SerializeField] private Slider progressBar;

    private void Start()
    {
        Debug.Log("[LoadingWindow] WindowId: " + WindowId);
    }

    private void OnEnable() {
        SceneControllerService sceneService = Resources.Load<SceneControllerService>("SceneControllerServiceAsset");
        sceneService.SubscribeToProgressChanged(UpdateProgress);
    }

    private void OnDisable() {
        SceneControllerService sceneService = Resources.Load<SceneControllerService>("SceneControllerServiceAsset");
        sceneService.UnsubscribeFromProgressChanged(UpdateProgress);
    }

    private void UpdateProgress(float progress)
    {
        progressBar.value = progress;
    }

    public override void Show()
    {
        base.Show();
        Debug.Log("Loading Window is showed");
    }

    public override void Hide()
    {
        base.Hide();
        Debug.Log("Loading Window is hided");
    }
}