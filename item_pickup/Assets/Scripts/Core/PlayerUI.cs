using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject _defaultCrosshair, _pickupCrosshair, _draggingCrosshair;

    PlayerInteraction _interaction;
    void Start()
    {
        _defaultCrosshair.SetActive(true);
        _pickupCrosshair.SetActive(false);
        _draggingCrosshair.SetActive(false);

        _interaction = GetComponent<PlayerInteraction>();
    }

    void Update()
    {
        if (_interaction.CurrentInteraction == null)
        {
            _defaultCrosshair.SetActive(!_interaction.CanPickup);
            _pickupCrosshair.SetActive(_interaction.CanPickup);
            _draggingCrosshair.SetActive(false);
        }
        else
        {
            _defaultCrosshair.SetActive(false);
            _pickupCrosshair.SetActive(false);
            _draggingCrosshair.SetActive(true);
        }
    }
}
