using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelEditor : EditorWindow
{
    public static bool isRaodEditorOn = false;
    public static int index = 0;
    public static List<GameObject> prefabs = new List<GameObject>();
    static List<String> prefabsName = new List<String>();

    internal static GameObject GetObjectToCreate()
    {
        if(prefabs.Count > 0)
        {
            return prefabs[index];
        }
        return null;
    }

    [MenuItem("Window/LevelEditor")]
    static void Init()
    {
        LevelEditor levelEditor = (LevelEditor)EditorWindow.GetWindow(typeof(LevelEditor));
        levelEditor.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("RoadEditor", EditorStyles.boldLabel);
        isRaodEditorOn = EditorGUILayout.BeginToggleGroup("Enable Road Editor", isRaodEditorOn);
        if (GUILayout.Button("Get prefabs"))
        {
            prefabs.Clear();
            prefabsName.Clear();
            foreach (GameObject g in Resources.LoadAll("Prefabs/Level/LevelProps", typeof(GameObject)))
            {
                Debug.Log("prefab found: " + g.name);
                prefabs.Add(g);
                prefabsName.Add(g.name);
                Debug.Log(g.name);
            }
        }
        if (prefabsName.Count > 0)
        {
            index = EditorGUILayout.Popup(index, prefabsName.ToArray());
        }
        EditorGUILayout.EndToggleGroup();
        this.SaveChanges();
    }
}
