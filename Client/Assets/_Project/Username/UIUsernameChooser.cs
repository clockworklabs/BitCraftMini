using System;
using System.Collections;
using System.Collections.Generic;
using SpacetimeDB;
using TMPro;
using UnityEngine;

public class UIUsernameChooser : Singleton<UIUsernameChooser>
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_InputField _usernameField;
    [SerializeField] private UIErrorText _error;

    private bool _initialized = false;

    public void Show()
    {
        if (!_initialized)
        {
            _initialized = true;
            _panel.SetActive(true);
            CameraController.AddDisabler(GetHashCode());
        }
    }

    private void OnEnable()
    {
        _panel.SetActive(false);
    }

    public void ShowError(string error) => _error.ShowError(error);

    public void ButtonPressed()
    {
        var username = _usernameField.text;
        var player = PlayerComponent.FilterByUsername(username);
        if (player == null)
        {
            BitCraftMiniGameManager.instance.CreatePlayer(username);
            _panel.SetActive(false);
            CameraController.RemoveDisabler(GetHashCode());
        }
        else
        {
            _error.ShowError($"{username} is already in use.\nPlease try another username.");
        }
    }
}
