using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Models
{
    public class Prompt
    {
        public string? Where { get; set; }
        public DateTime? When { get; set; }
        public override string ToString() => $"{Where}\t{When}";
    }

    public enum State
    {
        HaveIdeaState,
        BuyTicketState,
        //TODO: add more states
    }

    [PrimaryKey(nameof(guid))]
    public class Passenger
    {
        public Guid guid { get; set; }
        public int Passport { get; set; }
        public Guid? Id_Ticket { get; set; }
        public string? Name_Surname { get; set; }
        public bool? Bagage { get; set; }
        public Prompt? Request { get; set; }

        [Range(0,6)]
        public State State { get; set; }

        #region CTORS
        public Passenger()
        {
            //Id = Guid.NewGuid();
        }
        public Passenger(int _Passport, string _Name_Surname) //реквест будет генерироваться в шаге идеи
        {
            guid = Guid.NewGuid();
            Passport = _Passport;
            Id_Ticket = Guid.Empty; //нет билета
            Name_Surname = _Name_Surname;
            State = State.HaveIdeaState;
        }
        public Passenger(int _Passport, string _Name_Surname, Prompt _Request, State _State, Guid _Id_Ticket) //Для создания пассажира в самолёте (Он получит билет, куда летит, когда преземлится и состояние у самолёта)
        {
            guid = Guid.NewGuid();
            Passport = _Passport;
            Id_Ticket = _Id_Ticket;
            Name_Surname = _Name_Surname;
            Request = _Request;
            State = _State;
        }
        #endregion
    }
}
