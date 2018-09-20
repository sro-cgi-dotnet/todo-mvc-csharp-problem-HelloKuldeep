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
            return Ok("No Ginger Note Found.");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
