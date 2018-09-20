using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace GingerNote.Models{
    public class GingerNoteC{
        [Key]
        public int NoteId { get; set; }
        [Required]
        public string NoteTitle { get; set; }
        public string NoteBody { get; set; }
        public bool Pinned { get; set; }
        public List<Checklist> NoteChecklist { get; set; }
        // public List<Label> NoteLabel { get; set; }
    }
    public class Checklist{
        [Key]
        public int ChecklistId { get; set; }
        public bool IsChecked { get; set; }
        public string ChecklistName { get; set; }
        public int NoteId { get; set; }
    }
    // public class Label{
    //     public int LabelId { get; set; }
    //     public string LabelName { get; set; }
    // }
}