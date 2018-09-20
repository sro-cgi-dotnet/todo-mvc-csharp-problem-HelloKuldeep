using Microsoft.EntityFrameworkCore;
namespace GingerNote.Models{
    public class GingerNoteContext : DbContext {
        public DbSet<GingerNoteC> GingerNoteT { get; set; }
        public DbSet<Checklist> ChecklistT { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Gingertest1DB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<GingerNoteC>().HasMany(n => n.NoteChecklist).WithOne().HasForeignKey(c => c.Id);
        }
    }
}