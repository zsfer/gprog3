using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(GameManager)), CanEditMultipleObjects]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager _g = target as GameManager;
        GUILayout.Label("Current Level: " + _g.Exp.CurrentLevel);
        GUILayout.Label("Current XP: " + _g.Exp.CurrentExp);
        GUILayout.Label("XP to level up: " + _g.Exp.MaxExp);

        if (GUILayout.Button("Gain Level +100"))
        {
            _g.Exp.AddExp(100);
        }

    }
}
