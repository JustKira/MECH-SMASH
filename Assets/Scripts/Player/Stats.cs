using UnityEngine;
[System.Serializable]
public class StatsManager
{
    public StatsManager(int o_MaxStats, Stat o_AttachmentHealth, Stat o_ActivationHealth, Stat o_Weight, VariableStats o_VarStats)
    {
        maxStatPoints = o_MaxStats;
        availableStatPoints = maxStatPoints;
        activationHealth = new Stat(o_ActivationHealth);
        attachmentHealth = new Stat(o_AttachmentHealth);
        weight = new Stat(o_Weight);
        varStats = o_VarStats;
    }

    public int maxStatPoints;
    [SerializeField]
    private int availableStatPoints;
    public Stat activationHealth;
    public Stat attachmentHealth;
    public Stat weight;
    public VariableStats varStats;
    public bool ChangeStat(ref Stat changedStat, int changeValue)
    {
        if (changeValue > availableStatPoints)
            return false;
        changedStat.ChangeStats(changeValue);
        availableStatPoints -= changeValue;
        return true;
    }
}
[System.Serializable]
public class Stat
{
    public Stat(int o_BaseStatValue, int o_StatDelta)
    {
        baseStatValue = o_BaseStatValue;
        statDelta = o_StatDelta;
        assignedPoints = 0;
    }
    public Stat(Stat other)
    {
        this.baseStatValue = other.baseStatValue;
        this.statDelta = other.statDelta;
        assignedPoints = other.assignedPoints;
    }
    [SerializeField]
    private int baseStatValue;
    [SerializeField]
    private int statDelta;
    [SerializeField]
    public int assignedPoints;
    [SerializeField]
    public int value
    {
        get
        {
            return baseStatValue + assignedPoints * statDelta;
        }
    }
    public void ChangeStats(int StatChange)
    {
        if (assignedPoints + StatChange < 0)
            return;
        assignedPoints += StatChange;
    }
}

public enum DamageTypes
{
    slash, pierce, blunt
}


[System.Serializable]
public class ArmStatsManager : StatsManager
{
    public ArmStatsManager(int o_MaxStats, Stat o_AttachmentHealth, Stat o_ActivationHealth,
                        Stat o_Weight, Stat o_Speed, Stat o_MaxRotationAngle
                       , Stat o_Acceleration, VariableStats o_VarStats)
        : base(o_MaxStats, o_AttachmentHealth, o_ActivationHealth, o_Weight, o_VarStats)
    {
        speed = new Stat(o_Speed);
        maxRotation = new Stat(o_MaxRotationAngle);
        acceleration = new Stat(o_Acceleration);
    }

    public Stat speed;
    public Stat acceleration;
    public Stat maxRotation;
}

[System.Serializable]
public class BodyStatsManager : StatsManager
{
    public BodyStatsManager(int o_MaxStats, Stat o_AttachmentHealth, Stat o_ActivationHealth, Stat o_Weight,
            VariableStats o_VarStats) : base(o_MaxStats, o_AttachmentHealth, o_ActivationHealth, o_Weight, o_VarStats)
    {
    }
}
public class HeadStatsManager : StatsManager
{
    public HeadStatsManager(int o_MaxStats, Stat o_AttachmentHealth, Stat o_ActivationHealth,
                        Stat o_Weight, VariableStats o_VarStats) : base(o_MaxStats, o_AttachmentHealth, o_ActivationHealth, o_Weight, o_VarStats)
    {
    }
}
[System.Serializable]
public class WheelStatsManager : StatsManager
{
    public WheelStatsManager(int o_MaxStats, Stat o_AttachmentHealth, Stat o_ActivationHealth,
                        Stat o_Weight, Stat o_Acceleration, Stat o_MaxSpeed, Stat o_Grippiness, VariableStats o_VarStats)
        : base(o_MaxStats, o_AttachmentHealth, o_ActivationHealth, o_Weight, o_VarStats)
    {
        maxSpeed = new Stat(o_MaxSpeed);
        acceleration = new Stat(o_Acceleration);
        grippiness = new Stat(o_Grippiness);
    }
    public Stat maxSpeed;
    public Stat acceleration;
    public Stat grippiness;
}
