using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
/**
 * Class designed to load different languages easily to the game
 * It is a singleton
 * 
 * The default lenguage is set to english and its extension as ES (change this if english becomes a dead language)
 */
public class Localization : MonoBehaviour
{
    //The PlayerPref field that stores the language extension
    public const string LENG_PREFERENCE = "Language";
    private const string BASE_FILENAME = "Localization";
    private const string DEFAULT_EXTENSION = "EN";
    private string auxLangExtension;
    private bool web;
    /**Event called when the language has changed*/
    public delegate void LanguageChanged();
    public static event LanguageChanged onLanguagechanged;
    /**the actual language extension (filename made BASE_FILENAME+EXTENSION)*/
    public string langExtension
    {
        get;
        private set;
    }
    private static Localization instance;
    private Dictionary<string, string> localizatedText;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            if (PlayerPrefs.HasKey(LENG_PREFERENCE))
                langExtension = PlayerPrefs.GetString(LENG_PREFERENCE);
            else
            {
                PlayerPrefs.SetString(LENG_PREFERENCE, DEFAULT_EXTENSION);
                langExtension = DEFAULT_EXTENSION;
            }
            loadLocalizedText(BASE_FILENAME + langExtension);
        }
        else
            Destroy(this.gameObject);
    }
    /**Returns the instance of the singleton
	 */
    public static Localization getInstance()
    {
        return instance;
    }

    /**
	 * returns the lozalizated text correspondig to a key
	 */
    public string get(string key)
    {
        if (localizatedText.ContainsKey(key))
            return localizatedText[key];
        else
            return "";
    }


    private bool loadLocalizedText(string fileName)
    {
        localizatedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            web = true;
            StartCoroutine(getFile(filePath));
            return true;
        }
        if (File.Exists(filePath))
        {

            string dataAsJson = File.ReadAllText(filePath);
            LocalizationArray array = JsonUtility.FromJson<LocalizationArray>(dataAsJson);
            for (int i = 0; i < array.data.Length; i++)
            {
                localizatedText.Add(array.data[i].key, array.data[i].value);
            }
            return true;

        }
        else
        {



            return false;
        }


    }

    IEnumerator getFile(String path)
    {
        WWW www = new WWW(path);
        yield return www;
        string dataAsJson = www.text;
        LocalizationArray array = JsonUtility.FromJson<LocalizationArray>(dataAsJson);
        for (int i = 0; i < array.data.Length; i++)
        {
            localizatedText.Add(array.data[i].key, array.data[i].value);
        }
        if (onLanguagechanged != null)
            onLanguagechanged();
    }

    [Serializable]
    public class Data
    {
        public string key;
        public string value;
    }

    [Serializable]
    public class LocalizationArray
    {
        public Data[] data;
    }

    /**
	 * Checks if there have been changes to the PlayerPrefs that change the language
	* if that is the case tries  to change the language
	* if it is not consistent returns the PlayerPrefs to its value
	* 
	* If you want to change the language remember to call 
	*	PlayerPrefs.SetString (LENG_PREFERENCE, [newExtension]); replacing [newExtension]
	*  with the extension of the new language
	*/
    public void checkChangeOnLanguage()
    {

        auxLangExtension = PlayerPrefs.GetString(LENG_PREFERENCE);
        if (auxLangExtension != langExtension)
        {
            if (loadLocalizedText(BASE_FILENAME + auxLangExtension))
            {
                langExtension = auxLangExtension;
                if (onLanguagechanged != null)
                    onLanguagechanged();

            }
            else
            {
                if (!web)

                    PlayerPrefs.SetString(LENG_PREFERENCE, langExtension);
            }

        }
    }
}
