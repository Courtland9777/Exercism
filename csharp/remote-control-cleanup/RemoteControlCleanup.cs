public interface ITelemetry
{
    //public Speed curreSpeed { get; }
    public void Calibrate()
    {
    }

    public bool SelfTest()
    {
        return true;
    }

    public void ShowSponsor(string sponsorName)
    {
    }

    public void SetSpeed(decimal amount, string unitsString)
    {
    }
}
public class RemoteControlCar
{
    public string CurrentSponsor { get; private set; }
    public ITelemetry Telemetry { get; internal set; }

    private static Speed currentSpeed;

    public RemoteControlCar()
    {
        Telemetry = new InnerTelemetry(this);
    }

    private class InnerTelemetry : ITelemetry
    {
        private readonly RemoteControlCar _car;
        public InnerTelemetry(RemoteControlCar remoteControlCar)
        {
            _car = remoteControlCar;
        }
        public void Calibrate()
        {

        }

        public bool SelfTest()
        {
            return true;
        }

        public void ShowSponsor(string sponsorName)
        {
            _car.SetSponsor(sponsorName);
        }

        public void SetSpeed(decimal amount, string unitsString)
        {
            SpeedUnits speedUnits = SpeedUnits.MetersPerSecond;
            if (unitsString == "cps")
            {
                speedUnits = SpeedUnits.CentimetersPerSecond;
            }

            _car.SetSpeed(new Speed(amount, speedUnits));
        }
    }

    public string GetSpeed()
    {
        return currentSpeed.ToString();
    }

    private void SetSponsor(string sponsorName)
    {
        CurrentSponsor = sponsorName;

    }

    private void SetSpeed(Speed speed)
    {
        currentSpeed = speed;
    }
}

public enum SpeedUnits
{
    MetersPerSecond,
    CentimetersPerSecond
}

public struct Speed
{
    public decimal Amount { get; }
    public SpeedUnits SpeedUnits { get; }

    public Speed(decimal amount, SpeedUnits speedUnits)
    {
        Amount = amount;
        SpeedUnits = speedUnits;
    }

    public override string ToString()
    {
        string unitsString = "meters per second";
        if (SpeedUnits == SpeedUnits.CentimetersPerSecond)
        {
            unitsString = "centimeters per second";
        }

        return Amount + " " + unitsString;
    }
}