using UnityEngine.SceneManagement;
using UnityEngine;

public class UITitleScreenController : MonoBehaviour {

    public void Replay()
    {
        SceneManager.LoadScene(GameManager.Instance.IndexMapSelected);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
