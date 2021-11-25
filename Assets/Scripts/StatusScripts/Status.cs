using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{
    #region instantiated variables
    //Bools
    [SerializeField] public bool effectsSelf = false;       //If variable effects self
    [SerializeField] public bool isImmediate = false;       //If variable is immediate (next frame)

    [SerializeField] public string statusName = "";         //The stat name
    [SerializeField] public bool isNull = false;            //If the stat has an effect or not (deletion)

    //Time related
    [SerializeField] private float _remainingTime = 0f;     //The remaining time of the stat

    //Effects
    [SerializeField] public float healthChange = 0f;        //The health chagne of the stat
    [SerializeField] public float gpChange = 0f;            //The gp change of the stat
    [SerializeField][Range(0f, 1f)] 
    public float blockPercentageChange = 0f;                //Block ercentage

    //IDK
    public float maxTime;
    #endregion

    /// <summary>
    /// Subtract the change in time from the remaining time in the status
    /// </summary>
    /// <param name="deltaTime"></param>
    ///     The time change between updates in seconds
    public void SubtractTime(float deltaTime)
    {
        //Subtract the time
        _remainingTime -= deltaTime;

        //Sets the tag for ready for deletion
        if (IsRemainingTimeNegative())
        {
            isNull = true;
        }
    }

    /// <summary>
    /// Checks to see if the time remaining is less than 0 seconds
    /// </summary>
    /// <returns></returns>
    ///     Returns true if time remaining is less than or equal to zero
    public bool IsRemainingTimeNegative()
    {
        //If remaining time 
        return _remainingTime <= 0f;
    }

    /// <summary>
    /// Updates the time
    /// </summary>
    /// <param name="time"></param>
    ///     The new time in seconds
    public void SetTime(float time)
    {
        _remainingTime = time;
        maxTime = time;
    }

    /// <summary>
    /// A public float that gets the remaining time
    /// </summary>
    /// <returns></returns>
    ///     The remaining time in seconds
    public float GetRemainingTime()
    {
        return _remainingTime;
    }
}
