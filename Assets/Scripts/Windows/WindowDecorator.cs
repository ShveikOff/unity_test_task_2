using UnityEngine;

public abstract class WindowDecorator : WindowBase
{
    protected WindowBase wrappedWindow;

    public WindowDecorator(WindowBase window)
    {
        wrappedWindow = window;
    }

    public override string WindowId => wrappedWindow.WindowId;

    public override void Show()
    {
        wrappedWindow.Show();
    }

    public override void Hide()
    {
        wrappedWindow.Hide();
    }
}