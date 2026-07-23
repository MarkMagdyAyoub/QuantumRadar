using QuantumRadar.Models.Interfaces;

namespace QuantumRadar.Models.Rules;

public class SpeedLimitRule : IRule
{
  private readonly int _speedLimit;
  private readonly VehicleType _vehicleType;

  public SpeedLimitRule(int speedLimit, VehicleType vehicleType)
  {
    _speedLimit = speedLimit;
    _vehicleType = vehicleType;
  }

  public bool AppliedTo(VehicleType vehicle) => vehicle == _vehicleType;


  public List<Violation> CheckViolations(VehicleDetection detection)
  {
    var violations = new List<Violation>();

    if (detection.Speed > _speedLimit)
    {
      violations.Add(new Violation
      {
        Description = $"Speeding: {detection.Speed} km/h (limit: {_speedLimit} km/h)",
        FineAmount = CalculateFine(detection)
      });
    }

    return violations;
  }

  // i assumed that the fine is calculated based on the excess speed over the limit, with a fixed rate of 10 currency units per km/h over the limit.
  public decimal CalculateFine(VehicleDetection detection)
  {
    int excessSpeed = detection.Speed - _speedLimit;
    return excessSpeed * 10.00m;
  }
}