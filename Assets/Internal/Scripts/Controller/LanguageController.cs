using System.Collections.Generic;
using UnityEngine;

public class LanguageController : MonoBehaviour
{
    public static LanguageController instance;
    [SerializeField] private Language currentLanguage;
    private Dictionary<string, string> languegeKeys = new();

    public List<LanguageItemInit> languageSetups = new();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        languegeKeys?.Clear();
        for (int i = 0; i < languageSetups.Count; i++)
        {
            LanguageItemInit item = languageSetups[i];
            for (int j = 0; j < item.items.Count; j++)
            {
                LanguageItem tempItem = item.items[j];
                languegeKeys[item.option.ToString() + tempItem.language] = tempItem.value;
            }
        }
    }

    public string GetValue(LanguageOptions options)
    {
        string key = options.ToString() + currentLanguage.ToString();
        return languegeKeys.TryGetValue(key, out var value) ? value : string.Empty;
    }
}
[System.Serializable]
public class LanguageItem
{
    public Language language;
    public string value = "";
}
[System.Serializable]
public class LanguageItemInit
{
    public LanguageOptions option;
    public List<LanguageItem> items;
}
public enum LanguageOptions
{
    Inventory_Title,
}