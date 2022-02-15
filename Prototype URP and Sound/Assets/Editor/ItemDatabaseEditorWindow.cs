using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemDatabaseEditorWindow : EditorWindow
{
    protected static SerializedObject serializedObject;
    protected static SerializedProperty m_ItemList;

    // Add menu named "Custom Window" to the Window menu
    [MenuItem("Window/ItemDatabase")]
    public static void Init()
    {
        // Get existing open window or if none, make a new one:
        ItemDatabaseEditorWindow window = (ItemDatabaseEditorWindow)EditorWindow.GetWindow(typeof(ItemDatabaseEditorWindow));
        serializedObject = new SerializedObject(Selection.GetFiltered(typeof(ItemDatabase), SelectionMode.Assets));
        m_ItemList = serializedObject.FindProperty("items");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Cool Item Database Editor", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(m_ItemList, new GUIContent("Item"));
    }

    private void OnDestroy()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
