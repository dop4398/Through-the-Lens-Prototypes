using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseEditor : Editor
{
    SerializedProperty m_ItemList;

    void OnEnable()
    {
        m_ItemList = serializedObject.FindProperty("items");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(m_ItemList, new GUIContent("Item"));

        serializedObject.ApplyModifiedProperties();
    }
}
