using UnityEngine;

public class MainSceneDecorator : WindowDecorator
{
    public MainSceneDecorator(WindowBase window) : base(window)
    {
    }

    public override void Show()
    {
        Debug.Log("MainSceneDecorator: Showing Window " + wrappedWindow.WindowId);
        base.Show();
    }

    public override void Hide()
    {
        Debug.Log("MainSceneDecorator: Hiding Window " + wrappedWindow.WindowId);
        base.Hide();
    }
}