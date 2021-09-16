using System;

class RemoteControlCar
{
    private readonly int _speed;
    private readonly int _batteryDrain;
    private int _batteryLevel = 100;
    private int _distanceDriven = 0;

    public RemoteControlCar(int speed, int batteryDrain)
    {
        _speed = speed;
        _batteryDrain = batteryDrain;
    }

    public int GetBatteryDrain() => _batteryDrain;
    public int GetBatteryLevel() => _batteryLevel;
    public int GetSpeed() => _speed;

    public bool BatteryDrained() => _batteryLevel < 1;

    public int DistanceDriven() => _distanceDriven;

    public void Drive()
    {
        if (!BatteryDrained())
        {
            _distanceDriven += _speed;
            _batteryLevel -= _batteryDrain;
        }
    }

    public static RemoteControlCar Nitro() => new(50, 4);
}

class RaceTrack
{
    private readonly int _distance;

    public RaceTrack(int distance)
    {
        _distance = distance;
    }

    public bool CarCanFinish(RemoteControlCar car)
    {
        if (car.DistanceDriven() >= _distance) return true;
        var distanceToGo = _distance - car.DistanceDriven();
        var battery = car.GetBatteryLevel();
        var drive = 0;
        while (battery > 0)
        {
            battery -= car.GetBatteryDrain();
            drive += car.GetSpeed();
        }
        return distanceToGo <= drive;
    }
}
