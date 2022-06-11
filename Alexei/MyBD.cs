using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Alexei
{
    public class bs
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
    public class Player : bs
    {
        public string Surname { get; set; }
        public int Number { get; set; }
        public int Age { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

    }
    public class Coach : bs
    {
        public string Surname { get; set; }
        public int Expirience { get; set; }
        public int Age { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        
    }
    public class Team : bs
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public virtual ICollection<Player> Player { get; set; }
        public virtual ICollection<Coach> Coach { get; set; }
    }
    public class MyBD : DbContext
    {
        public DbSet<Player> Player { get; set; }
        public DbSet<Coach> Coach { get; set; }
        public DbSet<Team> Team { get; set; }
        public MyBD() : base(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\Alexei\Alexei\Database1.mdf;Integrated Security=True") { }

    }
}
