namespace QuantumRadar.Models;

using System.Text;

public class Fine
{
  public string PlateNumber { get; } = string.Empty;
  public List<Violation> Violations { get; } = [];
  public decimal TotalAmount => Violations.Sum(v => v.FineAmount);

  public Fine(string plateNumber, List<Violation> violations)
  {
    PlateNumber = plateNumber;
    Violations = violations;
  }
  
  public override string ToString()
  {
    var sb = new StringBuilder();
    sb.AppendLine($"Traffic fine for car {PlateNumber}");
    sb.AppendLine($"Total amount: {TotalAmount} EGP");
    sb.AppendLine("Violations:");
    
    foreach (var violation in Violations)
    {
        sb.AppendLine($"  - {violation.Description} : {violation.FineAmount} EGP");
    }
    
    return sb.ToString();
  }
}