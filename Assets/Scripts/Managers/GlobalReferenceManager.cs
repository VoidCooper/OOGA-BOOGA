using UnityEngine;

public class GlobalReferenceManager : MonoBehaviour
{
    public static GlobalReferenceManager Instance { 
        get { 
            if (m_instance == null)
            {
                InstantiateSelf();
            }
            return m_instance;
        } 
    }
    private static GlobalReferenceManager m_instance;

    public Camera MainCamera;
    public GameObject Player;

    private static void InstantiateSelf()
    {
        if (m_instance != null)
            return;

        GameObject go = new GameObject("GlobalReferenceManager");
        GlobalReferenceManager grm = go.AddComponent<GlobalReferenceManager>();
        m_instance = grm;
        grm.MainCamera = Camera.main;
    }


}
