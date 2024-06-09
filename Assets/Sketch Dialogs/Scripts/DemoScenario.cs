using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScenario : MonoBehaviour
{
    //============================================================================================================================================
    //VALUES =====================================================================================================================================
    //============================================================================================================================================

    public static DemoScenario instance = null;

    //============================================================================================================================================
    //INITIALIZATION =============================================================================================================================
    //============================================================================================================================================

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    //============================================================================================================================================
    //VALUE DATA =================================================================================================================================
    //============================================================================================================================================

    public void AddDefaultValueData()
    {
        DataHandler.instance.vData = new DataHandler.ValuesData();
        DataHandler.instance.vData.values.Add(new DataHandler.ValueData(1, "Strength", "C_Strength"));
        DataHandler.instance.vData.values.Add(new DataHandler.ValueData(2, "Agility", "C_Agility"));
        DataHandler.instance.vData.values.Add(new DataHandler.ValueData(3, "Stamina", "C_Stamina"));
        DataHandler.instance.vData.values.Add(new DataHandler.ValueData(4, "Intellect", "C_Intellect"));
        DataHandler.instance.vData.values.Add(new DataHandler.ValueData(5, "Charisma", "C_Charisma"));
    }

    //============================================================================================================================================
    //ADD DEMO CONTENT ===========================================================================================================================
    //============================================================================================================================================

    public void DemoScenarioContent()
    {
        // CREATE NEW SCENARIO
        DataHandler.ScenarioData IntroScenario = new DataHandler.ScenarioData(1, "IntroScenario");

        // CREATE NEW PLAYER
        DataHandler.ScenarioCharacter player = DataHandler.AddNewScenarioCharacter(IntroScenario.Characters, 0, "John doe", 0, "");

        // ADD PLAYER ABILITYS
        player.CharValues.Add(new DataHandler.ScenarioCharacterValues(1, 10));
        player.CharValues.Add(new DataHandler.ScenarioCharacterValues(2, 10));
        player.CharValues.Add(new DataHandler.ScenarioCharacterValues(3, 10));

        CharacterHandler.instance.cur_player = player;

        // CREATE 1-3 NPC
        DataHandler.AddNewScenarioCharacter(IntroScenario.Characters, 1, "Adam Smith", 1, "");
        DataHandler.AddNewScenarioCharacter(IntroScenario.Characters, 2, "Jane Smith", 2, "");
        DataHandler.AddNewScenarioCharacter(IntroScenario.Characters, 3, "Luke Smith", 3, "");

        // CREATE SCENARIO STAGES. STAGE 1.

        DataHandler.ScenarioStage s1 = DataHandler.AddNewStage(IntroScenario.Stages, 1, 2);
        DataHandler.AddReplica(s1.ReplicaList, 1, "Hello, adventurer!\n\nDo you need a demonstration of the dialogue system?", "Speach");

        // STAGE 2. TWO DECISIONS.

        DataHandler.ScenarioStage s2 = DataHandler.AddNewStage(IntroScenario.Stages, 2, 0);
        DataHandler.ScenarioReplica s2_r1 = DataHandler.AddReplica(s2.ReplicaList, 2, "Yes!");
        DataHandler.ScenarioReplica s2_r2 = DataHandler.AddReplica(s2.ReplicaList, 3, "No!");

        // STAGE 3. REACT ON DECISIONS.

        DataHandler.ScenarioStage s3 = DataHandler.AddNewStage(IntroScenario.Stages, 3, 3);

        // REACT ON STAGE_2.DECISION 1

        DataHandler.ScenarioReplica s3_r1 = DataHandler.AddReplica(s3.ReplicaList, 4, "Okay, then let's get started!", "Action");
        DataHandler.AddNewCondition(s3_r1.CheckConditions, s2_r1.ID);

        // REACT ON STAGE_2.DECISION 2

        DataHandler.ScenarioReplica s3_r2 = DataHandler.AddReplica(s3.ReplicaList, 5, "Okay, then see you soon! Bye!", "Thoughts");
        DataHandler.AddNewCondition(s3_r2.CheckConditions, s2_r2.ID);

        // STAGE 4. FINISH.

        DataHandler.ScenarioStage s4 = DataHandler.AddNewStage(IntroScenario.Stages, 4, 0, "Finish", 1);
        DataHandler.AddNewCondition(s4.Conditions, s3_r2.ID);

        // STAGE 5. INPUT PLAYER NAME.

        DataHandler.ScenarioStage s5 = DataHandler.AddNewStage(IntroScenario.Stages, 5, 0, "Player_Name", 1);
        DataHandler.AddNewCondition(s5.Conditions, s3_r1.ID);

        // STAGE 6. GREETINGS.

        DataHandler.ScenarioStage s6 = DataHandler.AddNewStage(IntroScenario.Stages, 6, 3);
        DataHandler.AddReplica(s6.ReplicaList, 6, "Good choice, [UserName]!\n\nMy name is Jane Smith. Very nice to meet you!", "Speach");

        // STAGE 7. INPUT AVATAR ID.

        DataHandler.ScenarioStage s7 = DataHandler.AddNewStage(IntroScenario.Stages, 7, 0, "Player_Avatar", 1);

        // STAGE 8. INPUT GENDER.

        DataHandler.ScenarioStage s8 = DataHandler.AddNewStage(IntroScenario.Stages, 8, 0, "Player_Gender", 1);

        // STAGE 9. INPUT GENDER.

        DataHandler.ScenarioStage s9 = DataHandler.AddNewStage(IntroScenario.Stages, 9, 1);
        DataHandler.AddReplica(s9.ReplicaList, 7, "Very nice! My name is Adam Smith!\n\nPlease take a short survey to test your abilities.", "Shout");

        // STAGE 10. THREE DECISIONS.

        DataHandler.ScenarioStage s10 = DataHandler.AddNewStage(IntroScenario.Stages, 10, 0);
        DataHandler.ScenarioReplica s10_r1 = DataHandler.AddReplica(s10.ReplicaList, 8, "Okay, I hope this is fast enough...");
        DataHandler.ScenarioReplica s10_r2 = DataHandler.AddReplica(s10.ReplicaList, 9, "Hmm... why do I need this?");
        DataHandler.ScenarioReplica s10_r3 = DataHandler.AddReplica(s10.ReplicaList, 10, "No! I don't want to waste my time");
       
        DataHandler.AddNewCondition(s10_r3.CheckConditions, 1, 1, "Strength", 25);

        // STAGE 11. REACT ON DECISIONS. 2 CHARACTER.

        DataHandler.ScenarioStage s11 = DataHandler.AddNewStage(IntroScenario.Stages, 11, 2);

        // REACT ON STAGE_10 DECISION 1

        DataHandler.ScenarioReplica s11_r1 = DataHandler.AddReplica(s11.ReplicaList, 11, "Of course... it won't take long. Maybe...", "Action");
        DataHandler.AddNewCondition(s11_r1.CheckConditions, s10_r1.ID);

        // STAGE 12. REACT ON DECISIONS. 3 CHARACTER.

        DataHandler.ScenarioStage s12 = DataHandler.AddNewStage(IntroScenario.Stages, 12, 3);

        // REACT ON STAGE_10. DECISION 2

        DataHandler.ScenarioReplica s12_r2 = DataHandler.AddReplica(s12.ReplicaList, 12, "We need to check your psychological profile to understand who we are dealing with.", "Story");
        DataHandler.AddNewCondition(s12_r2.CheckConditions, s10_r2.ID);
        
        // REACT ON STAGE_10. DECISION 3. AVAILABLE IF STRENGTH > 5

        DataHandler.ScenarioReplica s12_r3 = DataHandler.AddReplica(s12.ReplicaList, 13, "Okay, then we're done with you. Bye!", "Shout");
        DataHandler.AddNewCondition(s12_r3.CheckConditions, s10_r3.ID);        

        // STAGE 13. FINISH.

        DataHandler.ScenarioStage s13 = DataHandler.AddNewStage(IntroScenario.Stages, 13, 0, "Finish", 1);
        DataHandler.AddNewCondition(s13.Conditions, s12_r3.ID);

        // STAGE 14. QUIZ 1.

        DataHandler.ScenarioStage s14 = DataHandler.AddNewStage(IntroScenario.Stages, 14, 0, "Quiz", 1);
        DataHandler.ScenarioReplica s14_r1 = DataHandler.AddReplica(s14.ReplicaList, 14, "Looking through your family tree and photos of your ancestors, you unwittingly note that all your ancestors...");
        DataHandler.ScenarioReplica s14_r2 = DataHandler.AddReplica(s14.ReplicaList, 15, "...enjoyed exceptional health and lived a long life.", "Story");
        DataHandler.ScenarioReplica s14_r3 = DataHandler.AddReplica(s14.ReplicaList, 16, "...were amazingly smart people, many of the achievements of world science and culture appeared thanks to their talent.");
        DataHandler.ScenarioReplica s14_r4 = DataHandler.AddReplica(s14.ReplicaList, 17, "...successfully led tens of thousands of people, some of them have written their names in history.");
      
        DataHandler.AddNewCondition(s14_r2.SuccessOutcome, 1, 1, "Strength", 1);
        DataHandler.AddNewCondition(s14_r2.SuccessOutcome, 3, 1, "Stamina", 1);
        DataHandler.AddNewCondition(s14_r3.SuccessOutcome, 4, 1, "Intellect", 2);
        DataHandler.AddNewCondition(s14_r4.SuccessOutcome, 5, 1, "Charisma", 2);

        // STAGE 15. QUIZ 2.

        DataHandler.ScenarioStage s15 = DataHandler.AddNewStage(IntroScenario.Stages, 15, 0, "Quiz", 1);
        DataHandler.ScenarioReplica s15_r1 = DataHandler.AddReplica(s15.ReplicaList, 18, "What is Love for you?");
        DataHandler.ScenarioReplica s15_r2 = DataHandler.AddReplica(s15.ReplicaList, 19, "The most beautiful and unique feeling on earth. He who did not love did not live.");
        DataHandler.ScenarioReplica s15_r3 = DataHandler.AddReplica(s15.ReplicaList, 20, "A complex chemical cocktail of hormones, pheromones and neural impulses in the brain.");
        DataHandler.ScenarioReplica s15_r4 = DataHandler.AddReplica(s15.ReplicaList, 21, "The primary instinct responsible for the formation of healthy offspring.");

        DataHandler.AddNewCondition(s15_r2.SuccessOutcome, 5, 1, "Charisma", 2);
        DataHandler.AddNewCondition(s15_r3.SuccessOutcome, 4, 1, "Intellect", 3);
        DataHandler.AddNewCondition(s15_r4.SuccessOutcome, 3, 1, "Stamina", 2);

        // STAGE 16. QUIZ 3.

        DataHandler.ScenarioStage s16 = DataHandler.AddNewStage(IntroScenario.Stages, 16, 0, "Quiz", 1);
        DataHandler.ScenarioReplica s16_r1 = DataHandler.AddReplica(s16.ReplicaList, 22, "You are attending a masquerade ball at the invitation of your old friend. " +
            "Everything around is replete with beautiful dresses, exquisite jewelry and elaborate masks. There is a spirit of mystery, passion and vice in the air... But how do you feel in this environment?");
        DataHandler.ScenarioReplica s16_r2 = DataHandler.AddReplica(s16.ReplicaList, 23, "Like a fish in water. You literally dissolve into the exuberant magic of what is happening.");
        DataHandler.ScenarioReplica s16_r3 = DataHandler.AddReplica(s16.ReplicaList, 24, "Are you bored. You want to take off your stupid mask and go to your cozy laboratory.");
        DataHandler.ScenarioReplica s16_r4 = DataHandler.AddReplica(s16.ReplicaList, 25, "You feel uncomfortable among these people, but you overpower yourself to find valuable acquaintances.");

        DataHandler.AddNewCondition(s16_r2.SuccessOutcome, 2, 1, "Agility", 3);
        DataHandler.AddNewCondition(s16_r3.SuccessOutcome, 1, 1, "Strength", 2);
        DataHandler.AddNewCondition(s16_r4.SuccessOutcome, 4, 1, "Intellect", 2);
                
        // STAGE 17. RESOLVE QUIZ.

        DataHandler.ScenarioStage s17 = DataHandler.AddNewStage(IntroScenario.Stages, 17, 0, "Resolve_Quiz", 1);

        // STAGE 17. WE ARE DONE.

        DataHandler.ScenarioStage s18 = DataHandler.AddNewStage(IntroScenario.Stages, 18, 3);
        DataHandler.AddReplica(s18.ReplicaList, 26, "Very nice! We are done for now! Bye!", "Action");

        // STAGE 18. WE ARE DONE.

        DataHandler.ScenarioStage s19 = DataHandler.AddNewStage(IntroScenario.Stages, 19, 0);
        DataHandler.AddReplica(s19.ReplicaList, 27, "Thank you all!", "Shout");

        // STAGE 19. FINISH.

        DataHandler.ScenarioStage s20 = DataHandler.AddNewStage(IntroScenario.Stages, 20, 0, "Finish", 1);

        // START SCENARIO

        ScenarioHandler.instance.StartScenario(IntroScenario);

        DataHandler.instance.sData.scenarios.Add(IntroScenario);
    }

}
