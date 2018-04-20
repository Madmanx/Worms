using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    private BoxCollider2D spawnArea;
    [SerializeField]
    private GameObject wormPrefab;

    private int nbWormsByTeam;

    private void Start()
    {
        nbWormsByTeam = GameManager.Instance.NbWorms;
        spawnArea = GetComponent<BoxCollider2D>();
        SpawnWorm();
    }

    private void SpawnWorm()
    {
        for(int i = 0; i<nbWormsByTeam; i++)
        {
            GameObject worm = Instantiate(wormPrefab);
            float posXrand = spawnArea.transform.position.x + Random.Range(-spawnArea.size.x / 2.0f, spawnArea.size.x / 2.0f);
            worm.transform.position = new Vector2(posXrand, spawnArea.transform.position.y);

            worm.GetComponent<WormInfo>().Pseudo = GetNextPossibleName();
            worm.GetComponent<WormInfo>().Color = Color.red;
            worm.GetComponent<WormInfo>().MyTeam = TeamState.Team1;

            GameLoopManager.Instance.WormsTeam1.Add(worm.GetComponent<WormInfo>());
        }

        for (int i = 0; i < nbWormsByTeam; i++)
        {
            GameObject worm = Instantiate(wormPrefab);
            float posXrand = spawnArea.transform.position.x + Random.Range(-spawnArea.size.x / 2.0f, spawnArea.size.x / 2.0f);
            worm.transform.position = new Vector2(posXrand, spawnArea.transform.position.y);

            worm.GetComponent<WormInfo>().Pseudo = GetNextPossibleName();
            worm.GetComponent<WormInfo>().Color = Color.blue;
            worm.GetComponent<WormInfo>().MyTeam = TeamState.Team2;

            GameLoopManager.Instance.WormsTeam2.Add(worm.GetComponent<WormInfo>());
        }
        
        GameLoopManager.Instance.ToggleCurrentWormInCurrentTeam(true);
        GameLoopManager.Instance.Activate = true;
    }

    public void SpawnCrate()
    {
        CrateType crateType = (CrateType)Random.Range(1, DatabaseManager.Instance.Db.ListCrate.Count + 1);
        float posXrand = spawnArea.transform.position.x + Random.Range(-spawnArea.size.x / 2.0f, spawnArea.size.x / 2.0f);
        DatabaseManager.Instance.SpawnCrate(crateType, new Vector2(posXrand, spawnArea.transform.position.y));

    }

    // Façade
    List<string> listPossibleNames = new List<string>() { "Toto", "Tata", "Titi", "Tutu", "Jean", "Bob", "Mickeal", "Jose", "Ponpon", "Superboy", "Anthony", "Remi", "Sebdami", "Olivier", "Gwen", "Anais" };
    public string GetNextPossibleName()
    {
        if (listPossibleNames.Count == 0)
            return "Lamba";

        int rand = Random.Range(0, listPossibleNames.Count);
        string s = listPossibleNames[rand];

        listPossibleNames.RemoveAt(rand);
        return s;
    }
}
