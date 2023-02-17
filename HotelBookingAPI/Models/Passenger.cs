namespace HotelBookingAPI.Models
{
    public struct Prompt
    {
        public string? Where;
        public DateTime? When;
        public override string ToString() => $"{Where}\t{When}";
    }

    public enum State
    {
        HaveIdeaState,
        ToAirportState,
        //TODO: add more states
    }

    public class Passenger
    {
        public Guid Id { get; set; }
        public int Passport { get; set; }
        public string? Name_Surname { get; set; }
        public bool? Bagage { get; set; }
        public Prompt? Request { get; set; }
        public State State { get; set; }
    }
}
