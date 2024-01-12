using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparelSwitcher : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _apparels;

    int _currActive;

    private void Start()
    {
        if (_apparels.Count == 0)
        {
            foreach (Transform child in transform)
            {
                _apparels.Add(child.gameObject);
            }
        }

        UpdateVisual();
    }

    public void SwitchNext()
    {
        _currActive = (_currActive + 1) % _apparels.Count;
        print("Switch to " + name + " " + _currActive);

        UpdateVisual();
    }

    public void SwitchPrev()
    {
        _currActive = (_currActive - 1 + _apparels.Count) % _apparels.Count;
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        foreach (var apparel in _apparels)
        {
            apparel.SetActive(apparel == _apparels[_currActive]);
        }
    }
}
