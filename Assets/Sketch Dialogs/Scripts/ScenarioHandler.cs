using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScenarioHandler : MonoBehaviour
{
    //============================================================================================================================================
    //VALUES =====================================================================================================================================
    //============================================================================================================================================

    public static ScenarioHandler instance = null;

    //UI VARIABLES -------------------------------------------------------------------------------------------------------------------------------

    public GameObject QuestionsPanel;
    public Text Questions_Text;

    public GameObject ExitButton;
    public GameObject BackButton;
    public GameObject NextButton;     

    public GameObject PlayerCustomizePanel;
    public GameObject PlayerNameInputPanel;
    public GameObject PlayerAvatarsPanel;
    public GameObject PlayerGenderChoosePanel;

    //CHARACTERS UI VARIABLES --------------------------------------------------------------------------------------------------------------------

    public GameObject NPC1;
    public GameObject NPC2;
    public GameObject NPC3;
    public GameObject PLAYER;

    private GameObject PLAYER_Dialogue;
    private GameObject NPC_Dialogue;

    //NPC UI VARIABLES ---------------------------------------------------------------------------------------------------------------------------

    public GameObject NPC_Dialogue_Speach;
    public GameObject NPC_Dialogue_Story;
    public GameObject NPC_Dialogue_Shout;
    public GameObject NPC_Dialogue_Thoughts;
    public GameObject NPC_Dialogue_Actions;

    public Text NPC_Dialogue_Speach_Text;
    public Text NPC_Dialogue_Story_Text;
    public Text NPC_Dialogue_Shout_Text;
    public Text NPC_Dialogue_Thoughts_Text;
    public Text NPC_Dialogue_Actions_Text;

    //UI VARIABLES -------------------------------------------------------------------------------------------------------------------------------

    public GameObject PLAYER_Dialogue_Speach;
    public GameObject PLAYER_Dialogue_Story;
    public GameObject PLAYER_Dialogue_Shout;
    public GameObject PLAYER_Dialogue_Thoughts;
    public GameObject PLAYER_Dialogue_Actions;

    public Text PLAYER_Dialogue_Speach_Text;
    public Text PLAYER_Dialogue_Story_Text;
    public Text PLAYER_Dialogue_Shout_Text;
    public Text PLAYER_Dialogue_Thoughts_Text;
    public Text PLAYER_Dialogue_Actions_Text;

    //VARIANT UI VARIABLES ------------------------------------------------------------------------------------------------------------------------

    public GameObject Variant_Panels;

    public GameObject Variant_1;
    public GameObject Variant_2;
    public GameObject Variant_3;

    public GameObject Variant_Line;

    public Text Variant_Text_1;
    public Text Variant_Text_2;
    public Text Variant_Text_3;

    //UI PANEL STATE VARIABLES --------------------------------------------------------------------------------------------------------------------

    private bool NPCShowed;
    private bool QuestionsShowed;
    private bool GenerationShowed;
    private bool VariantsShowed;
    private bool PlayerShowed;
    private bool BackButtonShowed;
    private bool ReplicaShowed;

    private bool Resetting_in_progress;

    //UI PANEL STATE VARIABLES --------------------------------------------------------------------------------------------------------------------

    public GameObject StatPrefab;
    public Transform TableGeneration;
    public GameObject GenerationPanel;
    public InputField debugInput;


    public InputField PlayerNameInputField;
    public SnapScrolling curSnapScrolling;

    //SCENARIO STATE VARIABLES --------------------------------------------------------------------------------------------------------------------

    private DataHandler.ScenarioReplica CurPlayerAnswer;
    private DataHandler.ScenarioData CurScenario;
    
    private int CurScenarioStageInt;
    private DataHandler.ScenarioStage CurScenarioStage;
    private DataHandler.ScenarioStage PrevScenarioStage;

    private int NPC1_curposition;
    private int NPC2_curposition;
    private int NPC3_curposition;

    private int CurReplicaSender = 0;
    private int PrevReplicaSender = 0;
    private string PrevStageType = "";
    private string CurStageType = "";    

    //SCENARIO SESSION VARIABLES -----------------------------------------------------------------------------------------------------------------

    private List<DataHandler.ScenarioStage> CurScenarioStagesList;
    private List<DataHandler.ScenarioCharacter> CurScenarioCharacters;

    private List<ScenarioResult> ScenarioResultsList = new List<ScenarioResult>();
    private List<DataHandler.ScenarioReplica> selReplicas = new List<DataHandler.ScenarioReplica>();
    private List<DataHandler.ScenarioReplica> used_replicas;
    private List<GameObject> GenerationStatsObjects = new List<GameObject>();

    //IMAGE VARIABLES ----------------------------------------------------------------------------------------------------------------------------

    [SerializeField]
    public Image PLAYER_Image;

    public Sprite Replica;
    public Sprite Thoughts;
    public Sprite Shout;
    public Sprite Action;
       
    //============================================================================================================================================
    //CLASSES ====================================================================================================================================
    //============================================================================================================================================

    public class ScenarioResult
    {
        public int ID;
        public int ModifierType;
        public string ModifierStat;
        public int Amount;

        public ScenarioResult()
        {
            ;
        }
        public ScenarioResult(int ID_p, string ModifierStat_p, int ModifierType_p, int Amount_p)
        {
            ID = ID_p;
            ModifierStat = ModifierStat_p;
            ModifierType = ModifierType_p;
            Amount = Amount_p;
        }
    }

    public class GenerationStats
    {
        public string ModStat;
        public int Type;
        public int Amount;

        public GenerationStats()
        {
            ;
        }
        public GenerationStats(string ModStat_p, int Type_p, int Amount_p)
        {
            ModStat = ModStat_p;
            Type = Type_p;
            Amount = Amount_p;
        }
    }

    //============================================================================================================================================
    //INITIALIZATION =============================================================================================================================
    //============================================================================================================================================

    public void Awake()
    {
        if (instance == null) { instance = this; }
    }

    //============================================================================================================================================
    //MAIN SCENARIO HANDLER ======================================================================================================================
    //============================================================================================================================================

    public void StartIntroScenario()
    {
        if (DataHandler.instance.GetScenarioFromWeb)
        {
            DataHandler.instance.Get_Scenario_from_Web();
        }
        else
        {
            DemoScenario.instance.AddDefaultValueData();
            DemoScenario.instance.DemoScenarioContent();
        }
    }

    public void StartScenario(DataHandler.ScenarioData scenarioToStart)
    {
        CurScenario = scenarioToStart;
        CurScenarioStagesList = scenarioToStart.Stages;
        CurScenarioCharacters = scenarioToStart.Characters;
        used_replicas = new List<DataHandler.ScenarioReplica>();
        Resetting_in_progress = false;

        foreach (DataHandler.ScenarioCharacter c in CurScenarioCharacters)
        {
            if (c.ID == 1)
            {
                NPC1.SetActive(true);
                NPC1_curposition = 1;
                NPC1.GetComponent<Animation>().Play("Show_Big_NPC_Start");
            }
            else if (c.ID == 2)
            {
                NPC2.SetActive(true);
                NPC2_curposition = 2;
                NPC2.GetComponent<Animation>().Play("Show_Small_NPC_Start");
            }
            else if (c.ID == 3)
            {
                NPC3.SetActive(true);
                NPC3_curposition = 3;
                NPC3.GetComponent<Animation>().Play("Show_Small_NPC_Start");
            }
        }

        PLAYER.SetActive(true);
        PLAYER.GetComponent<Animation>().Play("Show_Player_Start");
        PlayerShowed = true;

        StartStage(0);
    }

    //RESET SCENARIO ------------------------------------------------------------------------------------------------------------------------------

    public void ResetScenario()
    {
        Resetting_in_progress = true;

        CloseVariants();
        CloseQuestionPanel();
        CloseGeneration();

        used_replicas.Clear();
        PrevStageType = "";
        CurScenarioStageInt = 0;
        CurPlayerAnswer = null;

        StartCoroutine(ReturnDialogsAndRestart());
    }

    IEnumerator ReturnDialogsAndRestart()
    {
        MakeReplica(1, "Resetting scenario...");
        yield return new WaitForSeconds(2f);
        StartIntroScenario();
    }

    private void CloseScenario()
    {
        UIHandler.instance.ShowMessage("Quit demo!");
        Application.Quit();
    }

    //============================================================================================================================================
    //STAGE HANDLERS =============================================================================================================================
    //============================================================================================================================================

    private void NextStage()
    {
        if (!SystemActionsResolver()) { return; }
        CurScenarioStageInt++;
        StartStage(CurScenarioStageInt);
    }

    private void PrevStage()
    {
        CurScenarioStageInt--;
        StartStage(CurScenarioStageInt);
    }

    private void StartStage(int stageID)
    {
        CurScenarioStageInt = stageID;

        if (CurScenarioStagesList.Count >= (stageID + 1))
        {
            CurScenarioStage = CurScenarioStagesList[stageID];
            CurStageType = CurScenarioStage.StageType;

            if (!CheckConditions(CurScenarioStage.Conditions))
            {
                NextStage();
                return;
            }

            selReplicas.Clear();

            foreach (DataHandler.ScenarioReplica c in CurScenarioStage.ReplicaList)
            {
                var check = CheckConditions(c.CheckConditions);
                if (check) { selReplicas.Add(c); }
            }

            if (CurScenarioStage.Special == 0 && selReplicas.Count == 0)
            {
                NextStage();
                return;
            }

            if (PrevStageType == "Player_Name" || PrevStageType == "Player_Avatar" || PrevStageType == "Player_Gender" || PrevStageType == "Quiz" || PrevStageType == "Resolve_Quiz")
            {
                CloseQuestionPanel();
                CloseGeneration();
            }

            if (CurScenarioStage.Special == 1)
            {
                SystemAction(CurStageType, CurScenarioStage.PersonID);
            }
            else
            {
                StartNPC();

                if (CurScenarioStage.PersonID == 0) // 0 = Player
                {
                    MakeVariants();
                }
                else
                {
                    DataHandler.ScenarioReplica cr = selReplicas[0];
                    string ReplicaToSend = cr.Text;

                    if (ReplicaToSend == "")
                    {
                        NextStage();
                        return;
                    }
                    else
                    {
                        used_replicas.Add(cr);
                        MakeReplica(CurScenarioStage.PersonID, ReplicaToSend, cr.ReplicaType);
                    }
                }
            }

            PrevScenarioStage = CurScenarioStage;
            PrevStageType = CurStageType;
        }
        else
        {
            ResetScenario();
        }
    }

    //============================================================================================================================================
    //REPLICA HANDLERS ===========================================================================================================================
    //============================================================================================================================================

    private void StartNPC()
    {
        if (!NPCShowed)
        {
            if (NPC1_curposition == 1)
            {
                NPC1.GetComponent<Animation>().Play("Show_Big_NPC_Start");
            }
            else if (NPC1_curposition > 1)
            {
                NPC1.GetComponent<Animation>().Play("Show_Small_NPC_Start");
            }
            if (NPC2_curposition == 1)
            {
                NPC2.GetComponent<Animation>().Play("Show_Big_NPC_Start");
            }
            else if (NPC2_curposition > 1)
            {
                NPC2.GetComponent<Animation>().Play("Show_Small_NPC_Start");
            }
            if (NPC3_curposition == 1)
            {
                NPC3.GetComponent<Animation>().Play("Show_Big_NPC_Start");
            }
            else if (NPC3_curposition > 1)
            {
                NPC3.GetComponent<Animation>().Play("Show_Small_NPC_Start");
            }
        }

        NPCShowed = true;
    }

    IEnumerator DelayedReplicaShow(int ReplicaSender, string ReplicaText, string ReplicaType = "Replica", float secToShow = 0.5f)
    {
        yield return new WaitForSeconds(secToShow);
        
        NPC_Dialogue_Speach.SetActive(false);
        NPC_Dialogue_Story.SetActive(false);
        NPC_Dialogue_Shout.SetActive(false);
        NPC_Dialogue_Thoughts.SetActive(false);
        NPC_Dialogue_Actions.SetActive(false);

        PLAYER_Dialogue_Speach.SetActive(false);
        PLAYER_Dialogue_Story.SetActive(false);
        PLAYER_Dialogue_Shout.SetActive(false);
        PLAYER_Dialogue_Thoughts.SetActive(false);
        PLAYER_Dialogue_Actions.SetActive(false);

        if (ReplicaSender == 0)
        {
            if (ReplicaType == "Shout")
            {
                PLAYER_Dialogue_Shout.SetActive(true);
                PLAYER_Dialogue_Shout_Text.text = ReplicaText;
                PLAYER_Dialogue = PLAYER_Dialogue_Shout;
            }
            else if (ReplicaType == "Thoughts")
            {
                PLAYER_Dialogue_Thoughts.SetActive(true);
                PLAYER_Dialogue_Thoughts_Text.text = ReplicaText;
                PLAYER_Dialogue = PLAYER_Dialogue_Thoughts;
            }
            else if (ReplicaType == "Action")
            {
                PLAYER_Dialogue_Actions.SetActive(true);
                PLAYER_Dialogue_Actions_Text.text = ReplicaText;
                PLAYER_Dialogue = PLAYER_Dialogue_Actions;
            }
            else if (ReplicaType == "Story")
            {
                PLAYER_Dialogue_Actions.SetActive(true);
                PLAYER_Dialogue_Actions_Text.text = ReplicaText;
                PLAYER_Dialogue = PLAYER_Dialogue_Actions;
            }
            else
            {
                PLAYER_Dialogue_Speach.SetActive(true);
                PLAYER_Dialogue_Speach_Text.text = ReplicaText;
                PLAYER_Dialogue = PLAYER_Dialogue_Speach;
            }

            PLAYER_Dialogue.GetComponent<Animation>().Play("Dialog_Player_Start");
        }
        else
        {
            if (ReplicaType == "Shout")
            {
                NPC_Dialogue_Shout.SetActive(true);
                NPC_Dialogue_Shout_Text.text = ReplicaText;
                NPC_Dialogue = NPC_Dialogue_Shout;
            }
            else if (ReplicaType == "Thoughts")
            {
                NPC_Dialogue_Thoughts.SetActive(true);
                NPC_Dialogue_Thoughts_Text.text = ReplicaText;
                NPC_Dialogue = NPC_Dialogue_Thoughts;
            }
            else if (ReplicaType == "Action")
            {
                NPC_Dialogue_Actions.SetActive(true);
                NPC_Dialogue_Actions_Text.text = ReplicaText;
                NPC_Dialogue = NPC_Dialogue_Actions;
            }
            else if (ReplicaType == "Story")
            {
                NPC_Dialogue_Story.SetActive(true);
                NPC_Dialogue_Story_Text.text = ReplicaText;
                NPC_Dialogue = NPC_Dialogue_Story;
            }
            else
            {
                NPC_Dialogue_Speach.SetActive(true);
                NPC_Dialogue_Speach_Text.text = ReplicaText;
                NPC_Dialogue = NPC_Dialogue_Speach;
            }          
            NPC_Dialogue.GetComponent<Animation>().Play("Dialog_NPC_Start");
        }

        MoveDialogues(ReplicaSender);
    }

    //============================================================================================================================================
    //ANIMATIONS =================================================================================================================================
    //============================================================================================================================================

    private void MoveDialogues(int ReplicaSender)
    {
        if (ReplicaSender == 1)
        {
            if (1 != NPC1_curposition)
            {
                NPC1.GetComponent<Animation>().Play("Dialog_" + NPC1_curposition + "To1"); NPC1_curposition = 1;
                if (NPC2_curposition != 2 && NPC2_curposition != 0)
                {
                    NPC2.GetComponent<Animation>().Play("Dialog_" + NPC2_curposition + "To2"); NPC2_curposition = 2;
                }
                if (NPC3_curposition != 3 && NPC3_curposition != 0)
                {
                    NPC3.GetComponent<Animation>().Play("Dialog_" + NPC3_curposition + "To3"); NPC3_curposition = 3;
                }
            }
        }
        else if (ReplicaSender == 2)
        {
            if (1 != NPC2_curposition)
            {
                NPC2.GetComponent<Animation>().Play("Dialog_" + NPC2_curposition + "To1"); NPC2_curposition = 1;
                if (NPC1_curposition != 2 && NPC1_curposition != 0)
                {
                    NPC1.GetComponent<Animation>().Play("Dialog_" + NPC1_curposition + "To2"); NPC1_curposition = 2;
                }
                if (NPC3_curposition != 3 && NPC3_curposition != 0)
                {
                    NPC3.GetComponent<Animation>().Play("Dialog_" + NPC3_curposition + "To3"); NPC3_curposition = 3;
                }

            }
        }
        else if (ReplicaSender == 3)
        {
            if (1 != NPC3_curposition)
            {
                NPC3.GetComponent<Animation>().Play("Dialog_" + NPC3_curposition + "To1"); NPC3_curposition = 1;
                if (NPC1_curposition != 2 && NPC2_curposition != 0)
                {
                    NPC1.GetComponent<Animation>().Play("Dialog_" + NPC1_curposition + "To2"); NPC1_curposition = 2;
                }
                if (NPC2_curposition != 3 && NPC3_curposition != 0)
                {
                    NPC2.GetComponent<Animation>().Play("Dialog_" + NPC2_curposition + "To3"); NPC2_curposition = 3;
                }
            }
        }
    }

    //============================================================================================================================================
    //QUESTION HANDLER ===========================================================================================================================
    //============================================================================================================================================

    private void CloseQuestion()
    {
        if (QuestionsShowed)
        {
            QuestionsPanel.GetComponent<Animation>().Play("Fade");

            if (PrevStageType == "Player_Name")
            {
                PlayerNameInputPanel.GetComponent<Animation>().Play("Fade");
            }
            else if (PrevStageType == "Player_Avatar")
            {
                PlayerAvatarsPanel.GetComponent<Animation>().Play("Fade");
            }
            else if (PrevStageType == "Player_Gender")
            {
                PlayerGenderChoosePanel.GetComponent<Animation>().Play("Fade");
            }

            QuestionsShowed = false;
        }
    }

    private void CloseQuestionPanel()
    {
        if (QuestionsShowed)
        {
            CloseQuestion();
        }

        if (BackButtonShowed && CurStageType != "Quiz")
        {
            BackButton.GetComponent<Animation>().Play("Fade");
            BackButtonShowed = false;
        }
    }

    //============================================================================================================================================
    //SOLO REPLICA HANDLER =======================================================================================================================
    //============================================================================================================================================

    IEnumerator DelayedNPCExit(GameObject cNPC, float secToShow = 1f)
    {
        yield return new WaitForSeconds(secToShow);
        cNPC.SetActive(false);
    }

    private void CloseReplica()
    {
        if (PrevScenarioStage.Special != 1)
        {
            if (PrevReplicaSender == 0)
            {
                PLAYER_Dialogue.GetComponent<Animation>().Play("Dialog_Player_End");
            }
            else
            {
                NPC_Dialogue.GetComponent<Animation>().Play("Dialog_NPC_End");
            }
            ReplicaShowed = false;
        }
    }

    private void MakeReplica(int ReplicaSender, string ReplicaText, string ReplicaType = "Replica", float secToShow = 0.51f, bool closeReplica = true)
    {
        CurReplicaSender = ReplicaSender;

        if (!PlayerShowed)
        {
            PLAYER.GetComponent<Animation>().Play("Show_Player_Start");
            PlayerShowed = true;
        }

        if (ReplicaText.Contains("[UserName]"))
        {
            ReplicaText = ReplicaText.Replace("[UserName]", DataHandler.UserName);
        }

        if (closeReplica && ReplicaShowed)
        {
            CloseReplica();
        }

        ReplicaShowed = true;
        StartCoroutine(DelayedReplicaShow(ReplicaSender, ReplicaText, ReplicaType, secToShow));

        PrevReplicaSender = CurReplicaSender;
    }

    //============================================================================================================================================
    //VARIANTS HANDLERS ==========================================================================================================================
    //============================================================================================================================================

    private void MakeVariants()
    {      
        if (selReplicas.Count > 1)
        {
            if (ReplicaShowed)
            {
                CloseReplica();
            }           

            Variant_Panels.SetActive(true);

            if (!PlayerShowed && CurStageType != "Quiz")
            {
                PLAYER.GetComponent<Animation>().Play("Show_Player_Start");
                PlayerShowed = true;
            }

            int startId = 0;

            if(CurStageType == "Quiz")
            {
                Questions_Text.text = selReplicas[0].Text;
                startId = 1;
                Variant_Panels.GetComponent<Animation>().Play("Variants_Opros_Start");
            }
            else
            {
                Variant_Panels.GetComponent<Animation>().Play("Dialog_Player_Start");
            }

            Variant_1.SetActive(true);
            Variant_Text_1.text = selReplicas[startId].Text;

            Variant_2.SetActive(true);
            Variant_Text_2.text = selReplicas[startId + 1].Text;                       

            if (selReplicas.Count >= (startId + 3))
            {
                Variant_Line.SetActive(true);
                Variant_3.SetActive(true);
                Variant_Text_3.text = selReplicas[startId+2].Text;
            }
            else
            {
                Variant_Line.SetActive(false);
                Variant_3.SetActive(false);
            }

            VariantsShowed = true;
        }
        else if (selReplicas.Count == 1)
        {
            DataHandler.ScenarioReplica c = selReplicas[0];
            MakeReplica(CurScenarioStage.PersonID, c.Text, c.ReplicaType);
        }
    }
    
    private void CloseVariants()
    {
        if (VariantsShowed)
        {
            Variant_Panels.GetComponent<Animation>().Play("Fade");
            VariantsShowed = false;
        }       
    }

    private void ChooseVariant(int ReplicaVariant)
    {
        CloseVariants();
        DataHandler.ScenarioReplica curReplica = selReplicas[ReplicaVariant-1];
        used_replicas.Add(curReplica);

        CurPlayerAnswer = curReplica;
        CurScenarioStage.ScenarioReplica = curReplica;
        NextStage();
    }

    //============================================================================================================================================
    //SYSTEM ACTIONS HANDLERS ====================================================================================================================
    //============================================================================================================================================

    private void SystemAction(string StageType, int ReplicaSender)
    {
        if (PrevStageType == "Resolve_Quiz")
        {
            CloseGenerationResults();
        }

        PrevStageType = StageType;

        if (StageType == "Exit")
        {
            if (ReplicaSender == 1)
            {
                NPC1.GetComponent<Animation>().Play("Show_Player_End");
                StartCoroutine(DelayedNPCExit(NPC1));
                NPC1_curposition = 0;
            }
            else if (ReplicaSender == 2)
            {
                NPC2.GetComponent<Animation>().Play("Show_Player_End");
                StartCoroutine(DelayedNPCExit(NPC2));
                NPC2_curposition = 0;
            }
            else if (ReplicaSender == 3)
            {
                NPC3.GetComponent<Animation>().Play("Show_Player_End");
                StartCoroutine(DelayedNPCExit(NPC3));
                NPC3_curposition = 0;
            }
            NextStage();
        }
        else if(StageType == "Finish")
        {
            foreach (ScenarioResult c in ScenarioResultsList)
            {
                CharacterHandler.instance.AppendValue(CharacterHandler.instance.cur_player, c.ModifierStat, c.Amount);
            }
            ResetScenario();
        }
        else if (StageType == "Player_Name" || StageType == "Player_Avatar" || StageType == "Player_Gender" || StageType == "Quiz" || StageType == "Resolve_Quiz")
        {
            if (!QuestionsShowed && !GenerationShowed)
            {
                if (NPCShowed)
                {
                    if (NPC1_curposition != 0)
                    {
                        NPC1.GetComponent<Animation>().Play("Show_Player_End");
                    }
                    if (NPC2_curposition != 0)
                    {
                        NPC2.GetComponent<Animation>().Play("Show_Player_End");
                    }
                    if (NPC3_curposition != 0)
                    {
                        NPC3.GetComponent<Animation>().Play("Show_Player_End");
                    }

                    NPCShowed = false;
                }                               

                if (StageType == "Quiz" && PlayerShowed)
                {
                    PLAYER.GetComponent<Animation>().Play("Show_Player_End");
                    PlayerShowed = false;
                }

                if (!BackButtonShowed && StageType != "Player_Name" && StageType != "Player_Avatar" && StageType != "Player_Gender")
                {
                    BackButton.GetComponent<Animation>().Play("Rise");
                    BackButtonShowed = true;
                }

                if (ReplicaShowed)
                {
                    CloseReplica();
                }
            }
                                 
            if(StageType == "Resolve_Quiz")
            {
                CloseVariants();
                CloseQuestion();
                GenerationShowed = true;
                GenerationPanel.SetActive(true);
                GenerationPanel.GetComponent<Animation>().Play("Rise");
            }
            else
            {               
                QuestionsShowed = true;
                PlayerCustomizePanel.SetActive(true);
                QuestionsPanel.SetActive(true);
                QuestionsPanel.GetComponent<Animation>().Play("Rise");
            }

            if (StageType == "Player_Name")
            {
                Questions_Text.text = "Please enter your name bellow!";

                PlayerNameInputPanel.SetActive(true);
                PlayerNameInputPanel.GetComponent<Animation>().Play("Rise");
            }
            else if (StageType == "Player_Avatar")
            {
                Questions_Text.text = "Choose your avatar!";

                PlayerAvatarsPanel.SetActive(true);
                PlayerAvatarsPanel.GetComponent<Animation>().Play("Rise");
            }
            else if (StageType == "Player_Gender")
            {
                Questions_Text.text = "Choose your gender!";

                PlayerGenderChoosePanel.SetActive(true);
                PlayerGenderChoosePanel.GetComponent<Animation>().Play("Rise");
            }
            else if (StageType == "Quiz")
            {        
                MakeVariants();
            }
            else if (StageType == "Resolve_Quiz")
            {
                EventsActionsResolver();

                List<GenerationStats> GenerationStatsList = new List<GenerationStats>();

                foreach (ScenarioResult c in ScenarioResultsList)
                {
                    int result_ID = c.ID;
                    int result_ModType = c.ModifierType;
                    string result_ModStat = c.ModifierStat;
                    int result_Amount = c.Amount;

                    List<GenerationStats> sel_GenerationStats = GenerationStatsList.FindAll(u => u.ModStat == result_ModStat);
                    if (sel_GenerationStats.Count != 0)
                    {
                        GenerationStats newGenerationStats = sel_GenerationStats[0];
                        newGenerationStats.Amount = newGenerationStats.Amount + result_Amount;
                    }
                    else
                    {
                        GenerationStats newGenerationStats = new GenerationStats(result_ModStat, result_ModType, result_Amount);
                        GenerationStatsList.Add(newGenerationStats);
                    }
                }

                GenerationStatsList.Sort((a, b) => b.Type.CompareTo(a.Type));
                
                foreach (GenerationStats gl in GenerationStatsList)
                {
                    List<DataHandler.ValueData> sel_valdata = DataHandler.instance.vData.values.FindAll(u => u.Name == gl.ModStat);
                    if (sel_valdata.Count > 0)
                    {
                        GameObject sp = Instantiate(StatPrefab) as GameObject;
                        StatInfo spi = sp.GetComponent<StatInfo>();

                        spi.vData = sel_valdata[0];
                        spi.name = gl.ModStat;
                        spi.Amount = gl.Amount;
                        spi.UpdateText();

                        sp.transform.SetParent(TableGeneration, false);
                        GenerationStatsObjects.Add(sp);
                    }
                }                
            }
        }       
    }
    private void CloseGeneration()
    {
        if (GenerationShowed)
        {
            CloseGenerationResults();
            GenerationShowed = false;
        }
    }

    private void CloseGenerationResults()
    {
        if (GenerationShowed)
        {
            foreach (GameObject go in GenerationStatsObjects)
            {
                Destroy(go);
            }
            GenerationStatsObjects.Clear();
            GenerationPanel.GetComponent<Animation>().Play("Fade");           
        }
    }        

    private bool SystemActionsResolver()
    {
        bool result = true;
        if (PrevStageType == "Player_Name")
        {
            string PlayerName = PlayerNameInputField.text;
            if (PlayerName == "")
            {
                UIHandler.instance.ShowMessage("You have not chosen a name for yourself!");
                result = false;
            }
            else
            {
                PlayerPrefs.SetString("UserName", PlayerName);
                DataHandler.UserName = PlayerName;
            }
        }
        else if (PrevStageType == "Player_Avatar")
        {
            string curAvatarSelected = (curSnapScrolling.selectedPanID + 1).ToString();
            PlayerPrefs.SetString("AvatarID", curAvatarSelected);
            DataHandler.AvatarID = curAvatarSelected;
            SetUserAvatar();
        }

        if (Resetting_in_progress)
        {
            result = false;
        }

        return result;
    }

    //============================================================================================================================================
    //BUTTON HANDLERS ============================================================================================================================
    //============================================================================================================================================

    private void SetUserAvatar()
    {
        PLAYER_Image.sprite = DataHandler.instance.GetSpriteFromAtlas(DataHandler.AvatarID, "Avatar_");
    }

    private void GenderChoose(int gender)
    {
        PlayerPrefs.SetInt("Gender", gender);
        DataHandler.Gender = gender;
        NextStage();
    }

    //============================================================================================================================================
    //CHECK CONDITIONS HANDLERS ==================================================================================================================
    //============================================================================================================================================

    private void EventsActionsResolver()
    {
        ScenarioResultsList.Clear();

        foreach (DataHandler.ScenarioStage stage in CurScenarioStagesList)
        {
            if (stage.ScenarioReplica != null)
            {
                List<DataHandler.CommonConditions> SuccessOutcomeList = stage.ScenarioReplica.SuccessOutcome;

                if (SuccessOutcomeList != null)
                {
                    foreach (DataHandler.CommonConditions SuccessOutcome in SuccessOutcomeList)
                    {
                        ScenarioResult newScenarioResult = new ScenarioResult(SuccessOutcome.Conditions_ID, SuccessOutcome.StatToCheck, SuccessOutcome.TypeOfCondition, SuccessOutcome.Min);
                        ScenarioResultsList.Add(newScenarioResult);
                    }
                }
            }
        }
    }

    private bool CheckConditions(List<DataHandler.CommonConditions> cond_list)
    {
        bool condPassed = true;

        foreach (DataHandler.CommonConditions cond in cond_list)
        {
            if (cond.Conditions_ID != 0)
            {
                if (cond.TypeOfCondition == 2)
                {
                    List<DataHandler.ScenarioReplica> sel_replica_used = used_replicas.FindAll(u => u.ID == cond.IDCheck);
                    if (sel_replica_used.Count == 0)
                    {
                        condPassed = false;
                    }
                }
                else if (cond.TypeOfCondition == 1)
                {
                    int curValue = CharacterHandler.instance.GetValue(CharacterHandler.instance.cur_player, cond.StatToCheck);
                    if ((cond.Min != 0 && curValue < cond.Min) || (cond.Max != 0 && curValue > cond.Max))
                    {
                        condPassed = false;
                        break;
                    }
                }
            }
        }

        return condPassed;
    }
}
