using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerJumpScript.instance != null)
        {
            PlayerJumpScript.instance.SetPower(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerJumpScript.instance.SetPower(false);
    }

}
