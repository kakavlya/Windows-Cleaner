using UnityEditor;
using UnityEngine;

public class ListAllScenes : EditorWindow
{
    [MenuItem("Tools/List All Scenes")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ListAllScenes), false, "Scene List");
    }

    private Vector2 scrollPos;

    private void OnGUI()
    {
        GUILayout.Label("All Scenes in the Project", EditorStyles.boldLabel);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        string[] scenePaths = AssetDatabase.FindAssets("t:Scene"); // Find all scenes
        foreach (string sceneGUID in scenePaths)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(sceneGUID);
            GUILayout.Label(scenePath);
        }

        EditorGUILayout.EndScrollView();
    }
}