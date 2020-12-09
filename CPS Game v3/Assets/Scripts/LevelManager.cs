using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClickStart()
    {
        SceneManager.LoadScene("Level_Select");
    }
    public void OnClickLevel1()
    {
        SceneManager.LoadScene("Level01");
    }
    public void OnClickLevel2()
    {
        SceneManager.LoadScene("Level02");
    }
    public void OnClickExit()
    {
        print("Application will quit if on Windows or Mac");
        Application.Quit();
    }
}
