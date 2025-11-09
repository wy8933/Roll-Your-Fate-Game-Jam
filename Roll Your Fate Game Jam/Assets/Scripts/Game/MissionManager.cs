using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    public int resourceNeeded;
    public int currentResource;

    public int fuelNeeded;
    public int currentFuel;

    public int antennaNeeded;
    public int antennaFixed;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool IsAllMissionComplete() 
    {
        if (currentResource >= resourceNeeded)
        {
            if (currentFuel >= fuelNeeded) 
            {
                if (antennaFixed >= antennaNeeded) 
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void AddResource() 
    {
        currentResource++;
    }

    public void AddFuel() 
    {
        currentFuel++;
    }

    public void FixAntenna() 
    {
        antennaFixed++;
    }
}
