using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageChange : MonoBehaviour {

    Dropdown dropdown;
    private Localization localization;

    // Use this for initialization
    void Awake () {
        dropdown = GetComponent<Dropdown>();
        localization = Localization.getInstance();
        dropdown.onValueChanged.AddListener (delegate
        {
            DropdownValueChanged(dropdown);
        });
        switch (PlayerPrefs.GetString(Localization.LENG_PREFERENCE,"EN"))
        {
            case "EN":
                dropdown.value = 0;
                break;
            case "ES":
                dropdown.value = 1;
                break;
            case "IT":
                dropdown.value = 2;
                break;
            default:
                break;
        }
	}


    private void DropdownValueChanged(Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                PlayerPrefs.SetString(Localization.LENG_PREFERENCE, "EN");
                break;
            case 1:
                PlayerPrefs.SetString(Localization.LENG_PREFERENCE, "ES");
                break;
            case 2:
                PlayerPrefs.SetString(Localization.LENG_PREFERENCE, "IT");
                break;
            default:
                break;

        }
        localization.checkChangeOnLanguage();
    }

}
