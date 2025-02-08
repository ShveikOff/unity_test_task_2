using UnityEngine;

public class CloseWindowButton : ButtonHandler
{
    private WindowController windowController;

    private void Start()
    {
        windowController = FindFirstObjectByType<WindowController>();
    }

    public override void HandleClick()
    {
        WindowBase windowBase = GetComponentInParent<WindowBase>();
        if (windowBase != null && windowController != null)
        {
            windowController.CloseWindow(windowBase.WindowId);
        }
    }
}
