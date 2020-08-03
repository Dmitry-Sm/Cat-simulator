using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


[CustomEditor(typeof(Actions))]
public class Table : Editor
{
    private string new_action = "";
    private string new_mood = "";
    public Actions actions;

    private void OnEnable() 
    {
        if (target)
        {
            actions = (Actions)target;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (actions.effect_by_action_and_mood.Count == 0) {
            GUILayout.Label("List is empty");
            if (GUILayout.Button("Init"))
            {
                actions.Init();
            }
            return;
        }

        float sw = EditorGUIUtility.currentViewWidth/(actions.moods.Count + 1) - 10;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;

        GUILayout.BeginVertical("Box");

        GUILayout.BeginHorizontal();
        GUILayout.Space(sw);
        GUILayout.FlexibleSpace();
        foreach (var mood in actions.moods)
        {
            GUILayout.Label(mood, GUILayout.Width(sw));
        }
        GUILayout.EndHorizontal();
        GUI.skin.label.alignment = TextAnchor.MiddleRight;

        foreach (var a in actions.actions)
        {
            GUILayout.BeginHorizontal("Box");
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                actions.RemoveAction(a);
                return;
            }
            GUILayout.Label(a, GUILayout.Width(sw - 25));
            foreach (var m in actions.moods)
            {
                var e = actions.effect_by_action_and_mood[actions.GetKey(a, m)];
                GUILayout.BeginVertical();
                e.reaction = EditorGUILayout.TextArea(e.reaction, GUILayout.Width(sw));
                e.next_mood = EditorGUILayout.Popup(e.next_mood, actions.moods.ToArray(), GUILayout.Width(sw));
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
        }
        
        GUILayout.BeginHorizontal();
        GUILayout.Space(sw + 5);
        foreach (var mood in actions.moods)
        {
            actions.moodIcons[mood] = (Sprite)EditorGUILayout.ObjectField("", actions.moodIcons[mood], typeof(Sprite), false, GUILayout.MaxWidth(sw));
        }
        GUILayout.EndHorizontal();   

        GUILayout.BeginHorizontal();
        GUILayout.Space(sw + 5);
        GUILayout.FlexibleSpace();
        foreach (var mood in actions.moods)
        {
            if (GUILayout.Button("X", GUILayout.Width(sw)))
            {
                actions.RemoveMood(mood);
                return;
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Добавить действие:");
        new_action = GUILayout.TextField(new_action, GUILayout.Width(sw));
        if (GUILayout.Button("Добавить", GUILayout.Width(sw)))
        {
            actions.AddAction(new_action);
            new_action = "";
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Добавить настроение:");
        new_mood = GUILayout.TextField(new_mood, GUILayout.Width(sw));
        if (GUILayout.Button("Добавить", GUILayout.Width(sw)))
        {
            actions.AddMood(new_mood);
            new_mood = "";
        }
        GUILayout.EndHorizontal();

        if (GUI.changed) 
        {
            SetObjectDitry(actions.gameObject);
        }
    }

    public static void SetObjectDitry(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}
