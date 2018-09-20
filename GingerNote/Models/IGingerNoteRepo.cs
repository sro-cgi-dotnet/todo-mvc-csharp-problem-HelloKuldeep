using System.Collections.Generic;
namespace GingerNote.Models{
    public interface IGingerNoteRepo {
        List<GingerNoteC> GetAllNote();
        // List<GingerNote> GetNote(int id);
        
        // public List<GingerNote> GetNoteByTitle(string title){
        //     GingerNoteContext context = new GingerNoteContext();
        //     List<GingerNote> Lc = context.GingerNoteT.Where(ci => ci.Id == id).ToList();
        //     return Lc;           
        // }
        // public List<GingerNote> GetNoteByLabel(string title){
        //     GingerNoteContext context = new GingerNoteContext();
        //     List<GingerNote> Lc = context.GingerNoteT.Where(ci => ci.Id == id).ToList();
        //     return Lc;           
        // }
        // public List<GingerNote> GetPinnedNote(string title){
        //     GingerNoteContext context = new GingerNoteContext();
        //     List<GingerNote> Lc = context.GingerNoteT.Where(ci => ci.Id == id).ToList();
        //     return Lc;           
        // }
        bool PostNote(GingerNoteC c);
        // public bool PutNote(GingerNote c){
        //     if( c != null ){
        //         GingerNoteContext context = new GingerNoteContext();
        //         context.GingerNoteT.Add(c);
        //         context.SaveChanges();
        //         return true;
        //     }
        //     return false;
        // }
        // public bool DeleteNote(GingerNote c){
        //     if( c != null ){
        //         GingerNoteContext context = new GingerNoteContext();
        //         context.GingerNoteT.Add(c);
        //         context.SaveChanges();
        //         return true;
        //     }
        //     return false;
        // }
    }
}