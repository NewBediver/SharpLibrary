using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SharpLibrary.Areas.Admin.Controllers;
using SharpLibrary.Models;
using SharpLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SharpLibrary.Tests.AdminArea
{
    public class AuthorControllerTests
    {
        [Fact]
        public void CanPaginate()
        {
            var mock = CreateAndSetupMock(9);
            AuthorController target = new AuthorController(mock.Object)
            {
                PageSize = 5
            };
            Author[] result = GetViewModel<ListViewModel<Author>>(target.Index(2))?.Entities.ToArray();

            Assert.True(result.Length == 4);
            Assert.Equal("Фамилия6", result[0].Surname);
            Assert.Equal("Имя7", result[1].Name);
            Assert.Equal("Отчество8", result[2].Patronymic);
            Assert.Equal("Имя9", result[3].Name);
        }

        private Mock<IAuthorRepository> CreateAndSetupMock(int size)
        {
            Mock<IAuthorRepository> res = new Mock<IAuthorRepository>();
            res.Setup(elm => elm.Authors)
                .Returns(CreateAuthors(size));
            return res;
        }

        private IQueryable<Author> CreateAuthors(int size)
        {
            List<Author> authors = new List<Author>();
            for (int i = 1; i <= size; ++i)
            {
                authors.Add(new Author
                {
                    Id = i,
                    Name = $"Имя{i}",
                    Surname = $"Фамилия{i}",
                    Patronymic = $"Отчество{i}",
                    Description = $"Описание{i}"
                });
            }
            return authors.AsQueryable();
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }

        [Fact]
        public void CanSendPaginationViewModel()
        {
            var mock = CreateAndSetupMock(10);
            AuthorController target = new AuthorController(mock.Object)
            {
                PageSize = 3
            };
            PagingInfo result = GetViewModel<ListViewModel<Author>>(target.Index(2))?.PagingInfo;

            Assert.Equal(2, result.CurrentPage);
            Assert.Equal(3, result.ItemsPerPage);
            Assert.Equal(10, result.TotalItems);
            Assert.Equal(4, result.TotalPages);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(13)]
        [InlineData(29)]
        [InlineData(103)]
        public void IndexContainsAllAuthors(int size)
        {
            var mock = CreateAndSetupMock(size);
            AuthorController target = new AuthorController(mock.Object)
            {
                PageSize = size + 1
            };
            Author[] result = GetViewModel<ListViewModel<Author>>(target.Index(1))?.Entities.ToArray();

            Assert.Equal(size, result.Length);
            for (int i = 0; i < size; ++i)
            {
                Assert.Equal(i+1, result[i].Id);
                Assert.Equal($"Имя{i+1}", result[i].Name);
                Assert.Equal($"Фамилия{i+1}", result[i].Surname);
                Assert.Equal($"Отчество{i+1}", result[i].Patronymic);
                Assert.Equal($"Описание{i+1}", result[i].Description);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(13)]
        [InlineData(29)]
        [InlineData(103)]
        public void CanEditAuthor(int size)
        {
            var mock = CreateAndSetupMock(size);
            AuthorController target = new AuthorController(mock.Object);

            for (int i = 0; i < size; ++i)
            {
                Author result = GetViewModel<Author>(target.Edit(i+1));
                Assert.Equal(i + 1, result.Id);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(13)]
        [InlineData(29)]
        [InlineData(103)]
        public void CannotEditNonexistentAuthor(int size)
        {
            var mock = CreateAndSetupMock(size);
            AuthorController target = new AuthorController(mock.Object);

            int temp = new Random().Next(101);

            for (int i = 0; i < temp; ++i)
            {
                int index = new Random().Next(100);
                if (i % 2 == 0 || index <= size) index *= -1;
                Author result = GetViewModel<Author>(target.Edit(index));
                Assert.Null(result);
            }
        }

        [Fact]
        public void CanSaveValidChanges()
        {
            Mock<IAuthorRepository> mock = new Mock<IAuthorRepository>();
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();

            AuthorController target = new AuthorController(mock.Object)
            {
                TempData = tempData.Object
            };

            Author author = new Author { Name = "Имя1" };

            IActionResult result = target.Edit(author);

            mock.Verify(elm => elm.SaveAuthor(author));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void CannotSaveInvalidChanges()
        {
            Mock<IAuthorRepository> mock = new Mock<IAuthorRepository>();

            AuthorController target = new AuthorController(mock.Object);

            Author author = new Author { Name = "Имя1" };

            target.ModelState.AddModelError("error", "error");

            IActionResult result = target.Edit(author);

            mock.Verify(elm => elm.SaveAuthor(It.IsAny<Author>()), Times.Never);
            Assert.IsType<ViewResult>(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(13)]
        [InlineData(29)]
        [InlineData(103)]
        public void CanDeleteValidAuthor(int size)
        {
            var mock = CreateAndSetupMock(size);
            AuthorController target = new AuthorController(mock.Object);

            for (int i = 1; i <= size; ++i)
            {
                target.Delete(i);
                mock.Verify(elm => elm.DeleteAuthor(i));
            }
        }
    }
}
