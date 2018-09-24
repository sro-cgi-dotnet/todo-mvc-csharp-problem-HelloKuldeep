using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GingerNote.Models;

namespace GingerNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GingerController : ControllerBase
    {
        IGingerNoteRepo gingerNoteRepo;
        public GingerController(IGingerNoteRepo _gingerNoteRepo){
            this.gingerNoteRepo = _gingerNoteRepo;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get(){
            List<GingerNoteC> Lg = gingerNoteRepo.GetAllNote();
            if( Lg != null )
                return Ok(Lg);
            return NotFound("No Ginger Found.");
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id){
            GingerNoteC Lg = gingerNoteRepo.GetNote(id);
            if( Lg != null )
                return Ok(Lg);
            return NotFound($"No Ginger With Id:{id} Found.");
        }
        [HttpGet("{searchstring}")]
        public IActionResult Get(string searchstring, [FromQuery] string type ){
            if( type == null ) return BadRequest("Search Parameter Not Given. [?type=title]");
            List<GingerNoteC> Lg = gingerNoteRepo.GetNoteByTitle(searchstring, type);
            if( Lg.Count > 0 )
                return Ok(Lg);
            return NotFound($"No Ginger With Title/Pinned:{searchstring} Found.");
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] GingerNoteC g){
            if(gingerNoteRepo.PostNote(g)){
                return Created("/api/values",g);
            }
            return BadRequest("Database Error!! Note already found.");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GingerNoteC Lgn){
            if(ModelState.IsValid){
                bool val = gingerNoteRepo.PutNote(id, Lgn);
                if(val)
                    return Created("/api/values",Lgn);
                else
                    return NotFound($"Ginger With Id:{id} Not Found.");
            }
            return BadRequest("MODE ERROR.");
        }

        // DELETE api/values/5
        [HttpDelete("{deletestring}")]
        public IActionResult Delete(string deletestring){
            bool temp = gingerNoteRepo.DeleteNote(deletestring);
            // List<GingerNoteC> Lg = gingerNoteRepo.GetAllNote();
            // if( Lg.Count > 0 ){
            if( temp ){
                // return Created("/api/values",Lg);
                return Ok($"Note {deletestring} Deleted.");
            }
            return NotFound($"No Title:{deletestring} Found. [or Database Empty]");
        }
    }
}
