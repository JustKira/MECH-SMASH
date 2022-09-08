using UnityEngine;
public class DamageCalculator : MonoBehaviour
{
    public int weightWeight=1;
    public int speedWeight=1;
    public int accelerationWeight=1;
    public VariableStats Player;
    public VariableStats Other;
    public static DamageCalculator instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameObject = new GameObject("DamageCalculator").AddComponent<DamageCalculator>().gameObject;
                _instance = gameObject.GetComponent<DamageCalculator>();
            }
            
            return _instance;
        }
        private set
        {
            if (_instance != value || _instance != null)
            {
                Destroy(value);
                return;
            }
            _instance = value;
        }
    }
    private static DamageCalculator _instance;
    
    private void Start()
    {
        DontDestroyOnLoad(instance);
    }
    /**
     * returns an array with 2 elements the first being activationDamage and the second attachmentDamage
     */
    public int CalculateDamage(VariableStats Player, VariableStats Other)
    {
        int x = Mathf.CeilToInt((Other.currentAcceleration + Other.currentSpeed)!=0? (Other.currentAcceleration + Other.currentSpeed):1 
            / (Player.currentSpeed + Player.currentSpeed)!=0? (Player.currentSpeed + Player.currentSpeed):1 
            * (Other.currentWeight +1)
            /( Player.currentWeight+1));
        return x;
    }   
}