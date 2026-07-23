namespace QuantumRadar.Models;

public class VehicleDetection
{
  public string PlateNumber { get; set; } = string.Empty;
  public DateTime Date { get; set; }
  public VehicleType Type { get; set; }
  public int Speed { get; set; }
  public bool SeatBeltFastened { get; set; }

  public VehicleDetection(string plateNumber, DateTime date, VehicleType type, int speed, bool seatBeltFastened)
  {
    PlateNumber = plateNumber;
    Date = date;
    Type = type;
    Speed = speed;
    SeatBeltFastened = seatBeltFastened;
  }
}