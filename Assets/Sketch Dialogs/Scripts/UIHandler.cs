using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance = null;

    [Space(15)]
    
    public GameObject ShadowPanel;
    public GameObject MessagePanel;
    public Text MessageText;
    private bool MessageDescrSwitchedON;

    //============================================================================================================================================
    //INITIALIZATION =============================================================================================================================
    //============================================================================================================================================

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //============================================================================================================================================
    //MESSAGE ====================================================================================================================================
    //============================================================================================================================================

    IEnumerator MakeAndCloseMessageCoroutine(float sec, string message)
    {
        ShowMessage(message);
        yield return new WaitForSeconds(sec);
        CloseMessage();
    }

    public void MakeAndCloseMessage(float sec, string message)
    {
        StartCoroutine(MakeAndCloseMessageCoroutine(sec, message));
    }

    public void ShowMessage(string MessageString)
    {
        if (MessageText)
        {
            MessageText.text = MessageString;
            if (!MessageDescrSwitchedON)
            {
                if (MessagePanel)
                {
                    ShadowPanel.SetActive(true);
                    ShadowPanel.GetComponent<Animation>().Play("ShadowStart");

                    MessagePanel.SetActive(true);
                    MessagePanel.GetComponent<Animation>().Play("Rise");

                    MessageDescrSwitchedON = true;
                }

            }
        }
    }

    IEnumerator DelayedFadeMessage(float secToFade)
    {
        yield return new WaitForSeconds(secToFade);
        if (MessagePanel) MessagePanel.SetActive(false);
        if (ShadowPanel) { ShadowPanel.SetActive(false); };
    }

    public void CloseMessage()
    {
        if (MessageDescrSwitchedON)
        {
            StartCoroutine(DelayedFadeMessage(1f));
            if (ShadowPanel) { ShadowPanel.GetComponent<Animation>().Play("ShadowEnd"); }
            if (MessagePanel) MessagePanel.GetComponent<Animation>().Play("Fade");
            MessageDescrSwitchedON = false;
        }
    }
}
