using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectsText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    
    [SerializeField] private string _entityTag;
    [SerializeField] private EntityData _entityData;

    private void Awake()
    {
        _text = this.GetComponentInChildren<TMP_Text>();

        _entityData = GameObject.FindGameObjectWithTag(_entityTag).GetComponent<EntityData>();
    }

    public void UpdateEffectsListText(List<Status> currentEffects)
    {
        Status[] currentStats = currentEffects.ToArray();

        string textString = $"{_entityTag} stats:\n";

        foreach (Status stat in currentEffects)
        {
            if (stat.isImmediate == false)
            {
                //Add in stat name
                textString += $"[{stat.statusName}]";

                //Add in effects
                if (stat.blockPercentageChange != 0f)
                {
                    textString += $" block {stat.blockPercentageChange:f}";
                }
                else if(stat.healthChange < 0f)
                {
                    textString += $" damage health {stat.healthChange:f}";
                }
                else if(stat.healthChange > 0f)
                {
                    textString += $" heal health {stat.healthChange:f}";
                }

                //Remaining time
                textString += $" for {stat.GetRemainingTime():f2} seconds\n";
            }
        }

        //Text
        _text.text = textString;
    }

    private void Update()
    {
        UpdateEffectsListText(_entityData.activeEffects);
    }

}
