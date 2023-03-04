using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TTP.Domain;

namespace TTP.Models
{
    public class TournnamentViewModel
    {
        public TournnamentViewModel()
        {
            Players = new List<Player>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int MaxPlayers { get; set; }

        public int MinPlayers { get; set; }

        public int MaxRaiting { get; set; }

        public string TournamentType { get; set; }

        public int Count { get; set; }

        public List<Player> Players { get; set; }
    }
}
