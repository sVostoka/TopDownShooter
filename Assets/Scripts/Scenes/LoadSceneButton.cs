using System;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private Scenes _loadableScene;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        ScenesManager.ShowScene(_loadableScene);
    }
}
