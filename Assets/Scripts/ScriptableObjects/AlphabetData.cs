using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Alphabet Data")]
public class AlphabetData : ScriptableObject
{
    [System.Serializable]
    public class LetterData
    {
        public string letter;
        public Sprite image;
    }

    public List<LetterData> AlphabetPlain = new List<LetterData>();
    public List<LetterData> AlphabetNormal = new List<LetterData>();
    public List<LetterData> AlphabetHighlighted = new List<LetterData>();
    public List<LetterData> AlphabetWrong = new List<LetterData>();


#if UNITY_EDITOR
    // Botão direito no asset → Populate All Alphabets
    [ContextMenu("Populate All Alphabets (A-Z)")]
    private void PopulateAll()
    {
        Populate(AlphabetPlain);
        Populate(AlphabetNormal);
        Populate(AlphabetHighlighted);
        Populate(AlphabetWrong);

        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
    }

    private void Populate(List<LetterData> list)
    {
        list.Clear();
        for (char c = 'A'; c <= 'Z'; c++)
        {
            list.Add(new LetterData
            {
                letter = c.ToString(),
                image = null
            });
        }
    }
#endif
}