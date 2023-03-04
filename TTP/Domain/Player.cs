using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace TTP.Domain
{
    [Table("PLAYER")]
    public class Player
    {
        [Column("ID")]
        public int Id { get; set; }

        [Column("FULL_NAME")]
        public string FullName { get; set; }

        [Column("PHONE")]
        public string Phone { get; set; }

        [Column("USER_ID")]
        public int UserId { get; set; }

        [Column("TEAM_ID")]
        public Guid TeamId { get; set; }

        [Column("RAITING")]
        public int Raiting { get; set; }

        public List<Tournament> Tournaments { get; set;}
    }
}
