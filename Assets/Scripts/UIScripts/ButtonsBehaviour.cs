using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsBehaviour : MonoBehaviour
{
    public void LoadLevel(string sceneToLoad)
    {
        LevelManager.Instance.ChangeLevel(sceneToLoad);
    }

}