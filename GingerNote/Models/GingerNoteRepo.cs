using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GingerNote.Models{
    public class GingerNoteRepo : IGingerNoteRepo {
        GingerNoteContext context = null ;
        public GingerNoteRepo(GingerNoteContext _context){
            this.context = _context;
        }
        public List<GingerNoteC> GetAllNote(){
            List<GingerNoteC> Lg = context.GingerNoteT
                                    .Include( n => n.NoteChecklist )
                                    .Include( n => n.NoteLabel )
                                    .ToList();
            return Lg;
        }
        public List<GingerNoteC> GetNote(int id){
            List<GingerNoteC> Lg = context.GingerNoteT
                                    .Where( n => n.NoteId == id )
                                    .Include( n => n.NoteChecklist )
                                    .Include( n => n.NoteLabel )
                                    .ToList();
            return Lg;
        }
        public List<GingerNoteC> GetNoteByTitle(string searchstring, string type){
            List<GingerNoteC> finalLg = new List<GingerNoteC>();
            if( type == "title"){
                finalLg = context.GingerNoteT
                    .Where( n => n.NoteTitle == searchstring)
                    .Include( n => n.NoteChecklist )
                    .Include( n => n.NoteLabel )
                    .ToList();
            } else if ( type == "label" ) {
                List<GingerNoteC> Lg = context.GingerNoteT
                    .Include( n => n.NoteChecklist )
                    .Include( n => n.NoteLabel )
                    .ToList();
                foreach(GingerNoteC Gn in Lg){
                    foreach(Label Lb in Gn.NoteLabel){
                        if( Lb.LabelName == searchstring )
                            finalLg.Add(Gn);
                    }
                }
            } else if ( type == "pinned" ) {
                if( searchstring == "true" )
                    finalLg = context.GingerNoteT
                        .Where( n => n.Pinned == true )
                        .Include( n => n.NoteChecklist )
                        .Include( n => n.NoteLabel )
                        .ToList();
                else 
                    finalLg = context.GingerNoteT
                        .Where( n => n.Pinned == false )
                        .Include( n => n.NoteChecklist )
                        .Include( n => n.NoteLabel )
                        .ToList();
            }
            return finalLg;
        }
        public bool PostNote(GingerNoteC c){
            if( context.GingerNoteT.FirstOrDefault( n => n.NoteId == c.NoteId) == null ){
                context.GingerNoteT.Add(c);
                PostCheckList(c);
                PostLabel(c);
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
            }
        }
        void PostLabel(GingerNoteC c){
            if( c.NoteLabel != null ){
                foreach(Label la in c.NoteLabel){
                    context.LabelT.Add(la);
                }
            }
        }
        public bool PutNote( int id, GingerNoteC Lgn){
            context.Update<GingerNoteC>(Lgn);
            context.SaveChanges();
            return true;
        }
        public bool DeleteNote(string deletestring){
            List<GingerNoteC> Gn = context.GingerNoteT
                                .Where( n => n.NoteTitle == deletestring)
                                .ToList();
            if( Gn.Count > 0 ){
                context.RemoveRange(Gn);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}