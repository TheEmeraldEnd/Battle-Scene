using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : MonoBehaviour
{
    #region in game variables
    [Header("Hp related variables")]
    [SerializeField]
    [Tooltip("The health of the entity")]
    [Range(0f, 1f)] private float _hp;      //The entity's hp

    [SerializeField]
    [Tooltip("The ghost health and stamina/magic for attacks")]
    [Range(0f, 1f)] private float _gp;      //The entity's gp or "magic"

    [Header("Gost related data")]
    [Tooltip("Bool telling the program if the entity is a ghost or not")]
    [SerializeField] private bool _isGhost; //Check if entity is a ghost to apply stat
    private bool _ghostPriorUpdate;         //Makes ghost assign only once when changing

    [Tooltip("The attack multiplier of the ghost mode")]
    [SerializeField] 
    private float _ghostAttMultiplier;      //The stat multiplier

    [SerializeField]
    [Tooltip("The list of options used")]
    public Option[] options;                //The list of possible options the entity can choose

    [Tooltip("The active effects")]
    public List<Status> activeEffects = 
        new List<Status>();                 //List of active effects

    private readonly float _min = 0f;       //Min number for hp and gp
    private readonly float _max = 1f;       //Max number for hp and gp

    private SpriteRenderer _entitySprite;   //The sprite of the entity

    private Color _ghostColor 
        = new Color(1f, 1f, 1f, 0.5f);      //The transparency of the ghost
    private Color _normalColor = 
        new Color(1f, 1f, 1f, 1f);          //Normal colr
    #endregion

    #region properties
    public float Hp { get { return _hp; } }
    public float Gp { get { return _gp; } }
    public bool IsGhost { get { return _isGhost; } }
    public float AttMultiplier { get { return _ghostAttMultiplier; } }
    #endregion

    #region change methods

    /// <summary>
    /// Change the hp outside the class
    /// </summary>
    /// <param name="hpChange"></param>
    ///     What is the change of the health (Can be health 
    public void ChangeHp(float hpChange)
    {
        //Hp change
        float hpFinal = _hp + hpChange;
        _hp = hpFinal;

        _hp = SetRange(_hp, _min, _max);

        //Events if the hpFinal <= 0
        if (hpFinal <= 0f)
        {
            //Make the character a ghost
            _isGhost = true;

            //Set gp change to the remaining health change
            float gpChange = hpFinal;

            //Set the damage to the gp
            ChangeGp(gpChange);
        }
        else if (hpFinal > 0f)
        {
            _isGhost = false;
        }
    }

    /// <summary>
    /// Change the gp directly
    /// </summary>
    /// <param name="gpChange"></param>
    public void ChangeGp(float gpChange)
    {
        //Change the ghost point
        _gp += gpChange;
        _gp = SetRange(_gp, _min, _max);
    }

    #endregion

    #region miscMethods
    /// <summary>
    /// Checks if a stat is in a tolerance range or not (tolerance range is 0 by default)
    /// </summary>
    /// <param name="stat"></param>
    ///     The stat being tested
    /// <param name="toleranceRange"></param>
    ///     The tolerance of the check
    /// <returns></returns>
    ///     Returns true if the stat is within tolerance range of zero (and can be
    ///         considered zero
    public bool CheckZero(float stat, float toleranceRange = 0f)
    {
        //Check if the stat if it is zero
        return (-toleranceRange <= stat && stat <= toleranceRange);
    }

    /// <summary>
    /// Checks if the value is between two numbers. Sets to min if too small,
    ///     Sets to max if too big
    /// </summary>
    /// <param name="value"></param>
    ///     The value being checked
    /// <param name="min"></param>
    ///     The minimum the value can be
    /// <param name="max"></param>
    ///     The maximum the value can be
    /// <returns></returns>
    ///     The value within the range
    private float SetRange(float value, float min, float max)
    {
        //Set value to min if less than min
        if (value < min)
        {
            return min;
        }

        //Set value to max if over max
        else if (value > max)
        {
            return max;
        }

        //Return the regular value
        else
        {
            return value;
        }
    }
    #endregion


    #region Ghost setup

    /// <summary>
    /// Set up the ghost sprite
    /// </summary>
    private void Start()
    {
        //Get the entity sprite
        _entitySprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Happens every update
    /// </summary>
    private void Update()
    {
        //Checks if...
        if (
            _isGhost == true                //ghost is already a ghost
            && _ghostPriorUpdate == false   //The entity was not a ghost last update
            )  
        {
            //Set the ghost to be transparent
            _entitySprite.color = _ghostColor;

            //Modify the boost of each stat
            foreach (Option option in options)
            {
                option.ghostMult = _ghostAttMultiplier;
            }

            //Set the prior ghost is true
            _ghostPriorUpdate = true;
        }

        //Check if else
        else if (       
            _isGhost == false               //Entity is not a ghost
            && _ghostPriorUpdate == true)   //If ghost was a ghost last update
        {
            //Set transparency to none
            _entitySprite.color = _normalColor;

            //Set bonus to none
            foreach (Option option in options)
            {
                option.ghostMult = 0f;
            }

            //Set ghost prior update to false
            _ghostPriorUpdate = false;
        }
    }

    #endregion
}
