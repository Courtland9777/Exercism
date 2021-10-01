using System;

public class RemoteControlCar
{
    private int _batteryPercentage = 100;
    private int _distanceDrivenInMeters;
    private string[] _sponsors = Array.Empty<string>();
    private int latestSerialNum;

    public void Drive()
    {
        if (_batteryPercentage > 0)
        {
            _batteryPercentage -= 10;
            _distanceDrivenInMeters += 2;
        }
    }

    public void SetSponsors(params string[] sponsors) => _sponsors = sponsors;

    public string DisplaySponsor(int sponsorNum) => _sponsors[sponsorNum];

    public bool GetTelemetryData(ref int serialNum,
        out int batteryPercentage, out int distanceDrivenInMeters)
    {
        if (serialNum < latestSerialNum)
        {
            serialNum = latestSerialNum;
            batteryPercentage = -1;
            distanceDrivenInMeters = -1;
            return false;
        }

        latestSerialNum = serialNum;
        batteryPercentage = _batteryPercentage;
        distanceDrivenInMeters = _distanceDrivenInMeters;
        return true;
    }

    public static RemoteControlCar Buy() => new();
}

public class TelemetryClient
{
    private readonly RemoteControlCar _car;

    public TelemetryClient(RemoteControlCar car) => _car = car;

    public string GetBatteryUsagePerMeter(int serialNum) =>
        !_car.GetTelemetryData(ref serialNum, out int batteryPercentage, out int distanceDrivenInMeters) || distanceDrivenInMeters != 0
        ? $"usage-per-meter={(100 - batteryPercentage) / distanceDrivenInMeters}"
        : "no data";
}
