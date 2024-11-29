using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

[InitializeOnLoad]
public class HierarchyIconDisplay
{
    static bool show_prefabs = false;
    static bool _hierarchyHasFocus = false;
    static EditorWindow _hierarchyEditorWindow;

    static HierarchyIconDisplay() 
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        EditorApplication.update += OnEditorUpdate;
    }

    private static void OnEditorUpdate() 
    {
        _hierarchyEditorWindow ??= EditorWindow.GetWindow(System.Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor"));
        _hierarchyHasFocus = EditorWindow.focusedWindow != null && EditorWindow.focusedWindow == _hierarchyEditorWindow;
    }

    private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect) 
    { 
        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (obj == null) 
        {
            return;
        }

        if (PrefabUtility.GetCorrespondingObjectFromOriginalSource(obj) != null && show_prefabs)
        {
            return;
        }

        Component[] components = obj.GetComponents<Component>();
        if (components == null || components.Length == 0)
        {
            return;
        }

        Component component = components.Length > 1 ? components[1] : components[0];
        Type type = component.GetType();
        GUIContent content = EditorGUIUtility.ObjectContent(component, type);
        content.text = null;
        content.tooltip = type.Name;

        if (content.image == null)
        {
            return;
        }

        bool sel = Selection.instanceIDs.Contains(instanceID);
        bool hov = selectionRect.Contains(Event.current.mousePosition);

        Color color = GetColor(sel,hov,_hierarchyHasFocus);
        Rect bgrect = selectionRect;
        bgrect.width = 18.5f;
        EditorGUI.DrawRect(bgrect, color);

        EditorGUI.LabelField(selectionRect, content);
    }

    static Color GetColor(bool isSelected, bool isHovered, bool isWindowFocused) 
    {
        Color defColor = new Color(0.7843f, 0.7843f, 0.7843f);
        Color defPColor = new Color(0.2196f, 0.2196f, 0.2196f);

        Color selColor = new Color(0.22745f, 0.447f, 0.6902f);
        Color selPColor = new Color(0.1725f, 0.3647f, 0.5294f);

        Color selUColor = new Color(0.68f, 0.68f, 0.68f);
        Color selUPColor = new Color(0.3f, 0.3f, 0.3f);

        Color hovColor = new Color(0.698f, 0.698f, 0.698f);
        Color hovPColor = new Color(0.2706f, 0.2706f, 0.2706f);

        if (isSelected)
        {
            if (isWindowFocused)
            {
                return EditorGUIUtility.isProSkin ? selPColor : selColor;
            }
            else
            {
                return EditorGUIUtility.isProSkin ? selUPColor : selUColor;
            }
        }
        else if (isHovered)
        {
            return EditorGUIUtility.isProSkin ? hovPColor : hovColor;
        }
        else 
        {
            return EditorGUIUtility.isProSkin ? defPColor : defColor;
        }
    }
}
