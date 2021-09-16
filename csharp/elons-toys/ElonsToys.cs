using System;

class RemoteControlCar
{
    private int _batteryLevel;
    private int _distanceDriven;

    public RemoteControlCar()
    {
        _batteryLevel = 100;
        _distanceDriven = 0;
    }

    public static RemoteControlCar Buy() => new();

    public string DistanceDisplay() =>
        $"Driven {_distanceDriven} meters";

    public string BatteryDisplay() =>
    _batteryLevel == 0 ? "Battery empty" : $"Battery at {_batteryLevel}%";

    public void Drive()
    {
        if (_batteryLevel <= 0) return;
        _batteryLevel--;
        _distanceDriven += 20;
    }
}
