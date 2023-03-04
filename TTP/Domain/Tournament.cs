using System.ComponentModel.DataAnnotations.Schema;

namespace TTP.Domain
{
    [Table("TOURNAMENT")]
    public class Tournament
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("DATE")]
        public DateTime Date { get; set; }

        [Column("MIN_PLAYERS")]
        public int MinPlayers { get; set; }

        [Column("MAX_PLAYERS")]
        public int MaxPlayers { get; set; }

        [Column("MAX_RAITING")]
        public int MaxRaiting { get; set; }

        [Column("TOURNAMENT_TYPE")]
        public string TournamentType { get; set; }

        public List<Player> Players { get; set; }
    }
}
