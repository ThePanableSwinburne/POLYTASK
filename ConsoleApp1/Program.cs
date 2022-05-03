using System;
using System.Runtime.Serialization.Formatters;

namespace ConsoleApp1
{
    public abstract class Engine
    {
        public int horsePower { get; set; }
        public int energyStoreMaxSize { get; set; }
        public int currentEnergyStore { get; set; }

        public Engine()
        {
            this.horsePower = horsePower;
        }

        public Engine(int horsePower, int energyStoreMaxSize)
        {
            this.horsePower = horsePower;
            this.energyStoreMaxSize = energyStoreMaxSize;
            currentEnergyStore = this.energyStoreMaxSize;
        }

        public abstract void Rev();
        public abstract void Rev(int amount);
        public abstract void Refuel();
        public abstract void Refuel(int amount);
    }

    public class InternalCombustionEngine : Engine
    {
        public string fuelType { get; set; }
        public InternalCombustionEngine()
        {
        }

        public InternalCombustionEngine(string fuelType, int horsePower, int energyStoreMaxSize) : base(horsePower, energyStoreMaxSize)
        {
            this.fuelType = fuelType;
        }

        public override void Rev()
        {
            if (currentEnergyStore == 0)
            {
                Console.WriteLine("OUT OF FUEL");
                return;
            }
            Console.WriteLine("VROOOOMMMM!");
        }

        public override void Rev(int amount)
        {

            for (int i = 0; i < amount; i++)
            {
                if (currentEnergyStore == 0)
                {
                    Console.WriteLine("OUT OF FUEL");
                    return;
                }
                if (i > 10 - 1)
                {
                    Console.WriteLine("EXPLOOOODDDE!");
                    currentEnergyStore--;
                    return;
                }
                Console.WriteLine("VROOOOMMMM!");
                currentEnergyStore--;
            }
           
        }

        public override void Refuel()
        {
            currentEnergyStore = energyStoreMaxSize;
            Console.WriteLine("REFUELED TO FULL");
        }

        public override void Refuel(int amount)
        {
            currentEnergyStore += amount;
            currentEnergyStore = Math.Clamp(currentEnergyStore, 0, energyStoreMaxSize);
            Console.WriteLine($"REFUELED TO {currentEnergyStore} LITRES");
        }
    }

    public class ElectricEngine : Engine
    {
        public string batteryType { get; set; }
        public ElectricEngine()
        {
        }

        public ElectricEngine(string batteryType, int horsePower, int energyStoreMaxSize) : base(horsePower, energyStoreMaxSize)
        {
            this.batteryType = batteryType;
        }

        public override void Rev()
        {
            if (currentEnergyStore == 0)
            {
                Console.WriteLine("FLAT BATTERY");
                return;
            }
            Console.WriteLine("SWWIIIISSSSHHH!");
        }

        public override void Rev(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (currentEnergyStore == 0)
                {
                    Console.WriteLine("FLAT BATTERY");
                    return;
                }
                Console.WriteLine("SWWIIIISSSSHHH!");
                currentEnergyStore--;
            }
        }

        public override void Refuel()
        {
            currentEnergyStore = energyStoreMaxSize;
            Console.WriteLine("RECHARGED TO FULL");
        }

        public override void Refuel(int amount)
        {
            currentEnergyStore += amount;
            currentEnergyStore = Math.Clamp(currentEnergyStore, 0, energyStoreMaxSize);
            Console.WriteLine($"RECHARGED {currentEnergyStore} AMP HOURS");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Engine ICE = new InternalCombustionEngine("OIL",100, 100);
            Engine EE = new ElectricEngine("BATTERIES", 100, 100);

            ICE.Rev(20);
            EE.Rev(200);
            EE.Rev(1);
            EE.Refuel(2000);
            EE.Rev(1000);
            EE.Refuel();

        }
    }
}
