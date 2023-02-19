using HotelBookingAPI.Models;

namespace HotelBookingAPI.Utils
{
    //Factory Method
    public abstract class Generator
    {
        protected Random random;
        public HashSet<string> Names { get; private set; }
        public HashSet<string> Destinations { get; private set; }

        #region CTORS
        public Generator(string pathForNames, string pathForDestinations)
        {
            random = new Random();
            Names = new(File.ReadLines(pathForNames, System.Text.Encoding.UTF8));
            Destinations = new(File.ReadLines(pathForDestinations, System.Text.Encoding.UTF8));
        }
        public Generator(HashSet<string> _Names, HashSet<string> _Destinations)
        {
            random = new Random();
            Names = _Names;
            Destinations = _Destinations;
        }
        public Generator()
        {
            random = new Random();

            Names = new()
            {
                "Alex",
                "Arina",
                "Brian",
                "Camile",
                "Dora",
                "Francis",
                "Kristine",
                "Nick",
                "Sam",
                "Sergey",
            };

            Destinations = new()
            {
                "Moscow",
                "Saint-Petersburg",
                "Novorosiisk",
                "Volgograd",
                "Anapa",
                "Yaroslavl",
                "Bratsk",
                "Samara",
                "Syrgyt",
                "Krim",
            };
        }
        #endregion

        abstract public Passenger Generate();
        abstract public void GeneratePrompt(ref Passenger passenger);
        abstract public void GenerateName(ref Passenger passenger);
        abstract public void GeneratePassport(ref Passenger passenger);
    }

    //Можем создавать новые генераторы у которых будут новые способы генерации
    public class UsingPrimitiveRandom : Generator
    {
        public UsingPrimitiveRandom(string path_names, string path_destinations) : base(path_names, path_destinations) { }
        public UsingPrimitiveRandom() : base() { }
        public override Passenger Generate()
        {
            Random random = new();
            Passenger passenger = new(random.Next(), Names.ElementAt(random.Next(Names.Count)));
            GeneratePrompt(ref passenger);
            return passenger;
        }

        public override void GeneratePrompt(ref Passenger passenger)
        {
            var passenger_req = new Prompt() { When = RandomDate(), Where = Destinations.ElementAt(random.Next(Destinations.Count)) }; //where вообще по хэшсету будет долго итерировать надо будет менять стратегию
            passenger.Request = passenger_req;
        }

        public override void GenerateName(ref Passenger passenger)
        {
            passenger.Name_Surname = Names.ElementAt(random.Next(Names.Count));
        }

        public override void GeneratePassport(ref Passenger passenger)
        {
            passenger.Passport = random.Next();
        }

        private static DateTime RandomDate()
        {
            Random gen = new();
            var start = DateTime.Now;
            DateTime end;
        retrier:
            try
            {
                end = new DateTime(start.Year, start.Month + gen.Next(0, 4), start.Day + gen.Next(1, 28));
            }
            catch
            {
                goto retrier;
            }
            int range = (end - start).Days;
            return end.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        }
    }

    //например можем создать генератор используя RNGCryptoServiceProvider или RandomNumberGenerator.Create()
    //где уже будет рандом чуть рандомнее и не предсказуемее
}
