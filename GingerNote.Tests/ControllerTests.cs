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
        private List<GingerNoteC> DummyMock(){
            return new List<GingerNoteC>{
                new GingerNoteC{
                    NoteId = 1,
                    NoteTitle = "WishList",
                    NoteBody = "Nothing as such",
                    NoteChecklist = new List<Checklist>{
                        new Checklist{
                            ChecklistId = 1,
                            ChecklistName = "Laptop",
                            NoteId = 1
                        }, new Checklist{
                            ChecklistId = 2,
                            ChecklistName = "Bike",
                            NoteId = 1
                        }
                    },
                    NoteLabel = new List<Label>{
                        new Label{
                            LabelId = 1,
                            LabelName = "Casual",
                            NoteId = 1
                        }
                    }
                },
                new GingerNoteC{
                    NoteId = 2,
                    NoteTitle = "Courses",
                    NoteChecklist = new List<Checklist>{
                        new Checklist{
                            ChecklistId = 3,
                            ChecklistName = "Bootstrap",
                            NoteId = 2
                        }
                    },
                    NoteLabel = new List<Label>{
                        new Label{
                            LabelId = 2,
                            LabelName = ".Net",
                            NoteId = 2
                        }, new Label{
                            LabelId = 3,
                            LabelName = "Casual",
                            NoteId = 2
                        }
                    }
                }
            };
        }
        [Fact]
        public void GetAllNote_PositiveTest(){
            List<GingerNoteC> dummy = DummyMock();  // Arrange
            
            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>(); // Removing Dependency
            MockRepository.Setup(d => d.GetAllNote()).Returns(dummy);
            
            GingerController gingerontroller = new GingerController(MockRepository.Object); // Act
            var actual = gingerontroller.Get();
            
            Assert.NotNull(actual); // Assert
            Assert.Equal(2 , dummy.Count);
        }
        [Fact]
        public void GetAllNote_NegativeTest(){
            // List<GingerNoteC> dummy = DummyMock();  // Arrange
            
            // Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>(); // Removing Dependency
            // MockRepository.Setup(d => d.GetAllNote()).Returns(dummy);
            
            // GingerController gingerontroller = new GingerController(MockRepository.Object); // Act
            // var actual = gingerontroller.Get();
            
            // Assert.NotNull(actual); // Assert
            // Assert.Equal(2 , dummy.Count);
        }
        [Fact]
        public void GetNoteById_PositiveTest(){
            List<GingerNoteC> dummy = DummyMock();
            int NoteId = 1;

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();
            MockRepository.Setup(d => d.GetNote(NoteId)).Returns(dummy.FirstOrDefault( d => d.NoteId == NoteId ));

            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual  = gingerontroller.Get(NoteId);

            var okObjectResult = actual as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as Models.GingerNoteC;
            Assert.NotNull(model);

            Assert.Equal( dummy.FirstOrDefault( d => d.NoteId == NoteId ).NoteId , model.NoteId);
        }
        [Fact]
        public void GetNote_NegativeTest(){
            // List<GingerNoteC> dummy = DummyMock();
            // int NoteId = 3;

            // var MockRepository = new Mock<IGingerNoteRepo>();
            
            // MockRepository.Setup(d => d.GetNote(NoteId)).Returns(dummy.Find(n => n.NoteId == NoteId));
            // GingerController gingerontroller = new GingerController(MockRepository.Object);
            // var actual = gingerontroller.Get(NoteId);
            // Assert.Null(actual);
        }
        [Fact]
        public void GetNoteByTitle_PositiveTest(){
            List<GingerNoteC> dummy = DummyMock();
            string notetitle = "WishList";

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();
            MockRepository.Setup(d => d.GetNoteByTitle(notetitle,"title"))
                            .Returns(dummy.Where( d => d.NoteTitle == notetitle ).ToList());

            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual  = gingerontroller.Get(notetitle,"title");

            var okObjectResult = actual as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as Models.GingerNoteC;
            Assert.NotNull(model);

            Assert.Equal( dummy.FirstOrDefault( d => d.NoteTitle == notetitle ).NoteTitle , model.NoteTitle);
        }
        [Fact]
        public void GetNoteByLabel_PositiveTest(){
            List<GingerNoteC> dummy = DummyMock();
            string notetitle = "WishList";

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();
            MockRepository.Setup(d => d.GetNoteByTitle(notetitle,"label"))
                            .Returns(dummy.Where( d => d.NoteTitle == notetitle ).ToList());

            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual  = gingerontroller.Get(notetitle,"label");

            var okObjectResult = actual as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as Models.GingerNoteC;
            Assert.NotNull(model);

            Assert.Equal( dummy.FirstOrDefault( d => d.NoteTitle == notetitle ).NoteTitle , model.NoteTitle);
        }
        [Fact]
        public void GetNotePinned_PositiveTest(){
            List<GingerNoteC> dummy = DummyMock();
            string notetitle = "WishList";

            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();
            MockRepository.Setup(d => d.GetNoteByTitle(notetitle,"pinned"))
                            .Returns(dummy.Where( d => d.NoteTitle == notetitle ).ToList());

            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual  = gingerontroller.Get(notetitle,"pinned");

            var okObjectResult = actual as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as Models.GingerNoteC;
            Assert.NotNull(model);

            Assert.Equal( dummy.FirstOrDefault( d => d.NoteTitle == notetitle ).NoteTitle , model.NoteTitle);
        }
    }
}
