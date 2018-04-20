using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMapSelected : MonoBehaviour {

    private void OnEnable()
    {
        GameManager.OnNbPlayerRefresh += UpdateMapIndex;
        UpdateMapIndex(GameManager.Instance.IndexMapSelected);

    }

    private void OnDisable()
    {
        GameManager.OnNbPlayerRefresh -= UpdateMapIndex;
    }

    public void UpdateMapIndex(int value)
    {
        GetComponent<TextMeshProUGUI>().text = value.ToString();
    }
}
