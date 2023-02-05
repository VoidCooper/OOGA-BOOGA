using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfTimesScript : MonoBehaviour
{
    private ScaledOneshotTimer StartZoomTimer;
    private ScaledOneshotTimer ZoomTimer;
    public Camera EndCamera;
    public Transform FollowTarget;
    public GameObject EndModel;
    private Health m_playerHealth;
    private bool isDead = false;

    [SerializeField]
    private GameObject GameOverObjectScreenthing;
    private void Awake()
    {
        StartZoomTimer = gameObject.AddComponent<ScaledOneshotTimer>();
        ZoomTimer = gameObject.AddComponent<ScaledOneshotTimer>();
        StartZoomTimer.OnTimerCompleted += StartZoomTimer_OnTimerCompleted;
        ZoomTimer.OnTimerCompleted += ZoomTimer_OnTimerCompleted;
    }

    private void Start()
    {
        m_playerHealth = GlobalReferenceManager.Instance.Player.GetComponent<Health>();
        GameManager.Instance.OnEndIsNigh += Instance_OnEndIsNigh;
        m_playerHealth.IsDying += playerHealth_IsDying;
    }

    private void OnDestroy()
    {
        StartZoomTimer.OnTimerCompleted -= StartZoomTimer_OnTimerCompleted;
        GameManager.Instance.OnEndIsNigh -= Instance_OnEndIsNigh;
        m_playerHealth.IsDying -= playerHealth_IsDying;
        ZoomTimer.OnTimerCompleted -= ZoomTimer_OnTimerCompleted;
    }
    private void ZoomTimer_OnTimerCompleted()
    {
        GameOverObjectScreenthing.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void StartZoomTimer_OnTimerCompleted()
    {
        if (isDead)
            return;

        ZoomTimer.StartTimer(10);
        EndCamera.gameObject.SetActive(true);
        EndCamera.transform.position = Camera.main.transform.position;
        Camera.main.gameObject.SetActive(false);
        EndModel.SetActive(true);
    }

    private void Update()
    {
        if (!ZoomTimer.IsRunning)
            return;
        EndCamera.transform.LookAt(FollowTarget);
        EndCamera.transform.Translate(Vector3.forward * -Time.deltaTime * 3);
    }

    private void playerHealth_IsDying()
    {
        if (isDead)
            return;

        isDead = true;

        GameManager.Instance.PauseGame();
        ZoomTimer.StartTimer(10);
        EndCamera.gameObject.SetActive(true);
        EndCamera.transform.position = Camera.main.transform.position;
        Camera.main.gameObject.SetActive(false);
        EndModel.SetActive(true);
        var rb = EndModel.AddComponent<Rigidbody>();
        rb.AddTorque(new Vector3(0, 10, 0));
        GlobalReferenceManager.Instance.Player.GetComponent<PlayerFPSMovement>().enabled = false;
    }

    private void Instance_OnEndIsNigh()
    {
        if (isDead)
            return;

        StartZoomTimer.StartTimer(5);
    }
}
