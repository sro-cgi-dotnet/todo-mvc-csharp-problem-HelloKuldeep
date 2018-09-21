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
            if( Lg.Count > 0 )
                return Ok(Lg);
            return Ok("No Ginger Found.");
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        public ActionResult<string> Get(int id){
            List<GingerNoteC> Lg = gingerNoteRepo.GetNote(id);
            if( Lg.Count > 0 )
                return Ok(Lg);
            return Ok($"No Ginger With Id:{id} Found.");
        }
        [HttpGet("{searchstring}")]
        public ActionResult<string> Get(string searchstring, [FromQuery] string type ){
            if( type == null ) return BadRequest("Search Parameter Not Given. [?type=title]");
            List<GingerNoteC> Lg = gingerNoteRepo.GetNoteByTitle(searchstring, type);
            if( Lg.Count > 0 )
                return Ok(Lg);
            return Ok($"No Ginger With Title/Pinned:{searchstring} Found.");
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] GingerNoteC g){
            if(gingerNoteRepo.PostNote(g)){
                return Created("/api/values",g);
            }
            return BadRequest("Database Error!!");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] GingerNoteC Lgn){
            if(ModelState.IsValid){
                bool val = gingerNoteRepo.PutNote(id, Lgn);
                if(val)
                    return Created("/api/values",Lgn);
                else
                    return BadRequest($"Ginger With Id:{id} Not Found.");
            }
            return BadRequest("MODE ERROR.");
        }

        // DELETE api/values/5
        [HttpDelete("{deletestring}")]
        public ActionResult Delete(string deletestring){
            bool temp = gingerNoteRepo.DeleteNote(deletestring);
            List<GingerNoteC> Lg = gingerNoteRepo.GetAllNote();
            if( Lg.Count > 0 ){
                return Created("/api/values",Lg);
            }
            return NotFound($"No Title:{deletestring} Found. [or Database Empty]");
        }
    }
}
