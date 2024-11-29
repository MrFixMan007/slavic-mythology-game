using UnityEditor;
using UnityEngine;

public class SetLabel
{
    const string my_label = "ScriptIcon";

    [MenuItem("Tools/Script Icons/Assign Label")]
    static void AssignScriptIconMenuItem() 
    {
        Object[] objects = Selection.objects;
        if (objects == null)
        {
            return;
        }

        foreach (Object obj in objects)
        {
            string[] curr_labels = AssetDatabase.GetLabels(obj);
            if (!ArrayUtility.Contains<string>(curr_labels, my_label))
            {
                ArrayUtility.Add<string>(ref curr_labels, my_label);
                AssetDatabase.SetLabels(obj, curr_labels);
            }
        }
    }

    [MenuItem("Tools/Script Icons/Remove Label")]
    static void RemoveScriptIconMenuItem()
    {
        Object[] objects = Selection.objects;
        if (objects == null)
        {
            return;
        }

        foreach (Object obj in objects)
        {
            string[] curr_labels = AssetDatabase.GetLabels(obj);
            if (ArrayUtility.Contains<string>(curr_labels, my_label))
            {
                ArrayUtility.Remove<string>(ref curr_labels, my_label);
                AssetDatabase.SetLabels(obj, curr_labels);
            }
        }
    }
}
