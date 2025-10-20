using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace WindowsCleaner.Editor
{
    public class ListSourceScripts : EditorWindow
    {
        private const string GameProgressPref = "GameProgress";

        private Vector2 _scrollPos;
        private List<ScriptInfo> _scripts = new List<ScriptInfo>();
        private string _result = string.Empty;

        [MenuItem("Tools/List Source Scripts")]
        public static void ShowWindow()
        {
            GetWindow<ListSourceScripts>("List Source Scripts");
        }

        private void OnGUI()
        {
            GUILayout.Label("List All Scripts in Assets/Source/Scripts", EditorStyles.boldLabel);

            if (GUILayout.Button("Scan Scripts", GUILayout.Height(30)))
            {
                ScanScripts();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Copy to Clipboard", GUILayout.Height(25)))
            {
                GUIUtility.systemCopyBuffer = _result;
                Debug.Log("Script list copied to clipboard!");
            }

            GUILayout.Space(10);

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            EditorGUILayout.TextArea(_result, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();
        }

        private void ScanScripts()
        {
            _scripts.Clear();
            _result = string.Empty;

            string sourcePath = Path.Combine(Application.dataPath, "Source", "Scripts");

            if (!Directory.Exists(sourcePath))
            {
                _result = "Path not found: " + sourcePath;
                return;
            }

            string[] files = Directory.GetFiles(sourcePath, "*.cs", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                string relativePath = "Assets" + file.Substring(Application.dataPath.Length).Replace("\\", "/");
                string content = File.ReadAllText(file);
                string currentNamespace = ExtractNamespace(content);
                string folderName = GetTopLevelFolder(file, sourcePath);
                string expectedNamespace = GetExpectedNamespace(folderName);

                ScriptInfo info = new ScriptInfo
                {
                    Path = relativePath,
                    CurrentNamespace = currentNamespace,
                    ExpectedNamespace = expectedNamespace,
                    FolderName = folderName,
                    NeedsUpdate = currentNamespace != expectedNamespace,
                };

                _scripts.Add(info);
            }

            _result = $"Found {_scripts.Count} scripts:\n\n";

            foreach (var script in _scripts)
            {
                _result += $"File: {script.Path}\n";
                _result += $"Folder: {script.FolderName}\n";
                _result += $"Current Namespace: {script.CurrentNamespace}\n";
                _result += $"Expected Namespace: {script.ExpectedNamespace}\n";
                _result += $"Needs Update: {(script.NeedsUpdate ? "YES" : "NO")}\n";
                _result += new string('-', 80) + "\n";
            }

            _result += $"\n\nSummary:\n";
            _result += $"Total Scripts: {_scripts.Count}\n";
            _result += $"Need Updates: {_scripts.FindAll(s => s.NeedsUpdate).Count}\n";
            _result += $"Already Correct: {_scripts.FindAll(s => !s.NeedsUpdate).Count}\n";
        }

        private string ExtractNamespace(string content)
        {
            Match match = Regex.Match(content, @"namespace\s+([\w\.]+)");
            return match.Success ? match.Groups[1].Value : "(no namespace)";
        }

        private string GetTopLevelFolder(string filePath, string sourcePath)
        {
            string relativePath = filePath.Substring(sourcePath.Length).TrimStart('\\', '/');
            int firstSlash = relativePath.IndexOfAny(new char[] { '\\', '/' });

            if (firstSlash > 0)
            {
                return relativePath.Substring(0, firstSlash);
            }

            return "(root)";
        }

        private string GetExpectedNamespace(string folderName)
        {
            if (folderName == "(root)")
            {
                return "WindowsCleaner";
            }

            if (folderName == "Player")
            {
                return "WindowsCleaner.PlayerNs";
            }

            return $"WindowsCleaner.{folderName}";
        }

        private class ScriptInfo
        {
            public string Path;
            public string CurrentNamespace;
            public string ExpectedNamespace;
            public string FolderName;
            public bool NeedsUpdate;
        }
    }
}