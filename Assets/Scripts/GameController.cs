using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI killsText;
    private int kills;

    void Start()
    {
        kills = 0;
        UpdateKillCount();
    }

    public void IncrementKillCount()
    {
        // al haber 2 firepoints el resultado de kills se duplica cuando los 2 bullets colisionan a 1 tanque.
        kills++;
        UpdateKillCount();
    }

    void UpdateKillCount()
    {
        killsText.text = "Kills: " + kills.ToString();
    }
}
