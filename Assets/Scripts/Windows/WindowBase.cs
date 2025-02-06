using UnityEngine;

public abstract class WindowBase : MonoBehaviour
{
    public abstract string WindowId {get; }
    public bool IsVisible {get; private set;}
    
    public virtual void Show()
    {
        gameObject.SetActive(true);
        IsVisible = true;
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        IsVisible = false;
    }
}
