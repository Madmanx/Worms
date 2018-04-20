using UnityEngine;
using UnityEngine.UI;

public class UITeamSumLife : MonoBehaviour {

    private int maxPv = 300;

    public void Start()
    {
        maxPv = GameManager.Instance.NbWorms * GameManager.Instance.LifeMax;
    }

    private void OnEnable()
    {
        GameLoopManager.OnNbPlayerRefresh += UpdateNbPlayer;
    }

    private void OnDisable()
    {
        GameLoopManager.OnNbPlayerRefresh -= UpdateNbPlayer;
    }

    //public bool coolingDown;
    //void Update()
    //{
    //    if (coolingDown == true)
    //    {
    //        transform.GetChild((int)t).GetChild(0).GetComponent<Image>().fillAmount -= 1.0f / deduceValue * Time.deltaTime;
    //    }
    //}

    public void UpdateNbPlayer(TeamState value)
    {
        int sum = 0;
        switch (value)
        {
            case TeamState.Team1:
                for(int i = 0; i< GameLoopManager.Instance.WormsTeam1.Count; i++)
                {
                    sum += GameLoopManager.Instance.WormsTeam1[i].GetComponent<WormInfo>().Life;
                }
                break;
            case TeamState.Team2:
                for (int i = 0; i < GameLoopManager.Instance.WormsTeam2.Count; i++)
                {
                    sum += GameLoopManager.Instance.WormsTeam2[i].GetComponent<WormInfo>().Life;
                }
                break;
        }
        
        transform.GetChild((int)value+1).GetChild(0).GetComponent<Image>().fillAmount = ((float)sum / (float)maxPv);
    }
}
