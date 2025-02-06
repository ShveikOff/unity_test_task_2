using UnityEngine;

public class SettingsWindow : WindowBase
{
    public override string WindowId => "SettingsWindow";

    public override void Show()
    {
        base.Show();
        Debug.Log("Settings Window is showed");
    }

    public override void Hide()
    {
        base.Hide();
        Debug.Log("Settings Window is hided");
    }
}