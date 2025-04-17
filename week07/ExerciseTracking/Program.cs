using System;
using System.Collections.Generic;

abstract class Activity
{
    private string _date;
    private int _durationMinutes;

    public Activity(string date, int durationMinutes)
    {
        _date = date;
        _durationMinutes = durationMinutes;
    }

    public string Date => _date;
    public int DurationMinutes => _durationMinutes;

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{Date} {this.GetType().Name} ({DurationMinutes} min): " +
               $"Distance: {GetDistance():0.0} km, " +
               $"Speed: {GetSpeed():0.0} kph, " +
               $"Pace: {GetPace():0.00} min per km";
    }
}

class Running : Activity
{
    private double _distanceKm;

    public Running(string date, int durationMinutes, double distanceKm)
        : base(date, durationMinutes)
    {
        _distanceKm = distanceKm;
    }

    public override double GetDistance() => _distanceKm;
    public override double GetSpeed() => (_distanceKm / DurationMinutes) * 60;
    public override double GetPace() => DurationMinutes / _distanceKm;
}

class Cycling : Activity
{
    private double _speedKph;

    public Cycling(string date, int durationMinutes, double speedKph)
        : base(date, durationMinutes)
    {
        _speedKph = speedKph;
    }

    public override double GetDistance() => (_speedKph * DurationMinutes) / 60;
    public override double GetSpeed() => _speedKph;
    public override double GetPace() => 60 / _speedKph;
}

class Swimming : Activity
{
    private int _laps;

    public Swimming(string date, int durationMinutes, int laps)
        : base(date, durationMinutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => (_laps * 50) / 1000.0;
    public override double GetSpeed() => (GetDistance() / DurationMinutes) * 60;
    public override double GetPace() => DurationMinutes / GetDistance();
}

class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running("03 Nov 2022", 30, 4.8),
            new Cycling("03 Nov 2022", 45, 20.0),
            new Swimming("03 Nov 2022", 40, 30)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
