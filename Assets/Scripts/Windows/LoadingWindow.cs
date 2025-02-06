using UnityEngine;

public class LoadingWindow : WindowBase
{
    public override string WindowId => "LoadingWindow";

    public override void Show()
    {
        base.Show();
        Debug.Log("Loading Window is showed");
    }

    public override void Hide()
    {
        base.Hide();
        Debug.Log("Loading Window is hided");
    }
}