using System.Collections.Generic;
namespace GingerNote.Models{
    public interface IGingerNoteRepo {
        List<GingerNoteC> GetAllNote();
        List<GingerNoteC> GetNote(int id);
        List<GingerNoteC> GetNoteByTitle(string title, string type);
        bool PostNote(GingerNoteC c);
        bool PutNote(int id, GingerNoteC Lgn);
        bool DeleteNote(string deletestring);
    }
}