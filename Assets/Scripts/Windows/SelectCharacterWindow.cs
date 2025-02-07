using UnityEngine;

public class SelectCharacterWindow : WindowBase
{
    public override string WindowId => "SelectCharacterWindow";

    private void Start()
    {
        Debug.Log("[SelectCharacterWindow] WindowId: " + WindowId);
    }

    public override void Show()
    {
        base.Show();
        Debug.Log("SelectCharacter Window is showed");
    }

    public override void Hide()
    {
        base.Hide();
        Debug.Log("SelectCharacter Window is hided");
    }
}