using UnityEngine;

public class StartWindowInitializer : MonoBehaviour
{
    [SerializeField] private string initialWindowId = "MenuWindow";

    private void Start()
    {
        WindowController windowController = FindFirstObjectByType<WindowController>();
        if (windowController != null)
        {
            Debug.Log($"[StartWindowInitializer] Открываем стартовое окно с ID: {initialWindowId}");
            windowController.OpenWindow(initialWindowId);
        }
        else
        {
            Debug.LogError("[StartWindowInitializer] WindowController не найден в текущей сцене.");
        }
    }
}
