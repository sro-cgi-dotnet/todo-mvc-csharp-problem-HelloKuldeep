using System;
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
                        }
                    },
                    NoteLabel = new List<Label>{
                        new Label{
                            LabelId = 1,
                            LabelName = "Casual",
                            NoteId = 2
                        }
                    }
                },
                new GingerNoteC{
                    NoteId = 2,
                    NoteTitle = "Courses",
                    NoteChecklist = new List<Checklist>{
                        new Checklist{
                            ChecklistId = 2,
                            ChecklistName = "Bootstrap",
                            NoteId = 2
                        }
                    },
                    NoteLabel = new List<Label>{
                        new Label{
                            LabelId = 2,
                            LabelName = ".Net",
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
        public void GetNote_PositiveTest(){
            List<GingerNoteC> dummy = DummyMock();
            Mock<IGingerNoteRepo> MockRepository = new Mock<IGingerNoteRepo>();
            MockRepository.Setup(d => d.GetNote(1)).Returns(dummy);
            GingerController gingerontroller = new GingerController(MockRepository.Object);
            var actual = gingerontroller.Get(1);
            Assert.NotNull(actual);
            Assert.Equal(1 , dummy.Count);
        }
    }
}
