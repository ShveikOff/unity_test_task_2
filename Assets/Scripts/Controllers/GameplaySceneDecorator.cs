using UnityEngine;

public class GameplaySceneDecorator : WindowDecorator
{
    public GameplaySceneDecorator (WindowBase window) : base(window)
    {
    }

    public override void Show()
    {
        Debug.Log("GameplaySceneDecorator: Showing Window " + wrappedWindow.WindowId);
        base.Show();
    }

    public override void Hide()
    {
        Debug.Log("GameplaySceneDecorator: Hiding Window " + wrappedWindow.WindowId);
        base.Hide();
    }
}