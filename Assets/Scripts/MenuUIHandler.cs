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
}
