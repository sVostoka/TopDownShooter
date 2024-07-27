using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager s_instance = null;

    public static CoroutineManager s_Instance { get => s_instance; }

    private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else if (s_instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
