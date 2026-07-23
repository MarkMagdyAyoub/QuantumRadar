using QuantumRadar.Models;
using QuantumRadar.Models.Rules;
namespace QuantumRadar;
class Program
{
    static void Main(string[] args)
    {
        var radar = new RadarSystem();
        
        radar.AddRule(new SpeedLimitRule(60, VehicleType.Truck));
        radar.AddRule(new SpeedLimitRule(80, VehicleType.PrivateCar));
        radar.AddRule(new SpeedLimitRule(70, VehicleType.Motorcycle));
        radar.AddRule(new SeatBeltRule(VehicleType.PrivateCar));
        radar.AddRule(new SeatBeltRule(VehicleType.Bus));
        radar.AddRule(new SeatBeltRule(VehicleType.Truck));
        // now i can add more rules without modifying the existing code , achieving the open/closed principle
        
        Console.WriteLine("=== Processing Observations ===\n");
        
        var detection_1 = new VehicleDetection(
            "ABC1234",
            DateTime.Now,
            VehicleType.PrivateCar,
            94,
            false
        );
        var fine1 = radar.ProcessDetection(detection_1);
        if (fine1 != null) Console.WriteLine(fine1);
        
        var detection_2 = new VehicleDetection(
            "XYZ7890",
            DateTime.Now,
            VehicleType.Truck,
            70,
            true
        );
        var fine2 = radar.ProcessDetection(detection_2);
        if (fine2 != null) Console.WriteLine(fine2);
        
        var detection_3 = new VehicleDetection(
            "BUS4567",
            DateTime.Now,
            VehicleType.Bus,
            50,
            true
        );
        var fine3 = radar.ProcessDetection(detection_3);
        if (fine3 != null) Console.WriteLine(fine3);
        
        var detection_4 = new VehicleDetection(
            "DEF5678",
            DateTime.Now,
            VehicleType.PrivateCar,
            65,
            false
        );
        var fine4 = radar.ProcessDetection(detection_4);
        if (fine4 != null) Console.WriteLine(fine4);
        

        Console.WriteLine(radar.GetStatistics());
    }
}