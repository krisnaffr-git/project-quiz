using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class StatInfo : MonoBehaviour
{
    //============================================================================================================================================
    //VALUES =====================================================================================================================================
    //============================================================================================================================================

    public DataHandler.ValueData vData;
    public int Amount;
    public Image curImage;
    public Text Amount_text;
    public Text Name_text;

    //============================================================================================================================================
    //PROCEDURES =================================================================================================================================
    //============================================================================================================================================

    public void UpdateText()
    {
        Amount_text.text = Amount.ToString();
        Name_text.text = vData.Name.ToString();
        curImage.sprite = DataHandler.instance.GetSpriteFromAtlas(vData.ImageID);
    }

}
