using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapifirst.Controllers;
using webapifirst.Models;
using webapifirst.Repository;
using webapifirst.Service;

namespace FoodDeliveryApp.Test.Services
{
    public class ProductServiceUnitTestWithMoq
    {
        private readonly Mock<IProductRepository> _productRepositoryMoq;
        private readonly IProductService _productService;
        public ProductServiceUnitTestWithMoq()
        {
            _productRepositoryMoq = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMoq.Object);
        }

        [Fact]
        public void Get_AllProductData_ReturnAllProductData()
        {
            var product = new List<ProductDTO>()
            {
                new ProductDTO
                {
                    ProductName = "Es Kepal Milo",
                    ProductId = "8983419-jdsiui-1288912-ijdsj",
                    ProductDescription = "Es kepal milo toping es krim",
                    ProductPrice = 12000,
                    ProductCount = 3,
                    CategoryName = "Minuman",
                },
            };

            _productRepositoryMoq.Setup(p => p.Get()).Returns(product);

            var result = _productService.Get();

            Assert.NotNull(result);
            var firstProduct = result.First();
            Assert.Equal("Es Kepal Milo", firstProduct.ProductName);
            Assert.Equal("Minuman", firstProduct.CategoryName);
            Assert.Equal(12000, firstProduct.ProductPrice);
            Assert.Equal(3, firstProduct.ProductCount);
        }

        [Fact]
        public void Get_ProductById_ReturnProductById()
        {
            var product = new List<ProductDTO>
            {
                new ProductDTO()
                {
                    ProductId = "123",
                    ProductName = "Es Kepal Milo",
                    ProductDescription = "Es kepal milo toping es krim",
                    ProductPrice = 12000,
                    ProductCount = 3,
                    CategoryName = "Minuman",
                },
                new ProductDTO()
                {
                    ProductId = "124",
                    ProductName = "Ayam Taliwang",
                    ProductDescription = "Ayam taliwang extra cabe pedas",
                    ProductPrice = 17000,
                    ProductCount = 2,
                    CategoryName = "Makanan Berat",
                }
            };

            _productRepositoryMoq.Setup(p => p.GetById("123")).Returns(product.First(p => p.ProductId == "123"));

            var result = _productService.GetById("123");

            Assert.NotNull(result);
            Assert.Equal("123", result.ProductId);
            Assert.Equal("Es Kepal Milo", result.ProductName);
            Assert.Equal("Minuman", result.CategoryName);
            Assert.Equal(12000, result.ProductPrice);
            Assert.Equal(3, result.ProductCount);
        }

        [Fact]
        public void Add_SaveProduct() 
        {
            var product = new ProductDTO()
            {
                ProductName = "Es Kepal Milo",
                ProductDescription = "Es kepal milo toping es krim",
                ProductPrice = 12000,
                ProductCount = 3,
                CategoryId = "123",
            };

            _productRepositoryMoq.Setup(p => p.Add(It.IsAny<Product>()));

            _productService.Add(product);

            _productRepositoryMoq.Verify(p => p.Add(It.IsAny<Product>()), Times.Once);
        }
        [Fact]
        public void Delete_DeleteProduct_ReturnProductDelete()
        {
            var product = new Product()
            {
                ProductId = "123",
                ProductName = "Es Kepal Milo",
                ProductDescription = "Es kepal milo toping es krim",
                ProductPrice = 12000,
                ProductCount = 3,
                CategoryId = "123",
            };

            _productRepositoryMoq.Setup(p => p.Delete(It.IsAny<string>())).Returns(product);

            var result = _productService.Delete("123");

            Assert.NotNull(result);
            Assert.Equal("123", result.ProductId);
            Assert.Equal("Es Kepal Milo", result.ProductName);
            Assert.Equal(12000, result.ProductPrice);
        }
    }
}
