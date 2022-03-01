using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemDatabaseEditorWindow : EditorWindow
{
    protected static SerializedObject serializedObject;
    protected static SerializedProperty m_ItemList;
    protected static IEnumerator childEnum;

    // Add menu named "Custom Window" to the Window menu
    [MenuItem("Window/ItemDatabase")]
    public static void Init()
    {
        // Get existing open window or if none, make a new one:
        ItemDatabaseEditorWindow window = (ItemDatabaseEditorWindow)EditorWindow.GetWindow(typeof(ItemDatabaseEditorWindow));
        serializedObject = new SerializedObject(Selection.GetFiltered(typeof(ItemDatabase), SelectionMode.Assets));
        m_ItemList = serializedObject.FindProperty("items");
        childEnum = serializedObject.FindProperty("items").GetEnumerator();
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Cool Item Database Editor", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(m_ItemList, new GUIContent("Item"));

        //while (childEnum.MoveNext())
        //{
        //    var current = childEnum.Current as SerializedProperty;
        //    if (current.name == "info")
        //    {
        //        EditorGUILayout.PropertyField(current, new GUIContent("Item"));
        //        EditorGUILayout.TextArea(current.stringValue, GUILayout.MinHeight(5));
        //    }
        //}
    }

    private void OnDestroy()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
