using System.Text;
using QuantumRadar.Models.Interfaces;

namespace QuantumRadar.Models;
// the radar system is a collection of rules that can be applied to vehicle detections.
public class RadarSystem
{
  private  readonly List<IRule> _rules = [];
  private readonly IDictionary<string , List<Fine>> _fineByPlate = new Dictionary<string, List<Fine>>();
  private readonly IDictionary<string , int> _violationCountByPlate = new Dictionary<string, int>();

  public void AddRule(IRule rule)
  {
    _rules.Add(rule);
  }

  public Fine? ProcessDetection(VehicleDetection detection)
  {
    var violations = new List<Violation>();

    foreach (var rule in _rules)
    {
      if (rule.AppliedTo(detection.Type))
      {
        violations.AddRange(rule.CheckViolations(detection));
      }
    }

    decimal totalFine = violations.Sum(v => v.FineAmount);

    if (totalFine > 0)
    {
      var fine = new Fine(detection.PlateNumber, violations);

      if (!_fineByPlate.ContainsKey(detection.PlateNumber))
      {
        _fineByPlate[detection.PlateNumber] = new List<Fine>();
      }
      _fineByPlate[detection.PlateNumber].Add(fine);

      if (!_violationCountByPlate.ContainsKey(detection.PlateNumber))
      {
        _violationCountByPlate[detection.PlateNumber] = 0;
      }
      _violationCountByPlate[detection.PlateNumber]++;
      
      return fine;
    }

    return null;
  }

  public int GetTotalViolationCount() => _violationCountByPlate.Values.Sum();

  public StringBuilder GetStatistics()
  {
    var stats = new StringBuilder();
    stats.AppendLine("=== Violation Statistics ===");
    foreach (var entry in _violationCountByPlate)
    {
      stats.AppendLine($"Plate: {entry.Key}, Violations: {entry.Value}");
    }
    stats.AppendLine($"Total Violations: {GetTotalViolationCount()}");

    int totalFines = _fineByPlate.Values.Sum(fines => fines.Count);
    stats.AppendLine($"Total Fines Issued: {totalFines}");
    decimal totalFineAmount = _fineByPlate.Values.Sum(fines => fines.Sum(fine => fine.Violations.Sum(v => v.FineAmount)));
    stats.AppendLine($"Total Fine Amount: {totalFineAmount}");
    return stats;
  }
}