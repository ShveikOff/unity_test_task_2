using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour // Сделал класс MonoBehavior
{
    [SerializeField] private List<GameObject> windowPrefabs;
    private Dictionary<string, WindowBase> loadedWindows = new Dictionary<string, WindowBase>();

    public event Action<string> OnWindowOpened;
    public event Action<string> OnWindowClosed;

    public void OpenWindow(string windowId)
    {
        if (!loadedWindows.ContainsKey(windowId)) 
        {
            GameObject prefab = windowPrefabs.Find(p => p.GetComponent<WindowBase>().WindowId == windowId);
            if (prefab != null)
            {
                GameObject newWindow = Instantiate(prefab, transform);
                loadedWindows[windowId] = newWindow.GetComponent<WindowBase>();
            }
        }
        loadedWindows[windowId].Show();
        StartCoroutine(InvokeOnWindowOpened(windowId));
    }

    private IEnumerator InvokeOnWindowOpened(string windowId)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        OnWindowOpened?.Invoke(windowId);
    }

    public void CloseWindow(string windowId)
    {
        if (loadedWindows.ContainsKey(windowId))
        {
            loadedWindows[windowId].Hide();
            StartCoroutine(InvokeOnWindowClosed(windowId));
        }
    }

    private IEnumerator InvokeOnWindowClosed(string windowId)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        OnWindowClosed?.Invoke(windowId);
    }
    
}
