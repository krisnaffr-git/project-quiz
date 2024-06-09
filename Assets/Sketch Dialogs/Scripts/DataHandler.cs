using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Networking;
using System.Text;

public class DataHandler : MonoBehaviour
{
    //============================================================================================================================================
    //VALUES =====================================================================================================================================
    //============================================================================================================================================

    public bool GetScenarioFromWeb;

    public static DataHandler instance = null;
    public SpriteAtlas s_atlas;
    public ScenariosData sData;
    public ValuesData vData;

    private string SKey = "secret!";
    public string GetScenarioPHP_URL = "http://perfect-human.com/Dialogs/GetScenario.php";

    public static string UserName;
    public static string AvatarID;
    public static int Gender;

    //============================================================================================================================================
    //CLASSES ====================================================================================================================================
    //============================================================================================================================================

    [System.Serializable]
    public class ScenariosData
    {
        public List<ScenarioData> scenarios;
    }

    [System.Serializable]
    public class ScenarioData
    {
        public int ID;
        public string Name;

        public List<CommonConditions> Conditions;
        public List<ScenarioCharacter> Characters;
        public List<ScenarioStage> Stages;

        public ScenarioData(int s_id, string s_Name)
        {
            ID = s_id;
            Name = s_Name;

            Conditions = new List<CommonConditions>();
            Characters = new List<ScenarioCharacter>();
            Stages     = new List<ScenarioStage>();
        }
    }

    [System.Serializable]
    public class PersData
    {
        public List<ScenarioCharacter> characters;
    }

    [System.Serializable]
    public class ScenarioCharacter
    {
        public int ID;
        public int ScenarioID;
        public string Name;
        public int Place;
        public string ImageID;

        public List<ScenarioCharacterValues> CharValues;

        public ScenarioCharacter(int s_id, string s_name = "", int s_place = 0, string s_image = "", List<ScenarioCharacterValues> s_CharValues = null)
        {
            ID = s_id;
            Name = s_name;
            Place = s_place;
            ImageID = s_image;

            if (s_CharValues == null)
            {
                CharValues = new List<ScenarioCharacterValues>();
            }
            else
            {
                CharValues = s_CharValues;
            }           
        }
    }

    [System.Serializable]
    public class StagesData
    {
        public List<ScenarioStage> stages;
    }

    [System.Serializable]
    public class ScenarioStage
    {
        public int ID;        
        public int StageOrder;
        public int ScenarioID;
        public int PersonID;
        public string StageType;
        public int Special;
        public float Delay;

        public ScenarioReplica ScenarioReplica;

        public List<ScenarioReplica> ReplicaList;
        public List<CommonConditions> Conditions;

        public ScenarioStage(int s_StageOrder, int s_PersonID, string s_StageType = "Talk", int s_Special = 0, float s_Delay = 0)
        {
            StageOrder = s_StageOrder;
            PersonID = s_PersonID;
            StageType = s_StageType;
            Special = s_Special;
            Delay = s_Delay;

            ReplicaList = new List<ScenarioReplica>();
            Conditions = new List<CommonConditions>();
        }
    }

    [System.Serializable]
    public class ReplicsData
    {
        public List<ScenarioReplica> replics;
    }

    [System.Serializable]
    public class ScenarioReplica
    {
        public int ID;
        public int StageID;
        public string Text;
        public string ReplicaType;

        public List<CommonConditions> CheckConditions;
        public List<CommonConditions> SuccessOutcome;
        public List<CommonConditions> FailOutcome;

        public ScenarioReplica(int s_id, string s_Text, string s_ReplicaType)
        {
            ID = s_id;
            Text = s_Text;
            ReplicaType = s_ReplicaType;

            CheckConditions = new List<CommonConditions>();
            SuccessOutcome = new List<CommonConditions>();
            FailOutcome = new List<CommonConditions>();
        }
    }

    [System.Serializable]
    public class CommonConditions
    {
        public int Conditions_ID;      
        public int TypeOfCondition;
        public string StatToCheck;
        public int IDCheck;
        public int Min;
        public int Max;

        public CommonConditions(int s_IDCheck, int s_TypeOfCondition, string s_StatToCheck, int s_Min, int s_Max)
        {
            Conditions_ID = 1;
            TypeOfCondition = s_TypeOfCondition;
            StatToCheck = s_StatToCheck;
            IDCheck = s_IDCheck;
            Min = s_Min;
            Max = s_Max;
        }
    }     

    //----------------------------------------------------
    //RESOURCE DATA

    [System.Serializable]
    public class ValuesData
    {
        public List<ValueData> values;
        public ValuesData()
        {
            values = new List<ValueData>();
        }
    }

    [System.Serializable]
    public class ValueData
    {
        public int ValueID;
        public string Name;
        public string ImageID;
        public Sprite sprite;

        public ValueData(int ValueID_s, string Name_s, string ImageID_s)
        {
            ValueID = ValueID_s;
            Name = Name_s;
            ImageID = ImageID_s;
            sprite = instance.s_atlas.GetSprite(ImageID_s);
        }
    }

    [System.Serializable]
    public class ScenarioCharacterValues
    {
        public ValueData value;
        public int ValueID;
        public int Amount;

        public ScenarioCharacterValues(int ValueID_s = 0, int amount_s = 0)
        {
            if (ValueID_s != 0)
            {
                List<ValueData> sel_ValueData = instance.vData.values.FindAll(u => u.ValueID == ValueID_s);
                if (sel_ValueData.Count != 0)
                {
                    value = sel_ValueData[0];
                }
            }
            ValueID = ValueID_s;
            Amount = amount_s;           
        }
    }

    //============================================================================================================================================
    //INITIALIZATION =============================================================================================================================
    //============================================================================================================================================

    void Awake()
    {
        if (instance == null) { instance = this; }
    }

    private void Start()
    {
        ScenarioHandler.instance.StartIntroScenario();
    }

    //============================================================================================================================================
    //SCENARIO MANUAL UPDATE =====================================================================================================================
    //============================================================================================================================================

    public static CommonConditions AddNewCondition(List<CommonConditions> cc, int s_IDCheck = 0, int s_TypeOfCondition = 2, string s_StatToCheck = "Replica", int s_Min = 1, int s_Max = 0)
    {
        CommonConditions newScenarioConditions = new CommonConditions(s_IDCheck, s_TypeOfCondition, s_StatToCheck, s_Min, s_Max);
        cc.Add(newScenarioConditions);
        return newScenarioConditions;
    }

    public static ScenarioReplica AddReplica(List<ScenarioReplica> cr, int s_id, string s_Text, string s_ReplicaType = "Story")
    {
        ScenarioReplica newScenarioReplica = new ScenarioReplica(s_id, s_Text, s_ReplicaType);
        cr.Add(newScenarioReplica);
        return newScenarioReplica;
    }

    public static ScenarioStage AddNewStage(List<ScenarioStage> cl, int s_StageOrder, int s_PersonID, string s_StageType = "Talk", int s_Special = 0, float s_Delay = 0)
    {
        ScenarioStage ScenarioStage = new ScenarioStage(s_StageOrder, s_PersonID, s_StageType, s_Special, s_Delay);
        cl.Add(ScenarioStage);
        return ScenarioStage;
    }

    public static ScenarioCharacter AddNewScenarioCharacter(List<ScenarioCharacter> cc, int id, string s_name, int s_place, string s_image = "")
    {
        ScenarioCharacter ScenarioCharacter = new ScenarioCharacter(id, s_name, s_place, s_image);
        cc.Add(ScenarioCharacter);
        return ScenarioCharacter;
    }

    //============================================================================================================================================
    //GET IMAGE FROM WEB =========================================================================================================================
    //============================================================================================================================================

    public Sprite GetSpriteFromAtlas(string SpriteID, string Prefix = "")
    {
        string FullId = Prefix + SpriteID.ToString();
        return s_atlas.GetSprite(FullId);
    }

    //============================================================================================================================================
    //SCENARIO WEB UPDATE ========================================================================================================================
    //============================================================================================================================================
    
    public void Get_Scenario_from_Web()
    {
        StartCoroutine(Get_Scenario_Web_Process());
    }

    IEnumerator Get_Scenario_Web_Process()
    {
        WWWForm mForm = new WWWForm();
        mForm.AddField("hash", Md5Sum(SKey).ToLower());
        UnityWebRequest www = UnityWebRequest.Post(GetScenarioPHP_URL, mForm);
        yield return www.SendWebRequest();

        string result = www.downloadHandler.text;

        //check if we have some error
        if (www.error != null || result == string.Empty)
        {
            Debug.LogWarning(www.error);
        }
        else
        {
            Debug.Log(result);

            string[] mSplit = result.Split("|"[0]);

            if (mSplit[0] == "SUCCESS")
            {
                vData = JsonUtility.FromJson<ValuesData>(mSplit[1]);
                PersData Persdata = JsonUtility.FromJson<PersData>(mSplit[2]);
                sData = JsonUtility.FromJson<ScenariosData>(mSplit[3]);               
                StagesData Stagesdata = JsonUtility.FromJson<StagesData>(mSplit[4]);
                ReplicsData Replicsdata = JsonUtility.FromJson<ReplicsData>(mSplit[5]);

                foreach (ScenarioData scen in sData.scenarios)
                {
                    if (Persdata.characters != null)
                    {
                        List<ScenarioCharacter> sel_Pers = Persdata.characters.FindAll(u => u.ScenarioID == scen.ID);
                        if (sel_Pers.Count != 0)
                        {
                            foreach (ScenarioCharacter persd in sel_Pers)
                            {
                                foreach (ScenarioCharacterValues pv in persd.CharValues)
                                {
                                    List<ValueData> sel_ValueData = instance.vData.values.FindAll(u => u.ValueID == pv.ValueID);
                                    if (sel_ValueData.Count != 0)
                                    {
                                        pv.value = sel_ValueData[0];
                                    }
                                }    
                                scen.Characters.Add(persd);
                            }
                        }
                    }

                    if (Stagesdata.stages != null)
                    {
                        List<ScenarioStage> sel_stages = Stagesdata.stages.FindAll(u => u.ScenarioID == scen.ID);

                        if (sel_stages.Count != 0)
                        {
                            foreach (ScenarioStage stage in sel_stages)
                            {
                                List<ScenarioReplica> sel_replicas = Replicsdata.replics.FindAll(u => u.StageID == stage.ID);

                                foreach (ScenarioReplica rep in sel_replicas)
                                {
                                    rep.Text = rep.Text.Replace("\\n","\n");
                                }
                                stage.ReplicaList = sel_replicas;
                            }
                            scen.Stages = sel_stages;
                        }
                    }
                }
                CharacterHandler.instance.cur_player = Persdata.characters[0];
                ScenarioHandler.instance.StartScenario(sData.scenarios[0]);
            }
        }

        www.Dispose();
    }

    //============================================================================================================================================
    //MD5 ENCRIPT ================================================================================================================================
    //============================================================================================================================================

    public static string Md5Sum(string input)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++) { sb.Append(hash[i].ToString("X2")); }
        return sb.ToString();
    }
}
