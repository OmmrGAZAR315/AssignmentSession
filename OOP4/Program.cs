using System.Globalization;

namespace OOP4
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*
             * Q1 — Object Copying
             * a) Assigning one object variable to another copies the memory address (reference) stored in the variable, not the object itself
             * b) No, it does not create a new object. Only the reference is duplicated.
             * c)  Copying a Reference: Creates a new pointer targeting the original object in memory.
             * c) Copying an Object: Allocates a new block of memory on the heap, instantiates a duplicate object,
             * Q2 - Shallow Copy vs. Deep Copy
             * a) Shallow Copy: Duplicates the object's top-level structure
             * b) Deep Copy: Duplicates the object along with all nested or referenced objects recursively
             * c) Both the original object and the copied object share references to the same underlying reference-type instances.
             * d) Brand-new copies of all reference-type fields are instantiated so that neither object shares memory pointers.
             * e) Deep copy is required when an object contains mutable reference-type fields that must be modified independently
             *
             * Q3 — Static Members
             * a) A static field belongs to the class type itself and shares a single memory location across all instances
             * An instance field belongs to a specific object instance, with every instantiated object receiving its own separate copy.
             * b) A static method belongs to the class and can be called without instantiating an object.
             * c) A specialized constructor used to initialize static data or perform actions required only once per class lifecycle
             * d) A class marked with the static modifier that can only contain static members
             *
             * Q4 Extension Method
             * a) A special kind of static method that lets you "add" methods to existing types without modifying their original source code
             * b) The this keyword must precede the modifier of the first parameter, specifying which type the method is extending
             * c) It must be declared as a static method inside a static class.
             * d) No, an extension method cannot access private or protected members of the class it extends
             *
             * Q5 — Partial Classes and Partial Methods
             *a) A class whose definition is split across two or more source files using the partial keyword
             * The compiler merges all parts into a single class type during compilation.
             *
             * b) Enabling multiple developers to work on the same large class simultaneously without merge conflicts.
             * c) A method whose declaration (signature) is defined in one part of a partial class and its implementation is optionally provided in another part. They must return void and are implicitly private.
             * d) If a partial method has no implementation provided, the compiler removes both the method definition and all calls to it during compilation, leaving zero performance overhead.
             */
            //Q3
            int NoOfShips = 1;
            string cleanStr = default;
            decimal cleanNumber = default;

            // Console.WriteLine("Copying Object");
            // var sh = new StandardShipment("123456789", "Test Shipment", 10, 50,
            //     new DeliveryAddress("Cairo", "Test Street", "123"));
            // var copy = sh.CopyShipment();
            // Console.WriteLine("Display Before: original:{0}, copied: {1}", sh.Destination.City, copy.Destination.City);
            // sh.Destination.City = "Giza";
            // Console.WriteLine("Display After: original:{0}, copied: {1}", sh.Destination.City, copy.Destination.City);
            // Console.WriteLine("Same DeliveryAddress object? {0}", sh.Destination == copy.Destination);
            //
            // Console.WriteLine("Shallow Copying Object");
            // sh = new StandardShipment("123456789", "Test Shipment", 10, 50,
            //     new DeliveryAddress("Cairo", "Test Street", "123"));
            // var shallowCopy = sh.ShallowCopy();
            // Console.WriteLine("Display Before: original:{0}, copied: {1}", sh.Destination.City, shallowCopy.Destination.City);
            // shallowCopy.Destination.City = "Alexandria-Shallow";
            // Console.WriteLine("Display After: original:{0}, copied: {1}", sh.Destination.City, shallowCopy.Destination.City);
            // Console.WriteLine("Same DeliveryAddress object? {0}", shallowCopy.Destination == sh.Destination);
            //
            // Console.WriteLine("Deep Copying Object");
            // sh = new StandardShipment("123456789", "Test Shipment", 10, 50,
            //     new DeliveryAddress("Cairo", "Test Street", "123"));
            // var deepCopy = sh.DeepCopy();
            // Console.WriteLine("Display Before: original:{0}, copied: {1}", sh.Destination.City, deepCopy.Destination.City);
            // deepCopy.Destination.City = "Aswan-Deep";
            // Console.WriteLine("Display After: original:{0}, copied: {1}", sh.Destination.City, deepCopy.Destination.City);
            // Console.WriteLine("Same DeliveryAddress object? {0}", deepCopy.Destination == sh.Destination);

            var driver = new Driver(1, "el 7g Gom3a", "0123456789");
            var deliveryCenter = new DeliveryCenter(driver, NoOfShips);

            Console.Write("Enter Center Name: ");
            deliveryCenter.CenterName = sanitizeStrInput(cleanStr, Console.ReadLine());

            DeliveryUtilities.PrintSystemTitle();

            Console.WriteLine($"Driver: {deliveryCenter.Driver.FullName}");

            var array = new Dictionary<string, List<int>>
            {
                { "StandardShipment", [0] },
                // { "ExpressShipment", [1] },
                // { "InternationalShipment", [2] }
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

                Console.WriteLine("Shipment added essfully.");
            }

            Console.WriteLine("--- All Shipments");
            DeliveryUtilities.PrintSeparator();

            deliveryCenter.PrintAllShipments();

            Console.WriteLine("Enter a tracking code to search for a shipment:");
            string SearchingTrackingCode = sanitizeStrInput(cleanStr, Console.ReadLine());

            Console.WriteLine("Searching for shipment...");
            try
            {
                Shipment? foundShipment = deliveryCenter[SearchingTrackingCode];
                if (foundShipment == null)
                    Console.WriteLine($"Shipment with tracking code {SearchingTrackingCode} not found.");
                else
                    DeliveryHelper.PrintShipmentDetails(foundShipment);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Shipment with tracking code {SearchingTrackingCode} not found.");
                return;
            }

            // Console.WriteLine("Remove Shipment.....");
            // deliveryCenter.RemoveShipment(SearchingTrackingCode);
            // Console.WriteLine("Shipment Removed");
            // Console.WriteLine("The Remaining Shipments are:");
            deliveryCenter.PrintAllShipments();

            Console.WriteLine("Updating Weight...");
            var w = deliveryCenter[SearchingTrackingCode].Weight;
            Console.WriteLine($"Original Weight: {w} kg");
            deliveryCenter[SearchingTrackingCode].SetWeight(w, 10);
            Console.WriteLine($"Updated Weight After Packing : {deliveryCenter[SearchingTrackingCode].Weight} kg");

            Console.WriteLine($"total shipment counter: {Shipment.GetTotalShipmentsCreated()}");

            deliveryCenter[0].GetSummary();
            deliveryCenter[0].IsDelivered();
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

        public class DeliveryAddress
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

            public DeliveryAddress Copy()
            {
                return new DeliveryAddress(City, Street, BuildingNumber);
            }
        }

        public abstract partial class Shipment
        {
            string _TrackingCode;

            string _Description;
            decimal _Weight;
            decimal _DeliveryFee;

            public static int TotalShipmentsCreated = 0;

            static Shipment()
            {
                TotalShipmentsCreated++;
                Console.WriteLine("Shipment System Initialized");
            }

            public static int GetTotalShipmentsCreated()
            {
                return TotalShipmentsCreated;
            }

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

            public abstract Shipment CopyShipment();
            public abstract Shipment ShallowCopy();
            public abstract Shipment DeepCopy();

            public DeliveryAddress Destination { get; set; }

            public abstract decimal EstimatedCost { get; }
            public string TrackingStatus = "Delivered";

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

            public partial void OnTrackingStatusChanged(string newStatus)
            {
                Console.WriteLine("Tracking status changed to: Out For Delivery");
            }
        }

        public abstract partial class Shipment
        {
            public partial void OnTrackingStatusChanged(string newStatus);

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

            public abstract void PrintShipment();
        }

        public static class DeliveryUtilities
        {
            public static void PrintSeparator()
            {
                Console.WriteLine("==========================================");
            }

            public static void PrintSystemTitle()
            {
                Console.WriteLine("Delivery Center");
            }
        }

        public class StandardShipment : Shipment
        {
            public StandardShipment(string trackingCode, string description, decimal weight, decimal deliveryFee,
                DeliveryAddress destination)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
            }

            public override Shipment CopyShipment()
            {
                return this;
            }

            public override Shipment ShallowCopy()
            {
                return (StandardShipment)MemberwiseClone();
            }

            public override Shipment DeepCopy()
            {
                return new StandardShipment(TrackingCode, Description, Weight, DeliveryFee, Destination.Copy());
            }

            public override decimal EstimatedCost { get; }

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


        public sealed class CompletedShipment : Shipment
        {
            public CompletedShipment(string trackingCode, string description, decimal weight, decimal deliveryFee,
                DeliveryAddress destination)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
            }

            public override Shipment CopyShipment()
            {
                throw new NotImplementedException();
            }

            public override Shipment ShallowCopy()
            {
                throw new NotImplementedException();
            }

            public override Shipment DeepCopy()
            {
                throw new NotImplementedException();
            }

            public override decimal EstimatedCost { get; }

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

            public override Shipment CopyShipment()
            {
                throw new NotImplementedException();
            }

            public override Shipment ShallowCopy()
            {
                throw new NotImplementedException();
            }

            public override Shipment DeepCopy()
            {
                throw new NotImplementedException();
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

            public override Shipment CopyShipment()
            {
                throw new NotImplementedException();
            }

            public override Shipment ShallowCopy()
            {
                throw new NotImplementedException();
            }

            public override Shipment DeepCopy()
            {
                throw new NotImplementedException();
            }

            public override decimal EstimatedCost
            {
                get => DeliveryFee + (Weight * 5) + CustomsFee;
            }

            public virtual void GenerateCustomsReport()
            {
                Console.WriteLine(
                    $"SGenerating customs report for shipment {TrackingCode} to {DestinationCountry} with customs fee of ${CustomsFee}");
            }

            public InternationalShipment(decimal customsFee, string destinationCountry, string trackingCode,
                string description, decimal weight, decimal deliveryFee, DeliveryAddress destination)
                : base(trackingCode, description, weight, deliveryFee, destination)
            {
                CustomsFee = customsFee;
                DestinationCountry = destinationCountry;
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
                        if (shipment?.TrackingCode == index)
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
                var newPos = Array.FindIndex(_shipments, x => x == null);
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

    public static class ShipmentExtensions
    {
        public static void GetSummary(this Program.Shipment shipment)
        {
            Console.WriteLine("Getting Summary...");
            Console.WriteLine(
                $"Tracking Code: {shipment.TrackingCode}, Description: {shipment.Description}, Weight: {shipment.Weight} kg, Delivery Fee: ${shipment.DeliveryFee}, Estimated Cost: ${shipment.EstimatedCost}");
        }

        public static bool IsDelivered(this Program.Shipment shipment)
        {
            return shipment.TrackingStatus == "Delivered";
        }
    }
}