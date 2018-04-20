using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TeamState { Team1, Team2 }

public class GameLoopManager : MonoBehaviour
{
    private static GameLoopManager singleton;

    public delegate void RefreshNbPlayer(TeamState value);
    public static event RefreshNbPlayer OnNbPlayerRefresh;

    public TextMeshProUGUI textTurnTimer;
    public TextMeshProUGUI textTeam1;
    public TextMeshProUGUI textTeam2;
    public GameObject TitleScreen;
    public GameObject ResumeBtn;
    public GameObject WinnerText;

    [SerializeField]
    private int turnDuration;
    private float timeTurn;
    private bool turnOver = false;

    private bool activate = false;

    private List<WormInfo> wormsTeam1 = new List<WormInfo>();
    private List<WormInfo> wormsTeam2 = new List<WormInfo>();

    private TeamState currentTeamTurn;
    private int currentPlayerWormIndexTeam1 = 0;
    private int currentPlayerWormIndexTeam2 = 0;

    private bool askForChangeTurn = false;

    public Spawner spawner;

    public List<WormInfo> WormsTeam1
    {
        get
        {
            return wormsTeam1;
        }

        set
        {
            wormsTeam1 = value;
        }
    }

    public List<WormInfo> WormsTeam2
    {
        get
        {
            return wormsTeam2;
        }

        set
        {
            wormsTeam2 = value;
        }
    }

    public static GameLoopManager Instance
    {
        get
        {
            return singleton;
        }

        set
        {
            singleton = value;
        }
    }

    public float TimeTurn
    {
        get
        {
            return timeTurn;
        }

        set
        {
            timeTurn = value;
            UpdateTurnTimer();
        }
    }

    public bool Activate
    {
        get
        {
            return activate;
        }

        set
        {
            activate = value;
        }
    }


    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        currentTeamTurn = (TeamState)Random.Range(0, 2);

        textTeam1.text = GameManager.Instance.TeamName[0];
        textTeam2.text = GameManager.Instance.TeamName[1];

        TimeTurn = turnDuration;
    }

    private void UpdateTurnTimer()
    {
        textTurnTimer.text = ((int)timeTurn).ToString();
    }

    private void Update()
    {
        if (!activate)
            return;

        if (askForChangeTurn)
        {
            askForChangeTurn = false;
            TimeTurn = 4.0f;
        } 
        if (turnOver == false)
        {
            if (timeTurn > 0.0f)
                TimeTurn -= Time.deltaTime;
            else
                turnOver = true;
        }
        else
        {
            ProcessChangeTurn();
        }

    }

    public void AskForChangeTurn()
    {
        // can't be called twice in a row
        if (TimeTurn < 4.0f || askForChangeTurn)
            return;
        // Un peu degeulasse

        askForChangeTurn = true;
    }

    private void ProcessChangeTurn()
    {
        if (wormsTeam1.Count == 0 || wormsTeam2.Count == 0)
        {
            OnCatchEnd();
            return;
        }

        if (askForChangeTurn)
            return;

        // Avant je faisais juste toggleCurrentWormInCurrentTeam mais j'ai un ou deux cas improbable a cause du askForChangeTurn
        for (int i = 0; i < wormsTeam1.FindAll(a => a.Possessed == true).Count; i++)
        {
            wormsTeam1.FindAll(a => a.Possessed == true)[i].Possessed = false;
        }
        for (int i = 0; i < wormsTeam2.FindAll(a => a.Possessed == true).Count; i++)
        {
            wormsTeam2.FindAll(a => a.Possessed == true)[i].Possessed = false;
        }

        ChangeTurn();
        ToggleCurrentWormInCurrentTeam(true);

        if( Random.Range(0, 5) == 0)
        {
            spawner.SpawnCrate();
        }

        TimeTurn = turnDuration;
        turnOver = false;
    }

    public void ChangeTurn()
    {
        if (currentTeamTurn == TeamState.Team1)
        {
            currentTeamTurn = TeamState.Team2;
            currentPlayerWormIndexTeam2++;
        } else
        {
            currentTeamTurn = TeamState.Team1;
            currentPlayerWormIndexTeam1++;
        }
    }

    public void ToggleCurrentWormInCurrentTeam(bool active)
    {
        switch (currentTeamTurn)
        {
            case TeamState.Team1:
                if (currentPlayerWormIndexTeam1 >= wormsTeam1.Count) currentPlayerWormIndexTeam1 = 0;
                wormsTeam1[currentPlayerWormIndexTeam1].Possessed = active;

                break;
            case TeamState.Team2:
                if (currentPlayerWormIndexTeam2 >= wormsTeam2.Count) currentPlayerWormIndexTeam2 = 0;
                wormsTeam2[currentPlayerWormIndexTeam2].Possessed = active;
                break;
        }
    }

    public void OnWormDeath(WormInfo worm)
    {
        if (wormsTeam1.Find(a => a == worm))
        {
            wormsTeam1.Remove(worm);
            OnNbPlayerRefresh(TeamState.Team1);
        }
        else if (wormsTeam2.Find(a => a == worm))
        {
            wormsTeam2.Remove(worm);
            OnNbPlayerRefresh(TeamState.Team2);
        }
    }

    public void OnWormTakeDamage(TeamState team)
    {
        OnNbPlayerRefresh(team);
    }

    public void OnCatchEnd()
    {
        int winner;
        if (wormsTeam1.Count > 0) {
            CameraManager.Instance.MainCameraFollow(wormsTeam1[0].transform);
            winner = (int)TeamState.Team1;
            WinnerText.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.Instance.TeamName[0] + " Winner !";
            WinnerText.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        }

        else if (wormsTeam2.Count > 0) {
            CameraManager.Instance.MainCameraFollow(wormsTeam2[0].transform);
            winner = (int)TeamState.Team2;
            WinnerText.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.Instance.TeamName[1] + " Winner !";
            WinnerText.GetComponentInChildren<TextMeshProUGUI>().color = Color.blue;
        }
        else {
            winner = 2;
            WinnerText.GetComponentInChildren<TextMeshProUGUI>().text = "All dead";
        }
        PlayerPrefs.SetInt("LastWinner", (int)winner);

        // Na rien afaire la mais bon 
        ResumeBtn.gameObject.SetActive(false);
        WinnerText.gameObject.SetActive(true);

        Activate = false;
        ToggleTitleScreen();
    }

    public void ToggleTitleScreen()
    {
        TitleScreen.SetActive(!TitleScreen.activeSelf);
        Time.timeScale = (TitleScreen.activeSelf)? 0 :  1;
    }

    // Just for debug.
    public WormInfo GetActiveWorm()
    {
        if (wormsTeam1.Find(a => a.Possessed == true))
        {
            return (wormsTeam1.Find(a => a.Possessed == true));
        }
        else if (wormsTeam2.Find(a => a.Possessed == true))
        {
            return (wormsTeam2.Find(a => a.Possessed == true));
        }
        return null;
    }

}
