using System.Collections.Generic;
using System;
using UnityEngine;

public class PlacementSettingsBlackboard : MonoBehaviour,
    IBlackboardForUI,
    ISliderReceiver<MyVariableOptions>,
    IButtonReceiver<MyButtonOptions>
{
    public Action<int, Vector2Int> OnApplyChanges;

    private int _tileSize;
    private Vector2Int _tileVector;

    [SerializeField] private Dictionary<MyVariableOptions, Action<float>> _sliderActions = new();
    [SerializeField] private Dictionary<MyButtonOptions, Action> _buttonActions = new();

    [SerializeField] private List<SliderRelay<MyVariableOptions>> _sliderRelays = new();
    [SerializeField] private List<ButtonRelay<MyButtonOptions>> _buttonRelays = new();


    #region Initialize Action Dictionaries
    public void InitializeAllActions()
    {
        InitializeSliderActions();
        InitializeButtonActions();
    }
    public void InitializeButtonActions()
    {
        _buttonActions = new Dictionary<MyButtonOptions, Action>
        {
            { MyButtonOptions.ApplyChanges, () => OnApplyChanges?.Invoke(_tileSize, _tileVector) },
            // Add more here..
        };
    }
    public void InitializeSliderActions()
    {
        _sliderActions = new Dictionary<MyVariableOptions, Action<float>>
        {
            { MyVariableOptions.TileSize, val => _tileSize = (int)val },
            { MyVariableOptions.TileAmountX, val => _tileVector = new Vector2Int((int)val, _tileVector.y) },
            { MyVariableOptions.TileAmountY, val => _tileVector = new Vector2Int(_tileVector.x, (int)val) },
            // Add more here..
        };
    }
    #endregion

    #region Start Listening to Relays
    public void StartAllRelayListening()
    {
        StartSliderRelaysListening();
        StartButtonRelaysListening();
    }
    public void StartSliderRelaysListening()
    {
        if (_sliderRelays.Count == 0)
            return;

        foreach (var relay in _sliderRelays)
        {
            if (_sliderActions.TryGetValue(relay.givenType, out var action))
            {
                // Optional immediately set value from current slider.
                action.Invoke(relay.slider.value);

                // Add the listener.
                relay.slider.onValueChanged.AddListener(value => action.Invoke(value));
            }
        }
    }
    public void StartButtonRelaysListening()
    {
        if (_buttonRelays.Count == 0)
            return;

        foreach (var relay in _buttonRelays)
        {
            if (_buttonActions.TryGetValue(relay.givenType, out var action))
            {
                // Add the listener.
                relay.button.onClick.AddListener(() => action());
            }
        }
    }
    #endregion

    #region Stop Listening to Relays
    public void StopAllRelayListening()
    {
        StopSliderRelaysListening();
        StopButtonRelaysListening();
    }
    public void StopSliderRelaysListening()
    {
        if (_sliderRelays.Count == 0)
            return;

        foreach (var relay in _sliderRelays)
        {
            if (_sliderActions.TryGetValue(relay.givenType, out var action))
            {
                // Add the listener.
                relay.slider.onValueChanged.RemoveListener(value => action.Invoke(value));
            }
        }
    }
    public void StopButtonRelaysListening()
    {
        if (_buttonRelays.Count == 0)
            return;

        foreach (var relay in _buttonRelays)
        {
            if (_buttonActions.TryGetValue(relay.givenType, out var action))
            {
                // Remove the listener.
                relay.button.onClick.RemoveListener(() => action());
            }
        }
    }
    #endregion
}