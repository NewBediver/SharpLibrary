using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SharpLibrary.Areas.Admin.Controllers;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;
using System.Linq;
using Xunit;

namespace SharpLibrary.Tests.AdminArea
{
    public class GenreControllerTests
    {
        [Fact]
        public void CanPaginate()
        {
            Mock<IGenreRepository> mock = new Mock<IGenreRepository>();
            mock.Setup(elm => elm.Genres)
                .Returns((new Genre[]
                {
                    new Genre {Id = 1, Name = "Классическая литература", Description = "Классику нужно знать!"},
                    new Genre {Id = 2, Name = "Зарубежная классика", Description = "Лучшая зарубежная литература"},
                    new Genre {Id = 3, Name = "Комиксы", Description = "Ну как же без них)"},
                    new Genre {Id = 4, Name = "Детективы", Description = "Если хочется поломать голову"},
                    new Genre {Id = 5, Name = "Научная фантастика", Description = "Невероятные и захватывающие миры ждут вас"},
                }).AsQueryable());

            GenreController target = new GenreController(mock.Object)
            {
                PageSize = 3
            };
            Genre[] result = GetViewModel<ListViewModel<Genre>>(target.Index(2))?.Entities.ToArray();

            Assert.True(result.Length == 2);
            Assert.Equal("Детективы", result[0].Name);
            Assert.Equal("Невероятные и захватывающие миры ждут вас", result[1].Description);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        [Fact]
        public void CanSendPaginationViewModel()
        {
            Mock<IGenreRepository> mock = new Mock<IGenreRepository>();
            mock.Setup(elm => elm.Genres)
                .Returns((new Genre[]
                {
                    new Genre {Id = 1, Name = "Классическая литература", Description = "Классику нужно знать!"},
                    new Genre {Id = 2, Name = "Зарубежная классика", Description = "Лучшая зарубежная литература"},
                    new Genre {Id = 3, Name = "Комиксы", Description = "Ну как же без них)"},
                    new Genre {Id = 4, Name = "Детективы", Description = "Если хочется поломать голову"},
                    new Genre {Id = 5, Name = "Научная фантастика", Description = "Невероятные и захватывающие миры ждут вас"},
                }).AsQueryable());

            GenreController target = new GenreController(mock.Object)
            {
                PageSize = 3
            };
            PagingInfo result = GetViewModel<ListViewModel<Genre>>(target.Index(2))?.PagingInfo;

            Assert.Equal(2, result.CurrentPage);
            Assert.Equal(3, result.ItemsPerPage);
            Assert.Equal(5, result.TotalItems);
            Assert.Equal(2, result.TotalPages);
        }

        [Fact]
        public void IndexContainsAllGenres()
        {
            Mock<IGenreRepository> mock = new Mock<IGenreRepository>();
            mock.Setup(elm => elm.Genres)
                .Returns((new Genre[]
                {
                    new Genre {Id = 1, Name = "Классическая литература", Description = "Классику нужно знать!"},
                    new Genre {Id = 2, Name = "Зарубежная классика", Description = "Лучшая зарубежная литература"},
                    new Genre {Id = 3, Name = "Комиксы", Description = "Ну как же без них)"}
                }).AsQueryable());

            GenreController target = new GenreController(mock.Object);
            Genre[] result = GetViewModel<ListViewModel<Genre>>(target.Index())?.Entities.ToArray();

            Assert.Equal(3, result.Length);
            Assert.Equal("Классическая литература", result[0].Name);
            Assert.Equal("Лучшая зарубежная литература", result[1].Description);
            Assert.Equal(3, result[2].Id);
        }

        [Fact]
        public void CanEditGenre()
        {
            Mock<IGenreRepository> mock = new Mock<IGenreRepository>();
            mock.Setup(elm => elm.Genres)
                .Returns((new Genre[]
                {
                    new Genre {Id = 1, Name = "Классическая литература", Description = "Классику нужно знать!"},
                    new Genre {Id = 2, Name = "Зарубежная классика", Description = "Лучшая зарубежная литература"},
                    new Genre {Id = 3, Name = "Комиксы", Description = "Ну как же без них)"}
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
                    new Genre {Id = 1, Name = "Классическая литература", Description = "Классику нужно знать!"},
                    new Genre {Id = 2, Name = "Зарубежная классика", Description = "Лучшая зарубежная литература"},
                    new Genre {Id = 3, Name = "Комиксы", Description = "Ну как же без них)"}
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

            Genre genre = new Genre { Name = "Тестовый жанр" };

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

            Genre genre = new Genre { Name = "Тестовый жанр" };

            target.ModelState.AddModelError("error", "error");

            IActionResult result = target.Edit(genre);

            mock.Verify(elm => elm.SaveGenre(It.IsAny<Genre>()), Times.Never);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CanDeleteValidGenres()
        {
            Genre genre = new Genre { Id = 2, Name = "Тестовый жанр" };

            Mock<IGenreRepository> mock = new Mock<IGenreRepository>();
            mock.Setup(elm => elm.Genres)
                .Returns((new Genre[]
                {
                    new Genre {Id = 1, Name = "Классическая литература"},
                    genre,
                    new Genre {Id = 3, Name = "Зарубежная литература"}
                }).AsQueryable());

            GenreController target = new GenreController(mock.Object);

            target.Delete(genre.Id);

            mock.Verify(elm => elm.DeleteGenre(genre.Id));
        }
    }
}
