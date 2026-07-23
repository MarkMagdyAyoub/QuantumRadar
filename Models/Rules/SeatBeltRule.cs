using QuantumRadar.Models.Interfaces;

namespace QuantumRadar.Models.Rules;

public class SeatBeltRule : IRule
{
  public VehicleType VehicleType { get; set; }

  public SeatBeltRule(VehicleType vehicleType)
  {
    VehicleType = vehicleType;
  }

  public bool AppliedTo(VehicleType vehicle) => vehicle == VehicleType;


  public List<Violation> CheckViolations(VehicleDetection detection)
  {
    var violations = new List<Violation>();

    if (!detection.SeatBeltFastened)
    {
      violations.Add(new Violation
      {
        Description = "Seat belt not fastened",
        FineAmount = CalculateFine(detection)
      });
    }

    return violations;
  }

  // i assumed that the fine for not wearing a seat belt is a fixed amount of 50 currency units.
  public decimal CalculateFine(VehicleDetection detection)
  {
    return 50.00m;
  }
}