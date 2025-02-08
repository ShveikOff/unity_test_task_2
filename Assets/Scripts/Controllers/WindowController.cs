using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour // Сделал класс MonoBehaviour
{
    [SerializeField] private List<GameObject> windowPrefabs;
    private Dictionary<string, WindowBase> loadedWindows = new Dictionary<string, WindowBase>();

    public event Action<string> OnWindowOpened;
    public event Action<string> OnWindowClosed;

    public void OpenWindow(string windowId)
    {
        Debug.Log("[WindowController] OpenWindow вызван с windowId: " + windowId);
        
        if (!loadedWindows.ContainsKey(windowId))
        {
            Debug.Log("[WindowController] Окно с id " + windowId + " не найдено среди загруженных. Ищем префаб...");
            GameObject prefab = windowPrefabs.Find(p =>
            {
                WindowBase wb = p.GetComponent<WindowBase>();
                if (wb == null)
                {
                    Debug.LogWarning("[WindowController] Предупреждение: Префаб " + p.name + " не содержит WindowBase.");
                    return false;
                }
                return wb.WindowId == windowId;
            });

            if (prefab != null)
            {
                Debug.Log("[WindowController] Префаб найден: " + prefab.name + ". Инстанцируем окно...");
                GameObject newWindow = Instantiate(prefab, transform);
                WindowBase wbNew = newWindow.GetComponent<WindowBase>();
                if (wbNew != null)
                {
                    loadedWindows[windowId] = wbNew;
                    Debug.Log("[WindowController] Окно " + windowId + " успешно загружено и добавлено в loadedWindows.");
                }
                else
                {
                    Debug.LogError("[WindowController] Ошибка: Инстанцированный объект " + newWindow.name + " не содержит WindowBase.");
                    return;
                }
            }
            else
            {
                Debug.LogError("[WindowController] Ошибка: Префаб для окна с id " + windowId + " не найден.");
                return;
            }
        }
        else
        {
            Debug.Log("[WindowController] Окно с id " + windowId + " уже загружено. Просто показываем его.");
        }
        
        loadedWindows[windowId].Show();
        Debug.Log("[WindowController] Окно с id " + windowId + " показано (Show вызван).");
        StartCoroutine(InvokeOnWindowOpened(windowId));
    }

    private IEnumerator InvokeOnWindowOpened(string windowId)
    {
        Debug.Log("[WindowController] Запуск InvokeOnWindowOpened для " + windowId);
        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log("[WindowController] Завершена задержка для " + windowId + ". Вызываем событие OnWindowOpened.");
        OnWindowOpened?.Invoke(windowId);
    }

    public void CloseWindow(string windowId)
    {
        Debug.Log("[WindowController] CloseWindow вызван с windowId: " + windowId);
        if (loadedWindows.ContainsKey(windowId))
        {
            loadedWindows[windowId].Hide();
            Debug.Log("[WindowController] Окно с id " + windowId + " скрыто (Hide вызван).");
            StartCoroutine(InvokeOnWindowClosed(windowId));
        }
        else
        {
            Debug.LogWarning("[WindowController] CloseWindow: Окно с id " + windowId + " не найдено в loadedWindows.");
        }
    }

    private IEnumerator InvokeOnWindowClosed(string windowId)
    {
        Debug.Log("[WindowController] Запуск InvokeOnWindowClosed для " + windowId);
        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log("[WindowController] Завершена задержка для закрытия " + windowId + ". Вызываем событие OnWindowClosed.");
        OnWindowClosed?.Invoke(windowId);
    }

    public void CloseAllWindows()
    {
        Debug.Log("[WindowController] CloseAllWindows вызван. Закрываем все окна, кроме MainWindow.");
        // Получаем список ключей, чтобы безопасно итерировать по словарю
        List<string> keys = new List<string>(loadedWindows.Keys);
        foreach (string id in keys)
        {
            if (id != "MenuWindow") // Здесь замените "MainWindow" на нужное значение, если требуется
            {
                Debug.Log("[WindowController] Закрывается окно с id: " + id);
                loadedWindows[id].Hide();
                StartCoroutine(InvokeOnWindowClosed(id));
            }
            else
            {
                Debug.Log("[WindowController] Пропускаем окно с id: " + id);
            }
        }
    }
}
