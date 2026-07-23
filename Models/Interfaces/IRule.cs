// i want to make rule extendable so i can add more rules in the future
// so i will create an contract for all rules , when you want to create a new rule you will implement this contract
// any role should defined as 
//  1. what type of vehicle it applies to
//  2. what is the logic to check if the vehicle is violating the rule
//  3. how to calculate the fine for violating the rule
namespace QuantumRadar.Models.Interfaces;
public interface IRule
{
  bool AppliedTo(VehicleType vehicle);
  List<Violation> CheckViolations(VehicleDetection detection);
  decimal CalculateFine(VehicleDetection detection);
}