using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfTimesScript : MonoBehaviour
{
    private ScaledOneshotTimer StartZoomTimer;
    private ScaledOneshotTimer ZoomTimer;
    public Camera EndCamera;
    public Transform FollowTarget;
    public MeshRenderer EndModel;

    private void Awake()
    {
        StartZoomTimer = gameObject.AddComponent<ScaledOneshotTimer>();
        ZoomTimer = gameObject.AddComponent<ScaledOneshotTimer>();
        StartZoomTimer.OnTimerCompleted += StartZoomTimer_OnTimerCompleted;
    }

    private void StartZoomTimer_OnTimerCompleted()
    {
        ZoomTimer.StartTimer(10);
        EndCamera.gameObject.SetActive(true);
        EndCamera.transform.position = Camera.main.transform.position;
        Camera.main.gameObject.SetActive(false);
        EndModel.enabled = true;
    }

    private void Update()
    {
        if (!ZoomTimer.IsRunning)
            return;
        EndCamera.transform.LookAt(FollowTarget);
        EndCamera.transform.Translate(Vector3.forward * -Time.deltaTime * 3);
    }

    private void Start()
    {
        GameManager.Instance.OnEndIsNigh += Instance_OnEndIsNigh;
    }

    private void Instance_OnEndIsNigh()
    {
        StartZoomTimer.StartTimer(5);
    }
}
