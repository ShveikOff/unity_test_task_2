using UnityEngine;

public class MenuWindow : WindowBase
{
    public override string WindowId => "MenuWindow";
    
    public override void Show()
    {
        base.Show();
        Debug.Log("Menu Window is showed");
    }

    public override void Hide()
    {
        base.Hide();
        Debug.Log("Menu Window is hided");
    }
}