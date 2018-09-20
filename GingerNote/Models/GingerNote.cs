using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace GingerNote.Models{
    public class GingerNoteC{
        public int Id { get; set; }
        [Required]
        public string NoteTitle { get; set; }
        public string NoteBody { get; set; }
        public bool Pinned { get; set; }
        public List<Checklist> NoteChecklist { get; set; }
        // public List<Label> NoteLabel { get; set; }
    }
    public class Checklist{
        public int ChecklistId { get; set; }
        public bool IsChecked { get; set; }
        public string ChecklistName { get; set; }
        public int Id { get; set; }
    }
    // public class Label{
    //     public int LabelId { get; set; }
    //     public string LabelName { get; set; }
    // }
}