using System;

public abstract class Goal
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Points { get; protected set; }
    public bool Completed { get; protected set; }

    protected Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        Completed = false;
    }

    public abstract int RecordEvent();

    public override string ToString()
    {
        return $"[{(Completed ? "X" : " ")}] {Name} - {Description}";
    }
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) 
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        if (!Completed)
        {
            Completed = true;
            return Points;
        }
        return 0;
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) 
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        return Points;
    }
}

public class ChecklistGoal : Goal
{
    public int TargetCount { get; private set; }
    public int CurrentCount { get; private set; }
    public int BonusPoints { get; private set; }

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        : base(name, description, points)
    {
        TargetCount = targetCount;
        CurrentCount = 0;
        BonusPoints = bonusPoints;
    }

    public override int RecordEvent()
    {
        if (!Completed)
        {
            CurrentCount++;
            if (CurrentCount >= TargetCount)
            {
                Completed = true;
                return Points + BonusPoints;
            }
            return Points;
        }
        return 0;
    }

    public override string ToString()
    {
        return $"[{(Completed ? "X" : " ")}] {Name} - {Description} (Completed {CurrentCount}/{TargetCount})";
    }
}
