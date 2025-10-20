using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class FindScriptWindow : EditorWindow
{
    private string _scriptName;
    private Vector2 _scroll;
    private List<GameObject> _foundObjects = new();

    [MenuItem("Tools/Find Script in Scene")]
    private static void OpenWindow()
    {
        GetWindow<FindScriptWindow>("Find Script");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Find Objects by Script", EditorStyles.boldLabel);
        _scriptName = EditorGUILayout.TextField("Script Name", _scriptName);

        if (GUILayout.Button("Search"))
        {
            SearchObjects();
        }

        EditorGUILayout.Space(10);
        _scroll = EditorGUILayout.BeginScrollView(_scroll);

        if (_foundObjects.Count > 0)
        {
            foreach (var obj in _foundObjects)
            {
                if (obj == null)
                    continue;

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(obj, typeof(GameObject), true);
                if (GUILayout.Button("Select", GUILayout.Width(60)))
                    Selection.activeObject = obj;
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            EditorGUILayout.LabelField("No objects found.");
        }

        EditorGUILayout.EndScrollView();
    }

    private void SearchObjects()
    {
        _foundObjects.Clear();
        if (string.IsNullOrEmpty(_scriptName))
            return;

        var all = Object.FindObjectsOfType<GameObject>(true);
        foreach (var go in all)
        {
            var comps = go.GetComponents<MonoBehaviour>();
            foreach (var comp in comps)
            {
                if (comp == null)
                    continue;
                if (comp.GetType().Name.ToLower() == _scriptName.ToLower())
                {
                    _foundObjects.Add(go);
                    break;
                }
            }
        }
        Debug.Log($"Found {_foundObjects.Count} object(s) with script {_scriptName}.");
    }
}
