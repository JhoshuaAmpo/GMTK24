using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerGrowth : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Rate that player size increase per trait point earned")]
    private float ScaleIncreaseRate;

    [SerializeField]
    [Tooltip("Rate that camera ortho size increases per trait point earned")]
    private float CameraOrthoSizeIncreaseRate;

    CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Awake()
    {
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    public void GainTraitPoints(int tp) {
        float sR = ScaleIncreaseRate * tp;
        transform.localScale += new Vector3(sR, sR, sR);
        virtualCamera.m_Lens.OrthographicSize += CameraOrthoSizeIncreaseRate * tp;
        virtualCamera.m_Lens.OrthographicSize = Mathf.Min(virtualCamera.m_Lens.OrthographicSize, 500f);
        transform.localScale = Vector3.Min(transform.localScale, new(100f, 100f, 100f));
    }
}
