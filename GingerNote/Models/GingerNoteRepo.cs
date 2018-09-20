using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace GingerNote.Models{
    public class GingerNoteRepo : IGingerNoteRepo {
        GingerNoteContext context = null ;
        public GingerNoteRepo(GingerNoteContext _context){
            this.context = _context;
        }
        public List<GingerNoteC> GetAllNote(){
            List<GingerNoteC> Lg = context.GingerNoteT.Include( n => n.NoteChecklist ).ToList();
            return Lg;
        }
        // public List<GingerNote> GetNote(int id){
        //     GingerNoteContext context = new GingerNoteContext();
        //     List<GingerNote> Lc = context.GingerNoteT.Where(ci => ci.Id == id).ToList();
        //     return Lc;           
        // }
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
        public bool PostNote(GingerNoteC c){
            if( context.GingerNoteT.FirstOrDefault( n => n.NoteId == c.NoteId) == null ){
                context.GingerNoteT.Add(c);
                PostCheckList(c);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        void PostCheckList(GingerNoteC c){
            if( c.NoteChecklist != null ){
                foreach(Checklist cl in c.NoteChecklist){
                    context.ChecklistT.Add(cl);
                }
                context.SaveChanges();
            }
        }
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