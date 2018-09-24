using System;
using System.Linq;
using System.Collections.Generic;
using GingerNote.Models;
using Microsoft.AspNetCore.Mvc;
using GingerNote.Controllers;
using Xunit;
using Moq;
namespace GingerNote.Tests
{
    public class ControllerTests
    {       
        [Fact]
        public void GetAllNote_PositiveTest(){
            DummyData DD = new DummyData();
            List<GingerNoteC> dummy = DD.DummyMock();  // Arrange
            
            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>(); // Removing Dependency
            MockRepository.Setup(d => d.GetAllNote()).Returns(dummy);
            
            GingerController gingerontroller = new GingerController(MockRepository.Object); // Act
            var actual = gingerontroller.Get();

            var okObjectResult = actual as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var actualList = okObjectResult.Value as List<GingerNoteC>;

            Assert.NotNull(actualList); // Assert
            Assert.Equal(actualList.Count , dummy.Count);
        }
        [Fact]
        public void GetAllNote_NegativeTest_One(){  // For Empty Result
            List<GingerNoteC> dummy = new List<GingerNoteC>();  // Arrange
            
            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>(); // Removing Dependency
            MockRepository.Setup(d => d.GetAllNote()).Returns(dummy);
            
            GingerController gingerontroller = new GingerController(MockRepository.Object); // Act
            var actual = gingerontroller.Get();

            var okObjectResult = actual as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<GingerNoteC>;
            Assert.NotNull(model);

            Assert.Equal(dummy.Count, model.Count);
        }
        [Fact]
        public void GetAllNote_NegativeTest_Two(){  // For Database Issue
            List<GingerNoteC> dummy = null;  // Arrange
            
            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>(); // Removing Dependency
            MockRepository.Setup(d => d.GetAllNote()).Returns(dummy);
            
            GingerController gingerontroller = new GingerController(MockRepository.Object); // Act
            var actual = gingerontroller.Get();
            Assert.IsType<NotFoundObjectResult>(actual);
        }
        [Fact]
        public void GetNoteById_PositiveTest(){
            DummyData DD = new DummyData();
            List<GingerNoteC> dummy = DD.DummyMock();  // Arrange
            int NoteId = 2;

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>(); // Removing Dependency
            MockRepository.Setup(d => d.GetNote(NoteId)).Returns(dummy.FirstOrDefault( d => d.NoteId == NoteId ));

            GingerController gingerontroller = new GingerController(MockRepository.Object); // Act
            var actual  = gingerontroller.Get(NoteId);

            var okObjectResult = actual as OkObjectResult;
            Assert.NotNull(okObjectResult); // Assert

            var actualClass = okObjectResult.Value as GingerNoteC;
            Assert.NotNull(actualClass);

            Assert.Equal( NoteId , actualClass.NoteId );
        }
        [Fact]
        public void GetNoteById_NegativeTest(){ 
            DummyData DD = new DummyData();
            List<GingerNoteC> dummy = DD.DummyMock();  // Arrange
            int NoteId = 100;

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>(); // Removing Dependency
            MockRepository.Setup(d => d.GetNote(NoteId)).Returns(dummy.FirstOrDefault( d => d.NoteId == NoteId ));

            GingerController gingerontroller = new GingerController(MockRepository.Object); // Act
            var actual  = gingerontroller.Get(NoteId);
            Assert.IsType<NotFoundObjectResult>(actual);   // Assert
        }   
        [Fact]
        public void GetNoteByTitle_PositiveTest(){ // For Checking Title
            DummyData DD = new DummyData();
            List<GingerNoteC> dummy = DD.DummyMock();
            string notetitle = "WishList";
            string type = Type.Title;

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();
            MockRepository.Setup(d => d.GetNoteByTitle(notetitle,type))
                            .Returns(dummy.Where( d => d.NoteTitle == notetitle ).ToList());

            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual  = gingerontroller.Get(notetitle,type);

            var okObjectResult = actual as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var actualList = okObjectResult.Value as List<GingerNoteC>;
            Assert.NotNull(actualList);

            Assert.Equal( 1 , actualList.Count);
        }
        [Fact]
        public void GetNoteByTitle_NegativeTest_One(){ // For Some Not Found
            DummyData DD = new DummyData();
            List<GingerNoteC> dummy = DD.DummyMock();
            string notetitle = "SomeRandomSearchStringThatIsNotFound";
            string type = Type.Title;

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();
            MockRepository.Setup(d => d.GetNoteByTitle(notetitle,type))
                            .Returns(dummy.Where( d => d.NoteTitle == notetitle ).ToList());

            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual  = gingerontroller.Get(notetitle,type);

            var NotFoundOR = actual as NotFoundObjectResult;
            Assert.NotNull(NotFoundOR);
        }
        [Fact]
        public void GetNoteByTitle_NegativeTest_Two(){ // For Some Bad Request
            DummyData DD = new DummyData();
            List<GingerNoteC> dummy = DD.DummyMock();
            string notetitle = "SomeRandomSearchStringThatIsNotFound";
            string type = null;

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();
            MockRepository.Setup(d => d.GetNoteByTitle(notetitle,type))
                            .Returns(dummy);

            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual  = gingerontroller.Get(notetitle,type);

            var BadRequestOR = actual as BadRequestObjectResult;
            Assert.NotNull(BadRequestOR);
        }
        [Fact]
        public void PostById_PositiveTest() { // For Returning Created
            GingerNoteC dummyC = new GingerNoteC{
                NoteTitle = " -- Xunit-- "
            };

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();
            
            MockRepository.Setup (d => d.PostNote(dummyC)).Returns (true);
            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual = gingerontroller.Post(dummyC);

            var createdResultvar = actual as CreatedResult;
            Assert.NotNull (createdResultvar);

            var model = createdResultvar.Value as GingerNoteC;
            Assert.Equal( dummyC.NoteTitle , model.NoteTitle );
        }
        [Fact]
        public void PostById_NegativeTest() { // For Returning Bad Request
            GingerNoteC dummyC = new GingerNoteC{
                NoteTitle = " -- Xunit-- "
            };

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();
            
            MockRepository.Setup (d => d.PostNote(dummyC)).Returns (false);
            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual = gingerontroller.Post(dummyC);

            var BadRequestOR = actual as BadRequestObjectResult;
            Assert.NotNull(BadRequestOR);
        }
        [Fact]
        public void PutById_PositiveTest() { // For Returning Created
            GingerNoteC dummyC = new GingerNoteC{
                NoteId = 1,
                NoteTitle = " -- Xunit-- "
            };

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();

            int id = (int)dummyC.NoteId;
            MockRepository.Setup (d => d.PutNote(id,dummyC)).Returns (true);
            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual = gingerontroller.Put(id,dummyC);

            var createdResultvar = actual as CreatedResult;
            Assert.NotNull(createdResultvar);

            var actualClass = createdResultvar.Value as GingerNoteC;
            Assert.Equal(id,actualClass.NoteId);
        }
        [Fact]
        public void PutById_NegativeTest() { // For Not Found
            GingerNoteC dummyC = new GingerNoteC{
                NoteId = 101, // This Dont Exit
                NoteTitle = " -- Xunit-- "
            };

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();

            int id = (int)dummyC.NoteId;
            MockRepository.Setup (d => d.PutNote(id,dummyC)).Returns (false);
            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual = gingerontroller.Put(id,dummyC);

            var NotFoundOR = actual as NotFoundObjectResult;
            Assert.NotNull(NotFoundOR);
        }
        [Fact]
        public void DeleteByTitle_PositiveTest() { // For Returning Created
            string title = "Courses";

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();

            MockRepository.Setup (d => d.DeleteNote(title)).Returns (true);
            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual = gingerontroller.Delete(title);

            var okObjectResult = actual as OkObjectResult;
            Assert.NotNull (okObjectResult);
        }
        [Fact]
        public void DeleteByTitle_NegativeTest() { // For Returning Not Found
            string title = "SomeRandomSearchStringThatIsNotFound";

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();

            MockRepository.Setup (d => d.DeleteNote(title)).Returns (false);
            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual = gingerontroller.Delete(title);

            var NotFoundOR = actual as NotFoundObjectResult;
            Assert.NotNull (NotFoundOR);
        }
    }
}
