using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

[InitializeOnLoad]
public class AutoSaveEditor
    {
    private static float saveInterval = 300f; // Save interval in seconds, default is 5 minutes
    private static double nextSaveTime = 0;

    static AutoSaveEditor()
        {
        EditorApplication.update += Update;
        }

    private static void Update()
        {
        if (EditorApplication.timeSinceStartup >= nextSaveTime && !EditorApplication.isPlaying)
            {
            SaveAll();
            nextSaveTime = EditorApplication.timeSinceStartup + saveInterval;
            }
        }

    private static void SaveAll()
        {
        Debug.Log("Auto-saving project and scene at " + System.DateTime.Now);

        // Save the currently open scene
        if (!EditorSceneManager.SaveOpenScenes())
            {
            Debug.LogError("AutoSave failed: Could not save the open scenes.");
            }

        // Save the project (assets, etc.)
        AssetDatabase.SaveAssets();
        }
    }

