using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    const int START_MENU_SCENE_INDEX = 0;
    const int GAME_SCENE_INDEX = 1;
    const int GAMEOVER_SCENE_INDEX = 2;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadStartMenuScene()
    {
        SceneManager.LoadScene(START_MENU_SCENE_INDEX);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(GAME_SCENE_INDEX);
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(GAMEOVER_SCENE_INDEX);
    }
}
