using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public delegate void RefreshNbPlayer(int value);
    public static event RefreshNbPlayer OnNbPlayerRefresh;

    #region NbWorms
    private int maxNbWorms = 3;
    private int minNbWorms = 1;
    public int MaxNbWorms
    {
        get
        {
            return maxNbWorms;
        }
    }
    public int MinNbWorms
    {
        get
        {
            return minNbWorms;
        }
    }

    private int nbWorms = 3;
    public int NbWorms
    {
        get
        {
            return nbWorms;
        }
        set
        {
            if (value >= minNbWorms && value <= maxNbWorms)
            {
                nbWorms = value;
                OnNbPlayerRefresh(nbWorms);
            }
        }
    }
    #endregion

    #region Map
    private int indexMapSelected;
    private int maxMap = 3;

    public int IndexMapSelected
    {
        get
        {
            return indexMapSelected;
        }

        set
        {

            if (value >= 1 && value <= maxMap)
            {
                indexMapSelected = value;
                OnNbPlayerRefresh(indexMapSelected);
            }
        }
    }

    public int MaxMap
    {
        get
        {
            return maxMap;
        }

        set
        {
            maxMap = value;
        }
    }
    #endregion

    private string[] teamName = new string[2] { "Team Red", "Team Blue" };
    public string[] TeamName
    {
        get
        {
            return teamName;
        }

        set
        {
            teamName = value;
        }
    }

    private int lifeMax = 100;
    public int LifeMax
    {
        get
        {
            return lifeMax;
        }

        set
        {
            lifeMax = value;
        }
    }

    private static GameManager singleton;
    public static GameManager Instance
    {
        get
        {
            return singleton;
        }
    }


    public void Awake()
    {
        if (!singleton)
        {
            singleton = this;

            teamName[0] = "Team Red";
            teamName[1] = "Team Blue";
            indexMapSelected = SceneManager.GetActiveScene().buildIndex;
            if (indexMapSelected == 0) indexMapSelected = 1;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
        Time.timeScale = 1;
    }
}
