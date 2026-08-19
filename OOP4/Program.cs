namespace PlayGround
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Q1
            // The practice of hiding complex implementation details and
            // showing only the essential features of an object is called encapsulation.

            //it solves the fundamental problem of managing code complexity at scale. Without 
            //abstraction, software systems would become too tangled and interdependent to build, maintain, or understand.

            // Q2
            // a)  an abstract class defines what an object is, while an interface defines what an object can do.
            // b) your primary goal is to define peripheral capabilities rather than a core identity.
            //c) No, yes

            //Q3
            int NoOfShips = 3;
            string cleanStr = default;
            decimal cleanNumber = default;

            var driver = new Driver(1, "el 7g Gom3a", "0123456789");
            var deliveryCenter = new DeliveryCenter(driver, NoOfShips);

            Console.Write("Enter Center Name: ");
            deliveryCenter.CenterName = sanitizeStrInput(cleanStr, Console.ReadLine());

            Console.WriteLine($"Driver: {deliveryCenter.Driver.FullName}");

            var array = new Dictionary<string, List<int>>
            {
                { "StandardShipment", [0] },
                { "ExpressShipment", [1] },
                { "InternationalShipment", [2] }
            };
            for (var i = 0; i < NoOfShips; i++)
            {
                fetchData(i, out var trackingCode, out var description, out var weightNo,
                    out var deliveryFee, out var address);

                switch (true)
                {
                    case var _ when array["StandardShipment"].Contains(i):
                        var shipment =
                            new StandardShipment(trackingCode, description, weightNo, deliveryFee, address);
                        deliveryCenter[i] = shipment;
                        break;

                    case var _ when array["ExpressShipment"].Contains(i):
                        var expressShipment = new ExpressShipment(10, trackingCode, description, weightNo,
                            deliveryFee, address);
                        deliveryCenter[i] = expressShipment;
                        break;

                    case var _ when array["InternationalShipment"].Contains(i):
                        var internationalShipment = new InternationalShipment(20, "Canada",
                            trackingCode, description, weightNo, deliveryFee, address);
                        deliveryCenter[i] = internationalShipment;
                        break;
                }

                Console.WriteLine("Shipment added successfully.");
            }

            Console.WriteLine("--- All Shipments");

            deliveryCenter.PrintAllShipments();

            Console.WriteLine("Enter a tracking code to search for a shipment:");
            string SearchingTrackingCode = sanitizeStrInput(cleanStr, Console.ReadLine());

            Console.WriteLine("Searching for shipment...");
            try
            {
                Shipment foundShipment = deliveryCenter[SearchingTrackingCode];
                DeliveryHelper.PrintShipmentDetails(foundShipment);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Shipment with tracking code {SearchingTrackingCode} not found.");
                return;
            }

            Console.WriteLine("Remove Shipment.....");
            deliveryCenter.RemoveShipment(SearchingTrackingCode);
            Console.WriteLine("The Remaining Shipments are:");
            deliveryCenter.PrintAllShipments();

            Console.WriteLine("Updating Weight...");
            var w = deliveryCenter[SearchingTrackingCode].Weight;
            Console.WriteLine($"Original Weight: {w} kg");
            deliveryCenter[SearchingTrackingCode].SetWeight(w, 10);
            Console.WriteLine($"Updated Weight After Packing : {deliveryCenter[SearchingTrackingCode].Weight} kg");
        }

        private static void fetchData(int i, out string trackingCode, out string description, out decimal weightNo,
            out decimal deliveryFee, out DeliveryAddress address)
        {
            string cleanStr = default;
            decimal cleanNumber = default;

            Console.WriteLine($"Enter Shipment {i + 1} Data");
            Console.Write("Tracking Code: ");
            trackingCode = sanitizeStrInput(cleanStr, Console.ReadLine());
            Console.Write("Description: ");
            description = sanitizeStrInput(cleanStr, Console.ReadLine());

            Console.Write("Weight: ");
            weightNo = sanitizeNumberInput(cleanNumber, Console.ReadLine());
            Console.Write("Delivery Fee: ");
            deliveryFee = sanitizeNumberInput(cleanNumber, Console.ReadLine());

            Console.Write("City: ");
            string City = sanitizeStrInput(cleanStr, Console.ReadLine());
            Console.Write("Street: ");
            string Street = sanitizeStrInput(cleanStr, Console.ReadLine());
            Console.Write("Building Number: ");
            string BuildingNumber = sanitizeStrInput(cleanStr, Console.ReadLine());

            address = new DeliveryAddress(City, Street, BuildingNumber);
        }

        public static decimal sanitizeNumberInput(decimal cleanNumber, string input)
        {
            while (!TrySanitizeInputs(out string _, out cleanNumber, Int: input))
            {
                input = Console.ReadLine();
            }

            return cleanNumber;
        }

        public static string sanitizeStrInput(string cleanStr, string input)
        {
            while (!TrySanitizeInputs(out cleanStr, out decimal _, Str: input))
            {
                input = Console.ReadLine();
            }

            return cleanStr;
        }

        public static bool TrySanitizeInputs(out string sanitizedStr, out decimal sanitizedInt, string Str = default,
            string Int = default)
        {
            sanitizedInt = default;
            sanitizedStr = default;
            if (Str != default)
            {
                if (!String.IsNullOrEmpty(Str) && !String.IsNullOrWhiteSpace(Str))
                {
                    sanitizedStr = Str.Trim();
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid input.");
                    return false;
                }
            }
            else if (Int != default)
            {
                if (decimal.TryParse(Int, out decimal result)) sanitizedInt = result;
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    return false;
                }
            }

            return true;
        }

        public struct DeliveryAddress
        {
            public string City { get; set; }
            string Street;
            string BuildingNumber;

            public DeliveryAddress(string city, string street, string buildingNumber)
            {
                City = city;
                Street = street;
                BuildingNumber = buildingNumber;
            }

            public string GetFullAddress()
            {
                return $"{BuildingNumber} {Street}, {City}";
            }
        }

        public abstract class Shipment
        {
            string _TrackingCode;

            string _Description;
            decimal _Weight;
            decimal _DeliveryFee;

            public string TrackingCode
            {
                private set
                {
                    if (!String.IsNullOrEmpty(value) && !String.IsNullOrEmpty(value))
                        _TrackingCode = value;
                }
                get => _TrackingCode;
            }

            public string Description
            {
                set
                {
                    if (!String.IsNullOrEmpty(value) && !String.IsNullOrEmpty(value))
                        _Description = value;
                }
                get => _Description;
            }

            public decimal Weight
            {
                set
                {
                    if (value > 0)
                        _Weight = value;
                }
                get => _Weight;
            }

            public decimal DeliveryFee
            {
                private set
                {
                    if (value > 0)
                        _DeliveryFee = value;
                }
                get => _DeliveryFee;
            }

            public DeliveryAddress Destination { get; set; }

            public abstract decimal EstimatedCost { get; }

            public Shipment(string trackingCode, string description)
            {
                TrackingCode = trackingCode;
                _Description = description;
                _TrackingCode = trackingCode;
                Description = description;
                Weight = 1;
                DeliveryFee = 50;
                Destination = new DeliveryAddress("Unknown", "Unknown", "Unknown");
            }

            protected Shipment(string trackingCode, string description, decimal weight,
                decimal deliveryFee, DeliveryAddress destination)
            {
                TrackingCode = trackingCode;
                _Description = description;
                _TrackingCode = trackingCode;
                Description = description;
                Weight = weight;
                DeliveryFee = deliveryFee;
                Destination = destination;
            }

            public decimal SetWeight(decimal weight)
            {
                _Weight = weight;
                return _Weight;
            }

            public decimal SetWeight(decimal weight, decimal extra)
            {
                _Weight = weight + extra;
                return _Weight;
            }

            public void UpdateDeliveryFee(decimal newFee)
            {
                if (newFee > 0)
                {
                    DeliveryFee = (decimal)newFee;
                }
            }

            public abstract virtual void PrintShipment();
        }

        public class StandardShipment : Shipment
        {

            public decimal EstimatedCost { get; }

            public override StandardShipment(string trackingCode, string description, decimal weight, decimal deliveryFee,
                Console.WriteLine($"Delivery Fee: ${DeliveryFee}");
                Console.WriteLine($"Destination: {Destination.GetFullAddress()}");
                Console.WriteLine($"Estimated Cost: ${EstimatedCost}");
                Console.WriteLine("======================================================");
            }
        }

        public class StandardShipment : Shipment
        {
            public StandardShipment(string trackingCode, string description, decimal weight, decimal deliveryFee,
                DeliveryAddress destination)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
            }
        }

        public sealed class CompletedShipment : Shipment
        {
            public CompletedShipment(string trackingCode, string description, decimal weight, decimal deliveryFee,
                DeliveryAddress destination)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
            }

            public override void PrintShipment()
            {
                Console.WriteLine("");
                Console.WriteLine($"Tracking Code: {TrackingCode}");
                Console.WriteLine($"Description: {Description}");
                Console.WriteLine($"Weight: {Weight} kg");
                Console.WriteLine($"Delivery Fee: ${DeliveryFee}");
                Console.WriteLine($"Estimated Cost: ${EstimatedCost}");
                Console.WriteLine("======================================================");
            }
        }

        public class ExpressShipment : Shipment
        {
            decimal _ExtraFee;

            public decimal ExtraFee
            {
                set
                {
                    if (value >= 0)
                        _ExtraFee = value;
                }
                get => _ExtraFee;
            }

            public override decimal EstimatedCost
            {
                get => DeliveryFee + (Weight * 5) + ExtraFee;
            }

            public ExpressShipment(decimal extraFee, string trackingCode, string description, decimal weight,
                decimal deliveryFee, DeliveryAddress destination)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
                ExtraFee = extraFee;
            }

            public override void PrintShipment()
            {
                Console.WriteLine("");
                Console.WriteLine($"Extra Fee: ${ExtraFee}");
                Console.WriteLine($"Tracking Code: {TrackingCode}");
                Console.WriteLine($"Description: {Description}");
                Console.WriteLine($"Weight: {Weight} kg");
                Console.WriteLine($"Delivery Fee: ${DeliveryFee}");
                Console.WriteLine($"Estimated Cost: ${EstimatedCost}");
                Console.WriteLine("======================================================");
            }
        }

        public class InternationalShipment : Shipment
        {
            private string _destinationCountry;
            private decimal _customsFee;

            public string DestinationCountry
            {
                get => _destinationCountry;
                set
                {
                    if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                        _destinationCountry = value;
                }
            }

            public decimal CustomsFee
            {
                set
                {
                    if (value >= 0)
                        _customsFee = value;
                }
                get => _customsFee;
            }

            public override decimal EstimatedCost
            {
                get => DeliveryFee + (Weight * 5) + CustomsFee;
            }

            public virtual void GenerateCustomsReport()
            {
            }

            public InternationalShipment(decimal customsFee, string DestinationCountry, string trackingCode,
                string description, decimal weight, decimal deliveryFee, DeliveryAddress destination)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
                CustomsFee = customsFee;
                DestinationCountry = DestinationCountry;
            }

            public override void PrintShipment()
            {
                Console.WriteLine("");
                Console.WriteLine($"Customs Fee: ${CustomsFee}");
                Console.WriteLine($"Destination Country: {DestinationCountry}");
                Console.WriteLine($"Tracking Code: {TrackingCode}");
                Console.WriteLine($"Description: {Description}");
                Console.WriteLine($"Weight: {Weight} kg");
                Console.WriteLine($"Delivery Fee: ${DeliveryFee}");
                Console.WriteLine($"Estimated Cost: ${EstimatedCost}");
                Console.WriteLine("======================================================");
            }
        }

        public class PriorityInternationalShipment : InternationalShipment
        {
            public PriorityInternationalShipment(decimal customsFee, string DestinationCountry, string trackingCode,
                string description, decimal weight, decimal deliveryFee, DeliveryAddress destination) : base(customsFee,
                DestinationCountry, trackingCode, description, weight, deliveryFee, destination)
            {
            }

            public sealed override void GenerateCustomsReport()
            {
                base.GenerateCustomsReport();
            }
        }

        class Driver(int driverId, string fullName, string phoneNumber)
        {
            public int DriverId = driverId;
            public string FullName = fullName;
            public string PhoneNumber = phoneNumber;
        }

        class DeliveryCenter
        {
            public Driver Driver;
            public string CenterName;

            private Shipment?[]? _shipments;

            public Shipment this[int index]
            {
                get
                {
                    if (_shipments != null && index >= 0 && index < _shipments.Length)
                        return _shipments[index];
                    else
                        return default;
                }
                set { _shipments[index] = value; }
            }

            public Shipment this[string index]
            {
                get
                {
                    foreach (var shipment in _shipments)
                    {
                        if (shipment.TrackingCode == index)
                            return shipment;
                    }

                    return default;
                }
            }

            public DeliveryCenter(Driver driver, int capacity = 20)
            {
                if (capacity > 20) throw new ArgumentException("Capacity must be less than or equal to 20");
                Driver = driver;
                _shipments = new Shipment[capacity];
            }

            public bool AddShipment(Shipment shipment)
            {
                var newPos = Array.FindIndex(_shipments, x => false);
                this[newPos] = shipment;

                // suppose be there exception thrown in int setter indexer
                return shipment.Equals(this[newPos]);
            }

            public bool RemoveShipment(string trackingCode)
            {
                var oldShipmentCount = _shipments?.Length;
                _shipments = _shipments?.Where(x => x?.TrackingCode != trackingCode).ToArray();

                return _shipments?.Length < oldShipmentCount;
            }

            public void PrintAllShipments()
            {
                if (_shipments == null) return;
                foreach (var shipment in _shipments)
                    shipment?.PrintShipment();
            }
        }

        public static class DeliveryHelper
        {
            public static void PrintShipmentDetails(Shipment shipment)
            {
                shipment.PrintShipment();
            }
        }
    }
}