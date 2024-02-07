using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class DebugTools : MonoBehaviour
{
    [SerializeField]
    bool _isGuysDisabled;

    void Start()
    {

    }

#if UNITY_EDITOR
    void Update()
    {
        // disable guys
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _isGuysDisabled = !_isGuysDisabled;
            var guys = FindObjectsOfType<FollowAI>();
            foreach (var guy in guys)
            {
                guy.Kill(_isGuysDisabled ? 10000 : 0);
            }
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new()
        {

            fontSize = 32,
        };

        if (_isGuysDisabled)
        {
            GUI.Label(new Rect(10, 10, 500, 500), "Guys disabled!", style);
        }
    }
#endif
}
