using UnityEngine;

public class SettingsWindow : WindowBase
{
    public override string WindowId => "SettingsWindow";

    private void Start()
    {
        Debug.Log("[SettingsWindow] WindowId: " + WindowId);
    }

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