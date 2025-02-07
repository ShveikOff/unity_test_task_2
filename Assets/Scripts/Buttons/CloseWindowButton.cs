using UnityEngine;

public class CloseWindowButton : ButtonHandler
{
    private WindowController windowController;

    private void Start()
    {
        // Попытка найти WindowController в сцене
        windowController = FindFirstObjectByType<WindowController>();
        if (windowController != null)
        {
            Debug.Log("[CloseWindowButton] WindowController найден: " + windowController.gameObject.name);
        }
        else
        {
            Debug.LogError("[CloseWindowButton] WindowController не найден!");
        }
    }

    public override void HandleClick()
    {
        Debug.Log("[CloseWindowButton] HandleClick вызван на объекте: " + gameObject.name);
        
        // Пытаемся получить WindowBase из родительских объектов
        WindowBase windowBase = GetComponentInParent<WindowBase>();
        if (windowBase != null)
        {
            Debug.Log("[CloseWindowButton] Найден WindowBase с WindowId: " + windowBase.WindowId);
            if (windowController != null)
            {
                windowController.CloseWindow(windowBase.WindowId);
                Debug.Log("[CloseWindowButton] Вызван OpenWindow с WindowId: " + windowBase.WindowId);
            }
            else
            {
                Debug.LogError("[CloseWindowButton] Не удалось вызвать OpenWindow, т.к. windowController равен null.");
            }
        }
        else
        {
            Debug.LogWarning("[CloseWindowButton] Компонент WindowBase не найден среди родительских объектов для: " + gameObject.name);
        }
    }
}
