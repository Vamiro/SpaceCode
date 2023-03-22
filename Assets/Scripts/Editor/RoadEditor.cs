using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GlowObjects))]

public class RoadEditor : Editor
{
    static Vector3[] _cubesPosition = new Vector3[6] {new Vector3(1f, 0f, 0f),
                                                      new Vector3(0f, 1f, 0f),
                                                      new Vector3(0f, 0f, 1f),
                                                      new Vector3(0f, 0f, -1f),
                                                      new Vector3(0f, -1f, 0f),
                                                      new Vector3(-1f, 0f, 0f)};

    static Color[] _colors = new Color[6] {new Color(1f, 0f, 0f, 1f),
                                           new Color(0.15f, 1f, 0f, 1f),
                                           new Color(0f, 0.15f, 1f, 1f),
                                           new Color(0f, 1f, 1f, 1f),
                                           new Color(1f, 1f, 0f, 1f),
                                           new Color(1f, 0f, 1f, 1f),};

    protected virtual void OnSceneGUI()
    {
        if (LevelEditor.isRaodEditorOn)
        {
            GameObject toCreateObject = LevelEditor.GetObjectToCreate();
            Transform transform = ((GlowObjects)target).transform;

            for (int i = 0; i < _cubesPosition.Length; i++)
            {
                Vector3 pos = transform.position + _cubesPosition[i];
                Handles.color = _colors[i];

                if (Physics.CheckSphere(pos, 0.3f)) continue;

                if (Handles.Button(pos, Quaternion.Euler(Vector3.forward), 0.25f, 0.1f, Handles.CubeHandleCap) && toCreateObject != null)
                {
                    var newCube = GameObject.Instantiate(toCreateObject, pos, transform.rotation, transform.parent);
                    Undo.RegisterCreatedObjectUndo(newCube, toCreateObject.name);
                    newCube.name = toCreateObject.name;
                    Selection.activeObject = newCube;
                }
            }
        }
    }
}
