using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    /**************************************************************   VARIABLES   *****************************************************************/

    // VARIABLES EXPOSED TO THE EDITOR
    [Tooltip("A list of all the levels in the game, with the scenes that are supposed to be loaded in each one.")]
    [SerializeField] private List<Level> levels = new List<Level>();
    [Tooltip("The name of the level to begin at as specified in the levels list.")]
    [SerializeField] private string startLevel;
    [Tooltip("The name of the level to begin at as specified in the levels list")]

    // PUBLIC VARIABLES
    public Action LevelLoadedEvent;

    // PUBLIC STATIC VARIABLES
    public static LevelManager Instance;

    // PRIVATE VARIABLES FOR INTERNAL CLASS USE
    private Dictionary<string, List<string>> levelsDictionary = new Dictionary<string, List<string>>();
    private List<string> currentLoadedScenes = new List<string>();
    private Level currentLevel;
    private bool _levelLoaded = false;

    // OBSERVED VARIABLES
    public bool LevelLoaded
    {
        get { return _levelLoaded; }
        set
        {
            _levelLoaded = value;
            if (value == true)
            {
                LevelLoadedEvent?.Invoke();
            }
        }
    }

    /**************************************************************   METHODS   *****************************************************************/

    // UNITY EXECUTION DEPENDANT METHODS
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeLevelsDictionary();
        ChangeLevel(startLevel);
    }

    // CLASS METHODS
    private void InitializeLevelsDictionary()
    {
        foreach (Level level in levels)
        {
            levelsDictionary[level.levelName] = level.scenes;
        }
    }

    /// <summary>
    /// Compares two scene lists, and returns the scenes that are not shared by both lists.
    /// </summary>
    /// <param name="level1Scenes">A list of scenes in one level.</param>
    /// <param name="level2Scenes">A list of scenes in another level.</param>
    /// <returns> The scenes in level 1 that aren't in level 2.</returns>
    private List<string> TryGetScenes(List<string> level1Scenes, List<string> level2Scenes)
    {
        List<string> listOfScenes = new List<string>();
        if (level1Scenes.Count > 0)
        {
            foreach (string scene in level1Scenes)
            {
                Debug.Log("New Level Scenes: " + scene);
                if (!level2Scenes.Contains(scene))
                {
                    listOfScenes.Add(scene);
                }
            }
        }
        return listOfScenes;
    }

    /// <summary>
    /// Additively loads or unloads scenes, modifying a fill bar to show the operations progress.
    /// </summary>
    /// <param name="scenes">The list of scenes to load or unload.</param>
    /// <param name="sceneValue">The percentage the fill bar should go up for each scene loaded or unloaded.</param>
    /// <param name="load">True to load, false to unload.</param>
    /// <returns></returns>
    private IEnumerator ManageScenes(List<string> scenes, float sceneValue, bool load)
    {
        if (scenes.Count > 0)
        {
            foreach (string scene in scenes)
            {
                yield return StartCoroutine(SceneryManager.Instance.ModifyFillBar(sceneValue));
                var operation = load ? SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive) : SceneManager.UnloadSceneAsync(scene);
                if (load)
                {
                    currentLoadedScenes.Add(scene);
                }
                else
                {
                    currentLoadedScenes.Remove(scene);
                }
                yield return new WaitUntil(() => operation.isDone);
            }
        }
    }

    public void ChangeLevel(string newLevelName)
    {
        List<string> toUnload = new List<string>();
        List<string> toLoad = new List<string>();
        List<string> currentLevelScenes = new List<string>();
        List<string> newLevelScenes = new List<string>();

        // Get scenes for both levels
        if (currentLevel != null)
        {
            currentLevelScenes = currentLevel.scenes;
        }
        if (levelsDictionary.ContainsKey(newLevelName))
        {
            newLevelScenes = levelsDictionary[newLevelName];
        }

        //Add the scenes in the current level that aren't in the new level to be unloaded.
        toUnload = TryGetScenes(currentLevelScenes, newLevelScenes);

        //Add the scenes in the new level that aren't in the current level to be loaded
        toLoad = TryGetScenes(newLevelScenes, currentLevelScenes);

        StartCoroutine(HandleLevelChange(toUnload, toLoad, newLevelName));
    }

    private IEnumerator HandleLevelChange(List<string> toUnload, List<string> toLoad, string newLevelName)
    {
        int totalScenes = toUnload.Count + toLoad.Count;
        float sceneFillPercentage = 1.0f / totalScenes;
        SceneryManager.Instance.EnableLoadScreen();

        yield return StartCoroutine(ManageScenes(toUnload, sceneFillPercentage, false));
        yield return StartCoroutine(ManageScenes(toLoad, sceneFillPercentage, true));

        currentLevel = levels.Find(level => level.levelName == newLevelName);
    }
}

