using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

public class SetIconWindow : EditorWindow
{
    const string menuPath = "Assets/Create/Set Icon...";

    List<Texture2D> icons = null;
    int selected = 0;

    [MenuItem(menuPath, priority = 0)]
    public static void ShowMenuItem() 
    {
        SetIconWindow window = (SetIconWindow)EditorWindow.GetWindow(typeof(SetIconWindow));
        window.titleContent = new GUIContent("Set Icon");
        window.Show();
    }

    [MenuItem(menuPath, validate = true)]
    public static bool ShowMenuItemValidation() 
    {
        foreach (Object asset in Selection.objects)
        {
            if (asset.GetType() != typeof(MonoScript))
            {
                return false;
            }
        }
        return true;
    }

    void OnGUI()
    {
        if (icons == null)
        {
            icons = new List<Texture2D>();
            string[] assetGuids = AssetDatabase.FindAssets("t:texture2d l:ScriptIcon");

            foreach (string assetguid in assetGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(assetguid);
                icons.Add(AssetDatabase.LoadAssetAtPath<Texture2D>(path));
            }
        }

        if (icons == null)
        {
            GUILayout.Label("No icons to display");

            if (GUILayout.Button("Close", GUILayout.Width(100)))
            {
                Close();
            }
        }
        else
        {
            selected = GUILayout.SelectionGrid(selected, icons.ToArray(), 5);
            if (Event.current != null)
            {
                if (Event.current.isKey)
                {
                    switch (Event.current.keyCode)
                    {
                        case KeyCode.KeypadEnter:
                        case KeyCode.Return:
                            ApplyIcon(icons[selected]);
                            Close();
                            break;
                        case KeyCode.Escape:
                            Close();
                            break;
                        default:
                            break;
                    }
                }
                else if (Event.current.button == 0 && Event.current.clickCount == 2)
                {
                    ApplyIcon(icons[selected]);
                    Close();
                }
            }

            if (GUILayout.Button("Apply", GUILayout.Width(100)))
            {
                ApplyIcon(icons[selected]);
                Close();
            }
        }
    }

    void ApplyIcon(Texture2D icon)
    {
        AssetDatabase.StartAssetEditing();

        foreach (Object asset in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(asset);
            MonoImporter monoImporter = AssetImporter.GetAtPath(path) as MonoImporter;
            monoImporter.SetIcon(icon);
            AssetDatabase.ImportAsset(path);
        }

        AssetDatabase.StopAssetEditing();

        AssetDatabase.Refresh();
    }
}
