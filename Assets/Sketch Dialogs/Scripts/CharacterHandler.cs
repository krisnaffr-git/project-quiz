using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public static CharacterHandler instance = null;

    public DataHandler.ScenarioCharacter cur_player;

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
    //VALUES HANDLER =============================================================================================================================
    //============================================================================================================================================

    public void AppendValue(DataHandler.ScenarioCharacter Character, string Attribute, int NewValue)
    {
        List<DataHandler.ScenarioCharacterValues> cs = Character.CharValues.FindAll(u => u.value.Name == Attribute);
        if (cs.Count != 0)
        {
            cs[0].Amount = cs[0].Amount + NewValue;
        }
        else
        {
            List<DataHandler.ValueData> values_list = DataHandler.instance.vData.values.FindAll(u => u.Name == Attribute);
            if (values_list.Count != 0)
            {
                Character.CharValues.Add(new DataHandler.ScenarioCharacterValues(values_list[0].ValueID, NewValue));
            }                      
        }
    }

    public int GetValue(DataHandler.ScenarioCharacter Character, string Attribute)
    {
        int result = 0;
        List<DataHandler.ScenarioCharacterValues> cs = Character.CharValues.FindAll(u => u.value.Name == Attribute);
        if (cs.Count != 0)
        {
            result = cs[0].Amount;
        }
        return result;
    }

}
