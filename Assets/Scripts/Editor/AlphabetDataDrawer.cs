using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(AlphabetData))]
public class AlphabetDataDrawer : Editor
{
    private ReorderableList plainList;
    private ReorderableList normalList;
    private ReorderableList highlightedList;
    private ReorderableList wrongList;

    private void OnEnable()
    {
        CreateList(plainList,       "AlphabetPlain",       "Alphabet Plain");
        CreateList(normalList,      "AlphabetNormal",      "Alphabet Normal");
        CreateList(highlightedList, "AlphabetHighlighted", "Alphabet Highlighted");
        CreateList(wrongList,       "AlphabetWrong",       "Alphabet Wrong");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        plainList.serializedProperty       = serializedObject.FindProperty("AlphabetPlain");
        normalList.serializedProperty      = serializedObject.FindProperty("AlphabetNormal");
        highlightedList.serializedProperty = serializedObject.FindProperty("AlphabetHighlighted");
        wrongList.serializedProperty       = serializedObject.FindProperty("AlphabetWrong");

        plainList.DoLayoutList();
        normalList.DoLayoutList();
        highlightedList.DoLayoutList();
        wrongList.DoLayoutList();

        EditorGUILayout.Space(15);
        if (GUILayout.Button("Populate All Lists (A-Z)", GUILayout.Height(40)))
            PopulateAllLists();

        serializedObject.ApplyModifiedProperties();
    }

    private void CreateList(ReorderableList list, string propertyName, string label)
    {
        var l = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName),
                                   true, true, true, true);

        l.drawHeaderCallback = rect => EditorGUI.LabelField(rect, label);

        l.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            float h = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(new Rect(rect.x, rect.y, 50, h),
                element.FindPropertyRelative("letter"), GUIContent.none);

            EditorGUI.PropertyField(new Rect(rect.x + 60, rect.y, rect.width - 60, h),
                element.FindPropertyRelative("image"), GUIContent.none);
        };

        l.elementHeight = EditorGUIUtility.singleLineHeight + 6;
        list = l; // atribui de volta
    }

    private void PopulateAllLists()
    {
        var data = (AlphabetData)target;
        void Fill(System.Collections.Generic.List<AlphabetData.LetterData> lst)
        {
            lst.Clear();
            for (char c = 'A'; c <= 'Z'; c++)
                lst.Add(new AlphabetData.LetterData { letter = c.ToString() });
        }

        Fill(data.AlphabetPlain);
        Fill(data.AlphabetNormal);
        Fill(data.AlphabetHighlighted);
        Fill(data.AlphabetWrong);

        EditorUtility.SetDirty(data);
    }
}