using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShambaFollowCamera : CameraBehaviour
{
    [Tooltip ("The second to set the cinemachines blend time to when using SetBlendTime")]
    [SerializeField] private float blendTime = 0.1f;

    public void SetBlendTime()
    {
        Debug.Log("FollowCamera");
        if (cineBrain != null)
        {
            cineBrain.m_DefaultBlend.m_Time = blendTime;
        }
    }
}
