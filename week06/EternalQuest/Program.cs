using System;

public class Program
{
    public static void Main()
    {
        var program = new EternalQuest();

        while (true)
        {
            Console.WriteLine("\n1. Add Goal\n2. Record Event\n3. Display Goals\n4. Display Score\n5. Save\n6. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Goal Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Description: ");
                    string description = Console.ReadLine();
                    Console.Write("Points: ");
                    int points = int.Parse(Console.ReadLine());
                    Console.Write("Type (simple/eternal/checklist): ");
                    string type = Console.ReadLine().ToLower();

                    if (type == "simple")
                    {
                        program.AddGoal(new SimpleGoal(name, description, points));
                    }
                    else if (type == "eternal")
                    {
                        program.AddGoal(new EternalGoal(name, description, points));
                    }
                    else if (type == "checklist")
                    {
                        Console.Write("Target Count: ");
                        int target = int.Parse(Console.ReadLine());
                        Console.Write("Bonus Points: ");
                        int bonus = int.Parse(Console.ReadLine());
                        program.AddGoal(new ChecklistGoal(name, description, points, target, bonus));
                    }
                    break;

                case "2":
                    program.DisplayGoals();
                    Console.Write("Enter goal number to record: ");
                    int index = int.Parse(Console.ReadLine()) - 1;
                    program.RecordGoal(index);
                    break;

                case "3":
                    program.DisplayGoals();
                    break;

                case "4":
                    program.DisplayScore();
                    break;

                case "5":
                    program.SaveProgress();
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }
}
