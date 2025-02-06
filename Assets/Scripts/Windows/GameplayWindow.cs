using UnityEngine;

public class GameplayWindow : WindowBase
{
    public override string WindowId => "GameplayWindow";

    public override void Show()
    {
        base.Show();
        Debug.Log("Gameplay Window is showed");
    }

    public override void Hide()
    {
        base.Hide();
        Debug.Log("Gameplay Window is hided");
    }
}