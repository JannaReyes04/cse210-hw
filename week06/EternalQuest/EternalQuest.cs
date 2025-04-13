using System;
using System.Collections.Generic;

public class EternalQuest
{
    private List<Goal> goals;
    private int score;

    public EternalQuest()
    {
        goals = new List<Goal>();
        score = 0;
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordGoal(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < goals.Count)
        {
            score += goals[goalIndex].RecordEvent();
        }
    }

    public void DisplayGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i]}");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Total Score: {score}");
    }

    public void SaveProgress(string filename = "progress.txt")
    {
        using (var writer = new System.IO.StreamWriter(filename))
        {
            writer.WriteLine($"Score: {score}");
            foreach (var goal in goals)
            {
                writer.WriteLine(goal);
            }
        }
    }

    public void LoadProgress(string filename = "progress.txt")
    {
        // Implementation for loading goals from file
    }
}
