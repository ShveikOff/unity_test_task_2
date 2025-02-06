using UnityEngine;

public class PauseWindow : WindowBase
{
    public override string WindowId => "PauseWindow";

    public override void Show()
    {
        base.Show();
        Debug.Log("Pause Window is showed");
    }

    public override void Hide()
    {
        base.Hide();
        Debug.Log("Pause Windowis is hided");
    }
}