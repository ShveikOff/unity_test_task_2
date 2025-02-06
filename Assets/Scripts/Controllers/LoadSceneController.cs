using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneCommand
{
    private string sceneName;
    public LoadSceneCommand(string sceneName) 
    {
        this.sceneName = sceneName;
    }

    public void Execute()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}

public class LoadSceneController
{
    public event Action<float> OnProgressChanged;

    public void LoadScene(string sceneName)
    {
        var command = new LoadSceneCommand(sceneName);
        command.Execute();
    }
}
