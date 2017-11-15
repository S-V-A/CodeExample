using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Tools : MonoBehaviour
{
    [MenuItem("Tools/Clean PlayerPrefs")]
    static void CleanLaunchGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
