using UnityEngine;

public class EnemyOptionInput : MonoBehaviour
{
    #region define variables
    //Enemy data variables
    [SerializeField] private EntityData _enemyData;     //The enemy data
    [SerializeField] private int _optionChoice;         //The enemy choice predefined
    [SerializeField] private Option[] _optionList;      //The potential options

    //Enemy enimations
    private Animator _enemyAnimations;                  //The enemy animations

    //cooldown time related variables
    [Tooltip("The delay of each action plus the cooldown cost")]
    [SerializeField] private float _inputDelay;         //The delay in seconds per option + 
                                                        //option cool down time in seconds

    [SerializeField] private float _coolDownTime;   //The cooldown time in seconds
    #endregion

    #region instantiate variables inside unity

    /// <summary>
    /// Set up everything before the first update
    /// </summary>
    private void Awake()
    {
        //Find the enemy assets
        _enemyData = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EntityData>();
        _enemyAnimations = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();

        //Set up the starting cooldown time
        _coolDownTime = 0f;

        //List the options
        _optionList = _enemyData.options;
    }

    #endregion

    public Status FindOptionChoice(float deltaTime)
    {
        #region cooldown related
        //Subtract the time for current cooldown
        _coolDownTime -= deltaTime;

        //Set cooldown to 0 if less than 0
        _coolDownTime = _coolDownTime < 0 ? 0: _coolDownTime ;
        #endregion

        #region option setup
        //Choose option
        _optionChoice = Random.Range(0, _optionList.Length);

        //Set option effect
        if (
            _coolDownTime <= 0f                         //Checks if he cooldown is ready
            && (_enemyData.Gp -                         
            _optionList[_optionChoice].GPCost) > 0f     //Checks if cooldown cost is ready
            )
        {
            //Set the cooldown time cost to cooldown time by option
            _coolDownTime = _optionList[_optionChoice].CoolDownTime + _inputDelay;

            //Play the enemy cast animation
            _enemyAnimations.SetTrigger("CastTrigger");

            //Set the available gp for enemy after cast
            _enemyData.ChangeGp(-1 * _optionList[_optionChoice].GPCost);

            //Return the stat version of the option
            return _optionList[_optionChoice].MakeStatusEffect();
        }
        //If no option is chosen
        else
        {
            //Return an empty status
            return new Status();
        }
        #endregion
    }
}
