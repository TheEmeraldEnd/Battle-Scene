using UnityEngine;

[CreateAssetMenu(fileName = "New Defensive Option", menuName = "Options/Defensive Option")]
public class DefensiveOption : Option
{
    //Set the variables
    [SerializeField] [Range(0f, 1f)] 
    public float blockPercentage;                               //Percentage of attack blocked
    [SerializeField] private string _statusName = "Block";      //Status name

    /// <summary>
    /// Gets the block percentage
    /// </summary>
    /// <returns></returns>
    ///     Returns block percentage
    public float GetBlockPercentage()
    {
        return blockPercentage;
    }

    /// <summary>
    /// Makes the status effect of the option
    /// </summary>
    /// <returns></returns>
    ///     Returns the made stat from the option with the effects
    public override Status MakeStatusEffect()
    {
        //Instantiate the status effect
        Status blockStatus = new Status();

        //Set the status name
        blockStatus.statusName = _statusName;

        //If the status is instant, set status time to the lowest value
        if (_isInstant == true)
            statusTime = float.MinValue;

        //Does the status effect the self
        blockStatus.effectsSelf = effectsSelf;

        //Is the effect instant
        blockStatus.isImmediate = _isInstant;

        //Set the remaining time to status time
        blockStatus.SetTime(statusTime);

        //Set the effects
        float blockPercent = blockPercentage * (1 + ghostMult);

        //Keep block percentage in range
        if (blockPercent < 0f)
        {
            blockPercent = 0f;
        } else if (blockPercent > 1f)
        {
            blockPercent = 1f;
        }

        //Set the stat block percentage
        blockStatus.blockPercentageChange = blockPercent;

        //Return the status effect
        return blockStatus;
    }
}
