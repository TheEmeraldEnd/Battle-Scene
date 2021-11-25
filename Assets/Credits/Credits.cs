using UnityEngine;

/// <summary>
/// Store a credits
/// </summary>
[CreateAssetMenu(menuName = "Credits", fileName = "New Credits Thing")]
public class Credits : ScriptableObject
{
    //Instantiate the variables
    [SerializeField] private string _creditsType;       //A catagory of the credit asset
    [SerializeField] private string _creditFor;         //A small synopsis of the asset
    [SerializeField] private string _webiteFound;       //Where the asset can be found for purchase
    [SerializeField] private string _author;            //Who made the asset

    //Allows the variables to be gotten publicly
    public string CreditsType { get { return _creditsType; } }
    public string CreditFor { get { return _creditFor; } }
    public string Website { get { return _webiteFound; } }
    public string Author { get { return _author; } }
}
