using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour
{
    [SerializeField]
    Scrollbar scrollbar;

    [SerializeField]
    private int goalTP = 2000;

    [SerializeField]
    private GameObject finalCutscene;
    [SerializeField]
    private GameObject endCutsceneController;

    TextMeshProUGUI text;

    private void Awake() {
        text = scrollbar.GetComponentInChildren<TextMeshProUGUI>();
        text.text = "Goal: Get to " + goalTP;
        finalCutscene.SetActive(false);
        endCutsceneController.SetActive(false);
    }

    public void UpdateBar(int totalTP) {
        scrollbar.size = totalTP / (float)goalTP;
        if (totalTP >= goalTP) {
            finalCutscene.SetActive(true);
            endCutsceneController.SetActive(true);
        }
    }
}
