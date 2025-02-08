using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    [SerializeField] private List<GameObject> windowPrefabs;
    private Dictionary<string, WindowBase> loadedWindows = new Dictionary<string, WindowBase>();

    public event Action<string> OnWindowOpened;
    public event Action<string> OnWindowClosed;

    public void OpenWindow(string windowId)
    {
        if (!loadedWindows.ContainsKey(windowId))
        {
            GameObject prefab = windowPrefabs.Find(p =>
            {
                WindowBase wb = p.GetComponent<WindowBase>();
                return wb != null && wb.WindowId == windowId;
            });

            if (prefab != null)
            {
                GameObject newWindow = Instantiate(prefab, transform);
                WindowBase wbNew = newWindow.GetComponent<WindowBase>();
                if (wbNew != null)
                {
                    loadedWindows[windowId] = wbNew;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
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

    public void CloseAllWindows()
    {
        List<string> keys = new List<string>(loadedWindows.Keys);
        foreach (string id in keys)
        {
            if (id != "MenuWindow")
            {
                loadedWindows[id].Hide();
                StartCoroutine(InvokeOnWindowClosed(id));
            }
        }
    }
}
