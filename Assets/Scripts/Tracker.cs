using UnityEngine;

public class Tracker : MonoBehaviour
{
    public static Tracker Instance;

    public string playerName;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
