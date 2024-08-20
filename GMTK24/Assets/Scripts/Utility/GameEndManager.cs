using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour
{
    [SerializeField]
    Scrollbar scrollbar;

    [SerializeField]
    private int goalTP = 2000;

    private void Awake() {

    }

    public void UpdateBar(int totalTP) {
        scrollbar.size = totalTP / goalTP;
        if (totalTP >= goalTP) {
            Debug.Log("You win!");
        }
    }
}
