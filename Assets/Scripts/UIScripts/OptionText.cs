using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionText : MonoBehaviour
{
    //Instantiate variables
    [SerializeField] private string _playerTag;         //Entity tags that the choices come from
    [SerializeField] private Option[] _entityChoices;   //Entity choices
    [SerializeField] private float _coolDown;           //Choice Cooldown

    [SerializeField] private TMP_Text _text;            //The text of the object
    [SerializeField] private float _toleranceRange;     //Tolerance range

    #region instantiate variables
    //Set up the scene
    public void Awake()
    {
        GameObject entity = GameObject.FindGameObjectWithTag(_playerTag);

        _entityChoices = entity.GetComponent<EntityData>().options;
        _text = this.GetComponentInChildren<TMP_Text>();
    }
    #endregion

    #region update methods for event handler
    //Update Cooldown
    public void UpdateCooldown(float updatedCooldown)
    {
        //Update Cooldown
        _coolDown = updatedCooldown;
    }

    //Update the text if cooldown is not in cooldown range
    public void UpdateText(float toleranceRange)
    {
        //Empty string
        string tempText = "";


        //Set text to cooldown if cooldown is not within cooldown range
        if (_coolDown > toleranceRange)
            tempText = $"Cooldown = [{_coolDown:f2}]";

        //Set text to choice if cooldown is less than tolerance range
        else if(_coolDown <= toleranceRange)
        {
            for(int i = 0; i < _entityChoices.Length; i++)
            {
                Option option = _entityChoices[i];

                string optionName = option.name;
                float optionGPCost = option.GPCost;
                float optionCoolDownTime = option.CoolDownTime;

                tempText += $"[{i + 1}]{optionName}: GP Cost:{optionGPCost:f2}, " +
                    $"cooldown = {optionCoolDownTime:f2} sec";

                //Add in effect time
                if (option._isInstant == false)
                {
                    float optionTime = option.statusTime;

                    tempText += $", duration = {optionTime} seconds";
                }

                //Sets in offensive option details
                if (option.isOffensive == true)
                {
                    OffensiveOption offensiveOp = (OffensiveOption)_entityChoices[i];

                    float hpChange = offensiveOp.GetDamageAmmount();

                    tempText += $", {hpChange:f2} base damage";
                }

                //Set the defensive option variables
                else if (option.isDefensive == true)
                {
                    DefensiveOption defOp = (DefensiveOption)option;

                    float blockAmmount = defOp.GetBlockPercentage();

                    tempText += $", {blockAmmount:f2} base block";
                }

                //Set the heal option stuff
                else if (option.isHeal == true)
                {
                    HealOption healOp = (HealOption)option;

                    float healAmmount = healOp.GetAmmountHealed();

                    tempText += $", {healAmmount:f2} base heal";
                }

                tempText += "\n";
            }
        }

        //Set the text
        _text.text = tempText;
    }

    private bool CheckIfInToleranceRange(float num, float toleranceRange)
    {
        if (num == null)
            return false;

        return -toleranceRange < num && num < toleranceRange;
    }



    #endregion
}
