using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CharacterChange : MonoBehaviour
{
    [SerializeField]
    float _swingSpeed;
    bool _isRotate = true;

    [SerializeField]
    GameObject _confirmCharacterPanel;

    private void Start()
    {
        _confirmCharacterPanel.SetActive(false);
    }

    public void Switch()
    {
        _isRotate = !_isRotate;
        Camera.main.transform.DORotate(_isRotate ? 180 * Vector3.up : Vector3.zero, _swingSpeed);
    }

    public void SelectCharacter()
    {
        _confirmCharacterPanel.SetActive(true);
        // TODO: bring character to new scene where u can move
        // (zsfer): note. im too lazy to do this....
    }
}
