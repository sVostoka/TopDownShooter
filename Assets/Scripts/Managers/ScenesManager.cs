using UnityEngine.SceneManagement;
using static Enums;

public static class ScenesManager
{
    public static void ShowScene(Scenes scene, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
    {
        var sceneId = (int)scene;
        SceneManager.LoadScene(sceneId, loadSceneMode);
    }
}
