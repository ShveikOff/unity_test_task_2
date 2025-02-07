using UnityEngine;

public class CloseAllWindowsButton : ButtonHandler
{
    private WindowController windowController;

    private void Start()
    {
        // Пытаемся найти WindowController в сцене
        windowController = FindFirstObjectByType<WindowController>();
        if (windowController != null)
        {
            Debug.Log("[CloseAllWindowsButton] WindowController найден: " + windowController.gameObject.name);
        }
        else
        {
            Debug.LogError("[CloseAllWindowsButton] WindowController не найден!");
        }
    }

    public override void HandleClick()
    {
        Debug.Log("[CloseAllWindowsButton] HandleClick вызван. Будем закрывать все окна, кроме MainWindow.");
        if (windowController != null)
        {
            windowController.CloseAllWindows();
        }
        else
        {
            Debug.LogError("[CloseAllWindowsButton] Невозможно закрыть окна, windowController равен null.");
        }
    }
}
