using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataContext
{
    public class User
    {
        public User()
        {
            this.Stories = new HashSet<Story>();
            this.Groups = new HashSet<Group>();
        }

        [Key]
        public string Name { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Story> Stories { get; set; }
    }

    public class Story
    {
        public Story()
        {
            this.Groups = new HashSet<Group>();
        }

        public int ID { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(50)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(200)]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(5000)]
        public string Content { get; set; }


        [Required]
        public DateTime PostedOn { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }

    public class Group
    {
        public Group()
        {
            this.Users = new HashSet<User>();
            this.Stories = new HashSet<Story>();
        }

        public int ID { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(50)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false), MaxLength(200)]
        public string Description { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Story> Stories { get; set; }
    }

    public class StoryBookEntities : DbContext
    {


        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Story> Stories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Story>()
                .HasRequired<User>(s => s.Author)
                .WithMany(s => s.Stories);

            modelBuilder.Entity<User>()
                .HasMany<Group>(g => g.Groups)
                .WithMany(u => u.Users)
                .Map(us =>
                {
                    us.MapLeftKey("Name");
                    us.MapRightKey("GroupID");
                    us.ToTable("UserGroup");
                });

            modelBuilder.Entity<Group>()
                .HasMany<Story>(s => s.Stories)
                .WithMany(g => g.Groups)
                .Map(sg =>
                {
                    sg.MapLeftKey("GroupID");
                    sg.MapRightKey("StoryID");
                    sg.ToTable("StoryGroup");
                });
        }
    }
}
