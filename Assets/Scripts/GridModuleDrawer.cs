using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(GridLayout))]
public class GridModuleDrawer: PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.PrefixLabel(position, label);
        Rect newPosition = position;
        newPosition.x = position.x + 10;
        newPosition.y += 18f;
        SerializedProperty data = property.FindPropertyRelative("rows");
        for (int j = 0; j < 5; j++)
        {
            SerializedProperty row = data.GetArrayElementAtIndex(j).FindPropertyRelative("row");
            if (row.arraySize != 5) {
                row.arraySize = 5;
            }
            newPosition.height = 18f;
            newPosition.width = 20;
            for (int i = 0; i < 5; i++)
            {
                EditorGUI.PropertyField(newPosition, row.GetArrayElementAtIndex(i), GUIContent.none);
                newPosition.x += newPosition.width;
            }
            newPosition.y += 18f;
            newPosition.x = position.x + 10;
        }

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 18f * 6f;
    }
}