using UnityEngine;

/// <summary>
/// Find the player option
/// </summary>
public class PlayerOptionInput : MonoBehaviour
{
    #region instantiated variables
    //Player Data
    private Option[] _playerOptions;                    //Available player options

    private EntityData _playerData;                     //The player data
    private Animator _playerAnimations;                 //The player animations

    //Cooldown
    [SerializeField] public float inputCooldown;        //The input cooldown in seconds

    #endregion

    #region find variables in scene

    /// <summary>
    /// Set up everything before the first frame
    /// </summary>
    private void Awake()
    {
        //Instantiate the player variables
        _playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityData>();
        _playerOptions = _playerData.options;

        //Instantiate player animations
        _playerAnimations = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    #endregion

    #region applyable methods

    /// <summary>
    /// Find the player choice
    /// </summary>
    /// <param name="deltaTime"></param>
    ///     The time to subtract from the player's input time cooldown
    /// <returns></returns>
    ///     Returns the stat version of the option
    public Status FindOptionChoice(float deltaTime)
    {
        //Update the cooldown with the time
        UpdateInputCooldown(-deltaTime);

        //Loop through options and buttons pressed
        for (int i = 0; i < _playerOptions.Length; i++)
        {
            //Check if...
            #region option set
            if (
                Input.GetKeyDown($"{i + 1}")        //Player pressed a valid button
                && inputCooldown <= 0f              //input is valid (less than or = to zero)
                && (_playerData.Gp - 
                _playerOptions[i].GPCost) > 0       //Check if cost will be less than 0
                    )
            {
                //Subtract the Gp cost in the player options
                _playerData.ChangeGp(-_playerOptions[i].GPCost);

                //Update cooldown to option cost
                UpdateInputCooldown(_playerOptions[i].CoolDownTime);

                //Set up the stat that will play
                Status playerStat = _playerOptions[i].MakeStatusEffect();

                //Play player cast animation
                _playerAnimations.SetTrigger("CastTrigger");

                //return the stat effect
                return playerStat;
            }
            #endregion
        }

        //Return null if there is no option choice
        return new Status();
    }

    /// <summary>
    /// Update the cooldown
    /// </summary>
    /// <param name="timeChange"></param>
    ///     The time in seconds 
    private void UpdateInputCooldown(float timeChange)
    {
        //Set the input cooldown to 0 if less than 0
        if (inputCooldown < 0f)
        {
            inputCooldown = 0f;
        }

        //change the input cooldown
        inputCooldown += timeChange;

        //A double min check in case input cooldown afterwords result in less than 0
        if (inputCooldown < 0f)
        {
            inputCooldown = 0f;
        }
    }
    #endregion
}
