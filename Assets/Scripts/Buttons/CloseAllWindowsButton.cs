using UnityEngine;

public class CloseAllWindowsButton : ButtonHandler
{
    private WindowController windowController;

    private void Start()
    {
        windowController = FindFirstObjectByType<WindowController>();
    }

    public override void HandleClick()
    {
        if (windowController != null)
        {
            windowController.CloseAllWindows();
        }
    }
}
