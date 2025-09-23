using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapifirst.Models;
using webapifirst.Repository;
using webapifirst.Service;

namespace FoodDeliveryApp.Test.Services
{
    public class CategoryServiceUnitTestMoq
    {
        private readonly Mock<ICategoryRepository> _repository;
        private readonly ICategoryService _categoryService;
        List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    CategoryId = "123",
                    Name = "Makanan",
                    dlt = 0
                },
                new Category()
                {
                    CategoryId = "124",
                    Name = "Minuman",
                    dlt = 0
                },
                new Category()
                {
                    CategoryId = "125",
                    Name = "Makanan Ringan",
                    dlt = 0
                }
            };
        public CategoryServiceUnitTestMoq() 
        {
            _repository = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_repository.Object);
        }

        [Fact]
        public void Get_AllCategoriesData_ReturnAllCategory()
        {
            _repository.Setup(s => s.Get()).Returns(categories);

            var result = _categoryService.Get();

            var firstProduct = result.First();

            Assert.NotNull(result);
            Assert.Equal(0, firstProduct.dlt);
        }

        [Fact]
        public void GetById_OneCategoryData_ReturnOneCategory()
        {
            _repository.Setup(s => s.GetById(It.IsAny<string>())).Returns((string id) => categories.FirstOrDefault(x => x.CategoryId == id));

            var result = _categoryService.GetById("123");

            Assert.NotNull(result);
            Assert.Equal("123", result.CategoryId);
            Assert.Equal("Makanan", result.Name);
            Assert.Equal(0, result.dlt);

        }
    }
}
