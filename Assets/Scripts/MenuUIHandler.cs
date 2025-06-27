using UnityEngine;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    public string playerName;

    public void GetPlayerNameFromInputField()
    {
        var inputObj = GameObject.Find("PlayerNameInputField");
        if (inputObj == null)
        {
            Debug.LogError("PlayerNameInputField GameObject not found!");
            return;
        }

        var inputField = inputObj.GetComponent<TMP_InputField>();
        if (inputField == null)
        {
            Debug.LogError("TMP_InputField component not found on PlayerNameInputField!");
            return;
        }

        playerName = inputField.text;
        Tracker.Instance.playerName = playerName;

        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }

    // This method is called when the player clicks the Quit button
    public void QuitGame()
    {
        // Log the player name and high score before quitting
        Debug.Log($"Player Name: {Tracker.Instance.playerName}, High Score: {Tracker.Instance.highScore}");

        // Call SaveHighScore method from Tracker to save the high score
        Tracker.Instance.SaveHighScore();

        // Quit the application
        Application.Quit();
        
        // If running in the editor, stop playing
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
