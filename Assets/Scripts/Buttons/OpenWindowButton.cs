using UnityEngine;

public class OpenWindowButton : ButtonHandler
{
    private WindowController windowController;
    [SerializeField] private GameObject windowPrefab;

    private void Start()
    {
        // Попытка найти WindowController в сцене
        windowController = FindFirstObjectByType<WindowController>();
        if (windowController != null)
        {
            Debug.Log("[OpenWindowButton] WindowController найден: " + windowController.gameObject.name);
        }
        else
        {
            Debug.LogError("[OpenWindowButton] WindowController не найден!");
        }
    }

    public override void HandleClick()
    {
        Debug.Log("[OpenWindowButton] Кнопка нажата!");
        
        WindowBase windowBase = windowPrefab.GetComponent<WindowBase>();
        if (windowBase != null)
        {
            Debug.Log("[OpenWindowButton] Найден WindowBase с WindowId: " + windowBase.WindowId);
            windowController.OpenWindow(windowBase.WindowId);
            Debug.Log("[OpenWindowButton] Вызван OpenWindow с WindowId: " + windowBase.WindowId);
        }
        else
        {
            Debug.LogWarning("[OpenWindowButton] windowPrefab не содержит компонента WindowBase.");
        }
    }
}
