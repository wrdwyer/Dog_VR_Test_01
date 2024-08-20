using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DogVR;
using FMODUnity;
using Unity6Test;
using TMPro;

public class GameStartMenu : MonoBehaviour
    {
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject options;
    public GameObject about;

    [Header("Main Menu Buttons")]
    public Button startButton;
    public Button optionButton;
    public Button aboutButton;
    public Button quitButton;

    public List<Button> returnButtons;

    [SerializeField]
    private StateManager stateManager;
    [SerializeField]
    private GameObject gameUI;
    [SerializeField]
    SceneFader sceneFader;
    [SerializeField]
    private GameObject getCamera;
    [SerializeField]
    private NewIntVariable levelMaxBones;
    [SerializeField]
    private TextMeshProUGUI maxBonesScoreText;
    private bool gameStarted = false;
      
    public string backgroundMusicEvent;

    private FMOD.Studio.EventInstance backgroundMusicInstance;


    // Start is called before the first frame update
    void Start()
        {

        EnableMainMenu();
        backgroundMusicInstance = RuntimeManager.CreateInstance(backgroundMusicEvent);
        backgroundMusicInstance.start();
        //Hook events
        startButton.onClick.AddListener(StartGame);
        optionButton.onClick.AddListener(EnableOption);
        aboutButton.onClick.AddListener(EnableAbout);
        quitButton.onClick.AddListener(QuitGame);

        foreach (var item in returnButtons)
            {
            item.onClick.AddListener(EnableMainMenu);
            }
        }

    public void LoadLevelOne()
        {
        levelMaxBones.value = 6;
        maxBonesScoreText.text = levelMaxBones.value.ToString();
        stateManager.UpdateGameState(StateManager.GameState.Playing);
        gameUI.SetActive(false);
        Debug.Log("Loading Level One");
        }
    public void QuitGame()
        {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        }

    public void StartGame()
        {
        if (!gameStarted)
            {
            StartCoroutine(StartGameFadeSequence());
            getCamera.SetActive(true);
            gameStarted = true;
            }
        else
            {
            return;
            }
        Debug.Log("Starting Game...");
        //SceneTransitionManager.singleton.GoToSceneAsync(1);
        }

    public void HideAll()
        {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);
        }

    public void EnableMainMenu()
        {
        mainMenu.SetActive(true);
        options.SetActive(false);
        about.SetActive(false);
        }
    public void EnableOption()
        {
        mainMenu.SetActive(false);
        options.SetActive(true);
        about.SetActive(false);
        }
    public void EnableAbout()
        {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(true);
        }

    private IEnumerator StartGameFadeSequence()
        {
        // Fade to black
        yield return StartCoroutine(sceneFader.FadeIn());
        backgroundMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        backgroundMusicInstance.release();
        Debug.Log("Fading in...");
        HideAll();
        Debug.Log("Hiding all...");
        LoadLevelOne();
        // Wait for the specified time
        yield return new WaitForSeconds(0.2f);
        // Fade back in
        yield return StartCoroutine(sceneFader.FadeOut());

        //stateManager.UpdateGameState(StateManager.GameState.Paused);
        }
    }
