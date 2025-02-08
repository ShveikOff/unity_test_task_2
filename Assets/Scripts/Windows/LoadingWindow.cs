using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWindow : WindowBase
{
    public override string WindowId => "LoadingWindow";

    [SerializeField] private Slider progressBar;

    private bool isInitialized = false; 
    private SceneControllerService sceneService;

    private void Awake()
    {
        // Пример инициализации сервиса один раз при создании окна
        sceneService = Resources.Load<SceneControllerService>("SceneControllerServiceAsset");

        if (sceneService == null)
        {
            Debug.LogError("Не удалось загрузить SceneControllerService.");
        }
    }

    private void Start()
    {
        Debug.Log("[LoadingWindow] WindowId: " + WindowId);

        // Флаг готовности окна
        isInitialized = true;
    }

    private void OnEnable()
    {
        if (sceneService != null)
        {
            sceneService.SubscribeToProgressChanged(UpdateProgress);
        }
        else
        {
            Debug.LogError("SceneControllerService не найден при OnEnable.");
        }
    }

    private void OnDisable()
    {
        if (sceneService != null)
        {
            sceneService.UnsubscribeFromProgressChanged(UpdateProgress);
        }
        else
        {
            Debug.LogWarning("SceneControllerService не найден при OnDisable.");
        }
    }

    public void UpdateProgress(float progress)
    {
        if (!isInitialized)
        {
            Debug.LogError("Окно LoadingWindow еще не инициализировано!");
            return;
        }

        if (progressBar == null)
        {
            Debug.LogError("progressBar не инициализирован!");
            return;
        }

        // Обновление прогресса
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
