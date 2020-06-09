using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SharpLibrary.Areas.Admin.Controllers;
using SharpLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SharpLibrary.Tests
{
    public class GenreControllerTests
    {
        [Fact]
        public void IndexContainsAllGenres()
        {
            Mock<IGenreRepository> mock = new Mock<IGenreRepository>();
            mock.Setup(elm => elm.Genres)
                .Returns((new Genre[]
                {
                    new Genre {Id = 1, Name = "������������ ����������", Description = "�������� ����� �����!"},
                    new Genre {Id = 2, Name = "���������� ��������", Description = "������ ���������� ����������"},
                    new Genre {Id = 3, Name = "�������", Description = "�� ��� �� ��� ���)"}
                }).AsQueryable());

            GenreController target = new GenreController(mock.Object);

            Genre[] result = GetViewModel<IEnumerable<Genre>>(target.Index())?.ToArray();

            Assert.Equal(3, result.Length);
            Assert.Equal("������������ ����������", result[0].Name);
            Assert.Equal("������ ���������� ����������", result[1].Description);
            Assert.Equal(3, result[2].Id);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        [Fact]
        public void CanEditGenre()
        {
            Mock<IGenreRepository> mock = new Mock<IGenreRepository>();
            mock.Setup(elm => elm.Genres)
                .Returns((new Genre[]
                {
                    new Genre {Id = 1, Name = "������������ ����������", Description = "�������� ����� �����!"},
                    new Genre {Id = 2, Name = "���������� ��������", Description = "������ ���������� ����������"},
                    new Genre {Id = 3, Name = "�������", Description = "�� ��� �� ��� ���)"}
                }).AsQueryable());

            GenreController target = new GenreController(mock.Object);

            Genre g1 = GetViewModel<Genre>(target.Edit(1));
            Genre g2 = GetViewModel<Genre>(target.Edit(2));
            Genre g3 = GetViewModel<Genre>(target.Edit(3));

            Assert.Equal(1, g1.Id);
            Assert.Equal(2, g2.Id);
            Assert.Equal(3, g3.Id);
        }

        [Fact]
        public void CannotEditNonexistentGenre()
        {
            Mock<IGenreRepository> mock = new Mock<IGenreRepository>();
            mock.Setup(elm => elm.Genres)
                .Returns((new Genre[]
                {
                    new Genre {Id = 1, Name = "������������ ����������", Description = "�������� ����� �����!"},
                    new Genre {Id = 2, Name = "���������� ��������", Description = "������ ���������� ����������"},
                    new Genre {Id = 3, Name = "�������", Description = "�� ��� �� ��� ���)"}
                }).AsQueryable());

            GenreController target = new GenreController(mock.Object);

            Genre g = GetViewModel<Genre>(target.Edit(4));

            Assert.Null(g);
        }

        [Fact]
        public void CanSaveValidChanges()
        {
            Mock<IGenreRepository> mock = new Mock<IGenreRepository>();
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();

            GenreController target = new GenreController(mock.Object)
            {
                TempData = tempData.Object
            };

            Genre genre = new Genre { Name = "�������� ����" };

            IActionResult result = target.Edit(genre);

            mock.Verify(elm => elm.SaveGenre(genre));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void CannotSaveInvalidChanges()
        {
            Mock<IGenreRepository> mock = new Mock<IGenreRepository>();

            GenreController target = new GenreController(mock.Object);

            Genre genre = new Genre { Name = "�������� ����" };

            target.ModelState.AddModelError("error", "error");

            IActionResult result = target.Edit(genre);

            mock.Verify(elm => elm.SaveGenre(It.IsAny<Genre>()), Times.Never);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CanDeleteValidGenres()
        {
            Genre genre = new Genre { Id = 2, Name = "�������� ����" };

            Mock<IGenreRepository> mock = new Mock<IGenreRepository>();
            mock.Setup(elm => elm.Genres)
                .Returns((new Genre[]
                {
                    new Genre {Id = 1, Name = "������������ ����������"},
                    genre,
                    new Genre {Id = 3, Name = "���������� ����������"}
                }).AsQueryable());

            GenreController target = new GenreController(mock.Object);

            target.Delete(genre.Id);

            mock.Verify(elm => elm.DeleteGenre(genre.Id));
        }
    }
}
