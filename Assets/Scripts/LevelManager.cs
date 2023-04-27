using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameBoardPresenter gameBoardPresenter;
    [SerializeField] private StartLevelPopup startLevelPopup;

    private LevelData levelData;
    private int currentLevel = 0;
    private readonly string SAVE_KEY = "currentLevel";


    private void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("levels");
        levelData = JsonUtility.FromJson<LevelData>(jsonFile.text);
        currentLevel = LoadLevel();
        startLevelPopup.Setup(currentLevel, StartLevel);
    }

    private void OnApplicationQuit()
    {
        SaveLevel();
    }

    private void SaveLevel()
    {
        PlayerPrefs.SetInt(SAVE_KEY, currentLevel);
    }

    private int LoadLevel()
    {
        return PlayerPrefs.GetInt(SAVE_KEY);
    }

    private void OnCompleteLevel()
    {
        if (levelData.levels.Length - 1 > currentLevel)
        {
            currentLevel++;
            SaveLevel();
        }

        ReloadScene();
    }

    private void StartLevel()
    {
        gameBoardPresenter.Initialize(levelData.levels[currentLevel], OnCompleteLevel);
    }

    private void ReloadScene() //TODO: use pooling objrct and restup tiles. //tmp
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
