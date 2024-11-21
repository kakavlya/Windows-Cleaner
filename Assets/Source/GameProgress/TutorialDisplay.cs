using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialElement;
    [SerializeField] private string _tutorialPrefsName = "TutorialShown";


    private void Start()
    {
        ShowTutorialIfNeeded();
    }

    public void ShowTutorialIfNeeded() {
        if (!PlayerPrefs.HasKey(_tutorialPrefsName))
        {
            _tutorialElement.SetActive(true);

            PlayerPrefs.SetInt(_tutorialPrefsName, 1);
            PlayerPrefs.Save();
        }
        else
        {
            _tutorialElement.SetActive(false);
        }
    }
}
