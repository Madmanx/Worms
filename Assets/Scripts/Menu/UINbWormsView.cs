using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINbWormsView: MonoBehaviour {

    private void OnEnable()
    {
        GameManager.OnNbPlayerRefresh += UpdateNbPlayer;
    }

    private void OnDisable()
    {
        GameManager.OnNbPlayerRefresh -= UpdateNbPlayer;
    }

    public void UpdateNbPlayer(int value)
    {
        GetComponent<TextMeshProUGUI>().text = value.ToString();
    }
}
