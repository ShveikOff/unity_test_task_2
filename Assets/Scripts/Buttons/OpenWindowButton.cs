using UnityEngine;

public class OpenWindowButton : ButtonHandler
{
    private WindowController windowController;
    [SerializeField] private GameObject windowPrefab;

    private void Start()
    {
        windowController = FindFirstObjectByType<WindowController>();
    }

    public override void HandleClick()
    {
        WindowBase windowBase = windowPrefab.GetComponent<WindowBase>();
        if (windowBase != null)
        {
            windowController.OpenWindow(windowBase.WindowId);
        }
    }
}
