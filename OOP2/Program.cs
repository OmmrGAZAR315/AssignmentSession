namespace OOP1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Q1
            // Difference between a class and a struct
            // Class: A reference type stored on the heap.
            // Copying it creates a new pointer to the same data.Struct:
            // A value type stored on the stack. Copying it creates an independent duplicate of the data.

            // Q1
            //Why classes fit large applications
            // No expensive copying: Classes pass by reference, avoiding the performance hit of duplicating large data.
            // Supports inheritance: Allows code reuse and complex architecture, which structs cannot do.
            // Shared state: Multiple parts of an app can easily reference and update the exact same object
            // .
            // Q2
            // a) Which class is the parent class? Shipment
            // b) Which class is the child class? ExpressShipment
            // c) What members are inherited by ExpressShipment  ? TrackingCode
            // d)  Why is inheritance better than duplicating the same code in multiple classes? 
            // Inheritance allows for code reuse and a more maintainable architecture by defining
            // a common base class with shared functionality, rather than duplicating the same code in each derived class.
            //Q3
            int NoOfShips = 1;
            string cleanStr = default;
            double cleanNumber = default;

            DeliveryCenter DeliveryCenter = new DeliveryCenter(NoOfShips);

            Console.Write("Enter Center Name: ");
            DeliveryCenter.CenterName = sanitizeStrInput(cleanStr, Console.ReadLine());

            var array = new Dictionary<string, List<int>> {
                { "StandardShipment", new List<int> { 0 } },
                { "ExpressShipment", new List<int> { 1 } },
                {  "InternationalShipment", new List<int> { 2 } }
            };
            for (int i = 0; i < NoOfShips; i++)
            {
                fetchData(i, out string TrackingCode, out string Description, out double WeightStr, out double DeliveryFee, out DeliveryAddress address);

                switch (true)
                {
                    case bool _ when array["StandardShipment"].Contains(i):
                        StandardShipment shipment = new StandardShipment(TrackingCode, Description, WeightStr, DeliveryFee, address);
                        DeliveryCenter[i] = shipment;
                        break;

                    case bool _ when array["ExpressShipment"].Contains(i):
                        ExpressShipment expressShipment = new ExpressShipment(10, TrackingCode, Description, WeightStr, DeliveryFee, address);
                        DeliveryCenter[i] = expressShipment;
                        break;

                    case bool _ when array["InternationalShipment"].Contains(i):
                        InternationalShipment internationalShipment = new InternationalShipment(20, "Canada", TrackingCode, Description, WeightStr, DeliveryFee, address);
                        DeliveryCenter[i] = internationalShipment;
                        break;
                }

                Console.WriteLine("Shipment added successfully.");
            }

            Console.WriteLine("--- All Shipments");

            DeliveryCenter.PrintAllShipments();

            Console.WriteLine("Enter a tracking code to search for a shipment:");
            string SearchingTrackingCode = sanitizeStrInput(cleanStr, Console.ReadLine());

            Console.WriteLine("Searching for shipment...");
            try
            {
                Shipment foundShipment = DeliveryCenter[SearchingTrackingCode];
                Console.WriteLine($"Shipment with tracking code {SearchingTrackingCode} found:");
                Console.WriteLine($"Shipment Details:");
                foundShipment.PrintShipment();
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Shipment with tracking code {SearchingTrackingCode} not found.");
                return;
            }

            Console.WriteLine("Remove Shipment.....");
            DeliveryCenter.RemoveShipment(SearchingTrackingCode);
            Console.WriteLine("The Remaining Shipments are:");
            DeliveryCenter.PrintAllShipments();
        }

        public static void fetchData(int i, out string TrackingCode, out string Description, out double WeightStr, out double DeliveryFee, out DeliveryAddress address)
        {
            string cleanStr = default;
            double cleanNumber = default;

            Console.WriteLine($"Enter Shipment {i + 1} Data");
            Console.Write("Tracking Code: ");
            TrackingCode = sanitizeStrInput(cleanStr, Console.ReadLine());
            Console.Write("Description: ");
            Description = sanitizeStrInput(cleanStr, Console.ReadLine());

            Console.Write("Weight: ");
            WeightStr = sanitizeNumberInput(cleanNumber, Console.ReadLine());
            Console.Write("Delivery Fee: ");
            DeliveryFee = sanitizeNumberInput(cleanNumber, Console.ReadLine());

            Console.Write("City: ");
            string City = sanitizeStrInput(cleanStr, Console.ReadLine());
            Console.Write("Street: ");
            string Street = sanitizeStrInput(cleanStr, Console.ReadLine());
            Console.Write("Building Number: ");
            string BuildingNumber = sanitizeStrInput(cleanStr, Console.ReadLine());

            address = new DeliveryAddress(City, Street, BuildingNumber);
        }

        public static double sanitizeNumberInput(double cleanNumber, string input)
        {
            while (!TrySanitizeInputs(out string _, out cleanNumber, Int: input))
            {
                input = Console.ReadLine();
            }
            return cleanNumber;
        }
        public static string sanitizeStrInput(string cleanStr, string input)
        {
            while (!TrySanitizeInputs(out cleanStr, out double _, Str: input))
            {
                input = Console.ReadLine();
            }
            return cleanStr;
        }
        public static bool TrySanitizeInputs(out string sanitizedStr, out double sanitizedInt, string Str = default, string Int = default)
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
                if (Double.TryParse(Int, out double result)) sanitizedInt = result;
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

        public class Shipment
        {
            string _TrackingCode;

            string _Description;
            double _Weight;
            double _DeliveryFee;
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
            public double Weight
            {
                set
                {
                    if (value > 0)
                        _Weight = value;
                }
                get => _Weight;
            }
            public double DeliveryFee
            {
                private set
                {
                    if (value > 0)
                        _DeliveryFee = value;
                }
                get => _DeliveryFee;
            }
            public DeliveryAddress Destination { get; set; }

            public double EstimatedCost
            {
                get => DeliveryFee + (Weight * 5);

            }

            public Shipment(string trackingCode)
            {
                TrackingCode = trackingCode;
                Description = "Unknown";
                Weight = 1;
                DeliveryFee = 50;
                Destination = new DeliveryAddress("Unknown", "Unknown", "Unknown");
            }

            public Shipment(string trackingCode, string description, double weight, double deliveryFee, DeliveryAddress destination)
            {
                TrackingCode = trackingCode;
                Description = description;
                Weight = weight;
                DeliveryFee = deliveryFee;
                Destination = destination;
            }

            public void UpdateDeliveryFee(decimal newFee)
            {
                if (newFee > 0)
                {
                    DeliveryFee = (double)newFee;
                }
            }

            public void PrintShipment()
            {
                Console.WriteLine("");
                Console.WriteLine($"Tracking Code: {TrackingCode}");
                Console.WriteLine($"Description: {Description}");
                Console.WriteLine($"Weight: {Weight} kg");
                Console.WriteLine($"Delivery Fee: ${DeliveryFee}");
                Console.WriteLine($"Destination: {Destination.GetFullAddress()}");
                Console.WriteLine($"Estimated Cost: ${EstimatedCost}");
                Console.WriteLine("======================================================");
            }
        }
        public class StandardShipment : Shipment
        {
            public StandardShipment(string trackingCode, string description, double weight, double deliveryFee, DeliveryAddress destination)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
            }
        }
        public class ExpressShipment : Shipment
        {
            double _ExtraFee;
            public double ExtraFee
            {
                set
                {
                    if (value >= 0)
                        _ExtraFee = value;
                }
                get => _ExtraFee;
            }

            public double EstimatedCost
            {
                get => DeliveryFee + (Weight * 5) + ExtraFee;
            }

            public ExpressShipment(double extraFee, string trackingCode, string description, double weight, double deliveryFee, DeliveryAddress destination)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
                ExtraFee = extraFee;
            }
        }

        public class InternationalShipment : Shipment
        {
            string _DestinationCountry;
            decimal _CustomsFee;
            public string DestinationCountry
            {
                set
                {
                    if (!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value))
                        _DestinationCountry = value;
                }
                get => _DestinationCountry;
            }

            public decimal CustomsFee
            {
                set
                {
                    if (value >= 0)
                        _CustomsFee = value;
                }
                get => _CustomsFee;
            }

            public double EstimatedCost
            {
                get => DeliveryFee + (Weight * 5) + (double)CustomsFee;
            }
            public InternationalShipment(decimal customsFee, string DestinationCountry, string trackingCode, string description, double weight, double deliveryFee, DeliveryAddress destination)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
                CustomsFee = customsFee;
                DestinationCountry = DestinationCountry;
            }
        }
        class DeliveryCenter
        {
            public string CenterName;

            private Shipment[] _shipments;

            public Shipment this[int index]
            {
                get
                {
                    if (_shipments != null && index >= 0 && index < _shipments.Length)
                        return _shipments[index];
                    else
                        return default;
                }
                set
                {
                    _shipments[index] = value;
                }
            }

            public Shipment this[string index]
            {
                get
                {
                    foreach (Shipment shipment in _shipments)
                    {
                        if (shipment.TrackingCode == index)
                            return shipment;
                    }
                    return default;
                }
            }

            public DeliveryCenter(int capacity = 20)
            {
                if (capacity > 20) throw new ArgumentException("Capacity must be less than or equal to 20");
                _shipments = new Shipment[capacity];
            }

            public bool AddShipment(Shipment shipment)
            {
                int newPos = _shipments.Length;
                this[newPos] = shipment;

                // suppose be there exception thrown in int setter indexer 
                return shipment.Equals(this[newPos]);
            }

            public bool RemoveShipment(string index)
            {
                for (int i = 0; i < _shipments.Length; i++)
                {
                    if (_shipments[i] != null && _shipments[i].TrackingCode == index)
                    {
                        _shipments[i] = null;
                        return true;
                    }
                }
                return false;
            }

            public void PrintAllShipments()
            {
                foreach (Shipment shipment in _shipments)
                {
                    if (shipment != null)
                        shipment.PrintShipment();
                }
            }

        }
    }
}
