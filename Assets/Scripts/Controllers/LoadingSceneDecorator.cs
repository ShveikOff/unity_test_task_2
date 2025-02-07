using UnityEngine;

public class LoadingSceneDecorator : WindowDecorator
{
    public LoadingSceneDecorator (WindowBase window) : base(window)
    {
    }

    public override void Show()
    {
        Debug.Log("LoadingSceneDecorator: Showing Window " + wrappedWindow.WindowId);
        base.Show();
    }

    public override void Hide()
    {
        Debug.Log("LoadingSceneDecorator: Hiding Window " + wrappedWindow.WindowId);
        base.Hide();
    }
}