using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIConfigController : MonoBehaviour {

    [Header("Reference Usage")]
    public GameObject TitleScreen;
    public GameObject ConfigNbWorms;
    public GameObject ConfigTeam;
    public GameObject ConfigMap;
    public GameObject PreviousBtn;
    public GameObject NextBtn;
    public GameObject StartBtn;

    public GameObject PlusAddWormBtn;
    public GameObject MinusRemoveWormBtn;

    public GameObject PlusMapBtn;
    public GameObject MinusMapBtn;

    public GameObject TextPlayer1;
    public GameObject TextPlayer2;

    private int state = 0;

    public void OnAddWorm()
    {
        GameManager.Instance.NbWorms += 1;
        OnChangeNbWorm();
    }

    public void OnRemoveWorm()
    {
        GameManager.Instance.NbWorms -= 1;
        OnChangeNbWorm();
    }

    public void OnNextMap()
    {
        GameManager.Instance.IndexMapSelected += 1;
        OnChangeMapIndex();
    }

    public void OnPreviousMap()
    {
        GameManager.Instance.IndexMapSelected -= 1;
        OnChangeMapIndex();
    }

    public void OnChangeMapIndex()
    {
        PlusMapBtn.GetComponent<Button>().interactable = (GameManager.Instance.IndexMapSelected < GameManager.Instance.MaxMap);
        MinusMapBtn.GetComponent<Button>().interactable = (GameManager.Instance.IndexMapSelected > 1);
    }

    public void OnChangeNbWorm()
    {
        PlusAddWormBtn.GetComponent<Button>().interactable = (GameManager.Instance.NbWorms < GameManager.Instance.MaxNbWorms);
        MinusRemoveWormBtn.GetComponent<Button>().interactable = (GameManager.Instance.NbWorms > 1);
    }

    public void OnTeamNamePlayer1Change()
    {
        string txt = TextPlayer1.GetComponent<TextMeshProUGUI>().text;
        GameManager.Instance.TeamName[0] = (txt == "") ? "Team Red" : txt;
    }

    public void OnTeamNamePlayer2Change()
    {
        string txt = TextPlayer2.GetComponent<TextMeshProUGUI>().text;
        GameManager.Instance.TeamName[1] = (txt == "")? "Team Blue" : txt;
    }

    public void OnNext()
    {
        state++;
        if (state == 1)
        {
            ConfigTeam.SetActive(true);
            ConfigNbWorms.SetActive(false);
        }
        else if (state == 2)
        {
            ConfigMap.SetActive(true);
            ConfigTeam.SetActive(false);
            StartBtn.SetActive(true);
            NextBtn.SetActive(false);
            OnChangeMapIndex();
        }
    }

    public void OnPrevious()
    {
        if (state == 0)
        {
            gameObject.SetActive(false);
            TitleScreen.SetActive(true);
        }
        else if (state == 1)
        {
            ConfigNbWorms.SetActive(true);
            ConfigTeam.SetActive(false);
            state--;
        }
        else if (state == 2)
        {
            ConfigTeam.SetActive(true);
            ConfigMap.SetActive(false);
            StartBtn.SetActive(false);
            NextBtn.SetActive(true);
            state--;
        }

    }

    public void OnStart()
    {
        SceneManager.LoadScene(GameManager.Instance.IndexMapSelected);
    }
}
