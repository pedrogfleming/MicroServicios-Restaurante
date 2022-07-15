using ApiInventario.Domain.Models;
using ApiInventario.DTOs;
using ApiInventario.Infrastructure;
using ApiInventario.Input;
using ApiInventario.Services.IService;
using LibreriaWinniePod;

namespace ApiInventario.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<string> errors { get; set; } = new();

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<ProductDTO> Update(ProductDTO dto)
        {
            try
            {
                _unitOfWork.Products.Update(MappeadorGenerico.Map<Product>(dto));
                _unitOfWork.Save();
                return Task.FromResult(dto);
            }
            catch (Exception)
            {
                this.errors.Add("Update couldn´t be done");
                return Task.FromResult(MappeadorGenerico.CreateEntityDTOWithError<ProductDTO>(errors));
            }
        }

        public Task<ProductDTO> Insert(ProductToAddInput dto)
        {
            try
            {
                var result = _unitOfWork.Products.Insert(MappeadorGenerico.Map<Product>(dto));
                _unitOfWork.Save();
                return Task.FromResult(MappeadorGenerico.Map<ProductDTO>(result));
            }
            catch (Exception)
            {
                var dtoWithError = MappeadorGenerico.Map<ProductDTO>(dto);
                dtoWithError.Errors.Add("Product not saved in database.");
                return Task.FromResult(dtoWithError);
            }
        }

        public Task<IEnumerable<ProductDTO>> GetAll()
        {
            var products = _unitOfWork.Products.GetAll();
            IEnumerable<ProductDTO> result = MappeadorGenerico.MapEntities<ProductDTO>(products);
            return Task.FromResult(result);
        }
        public async Task<bool> UpdateCost(int category, decimal cost)
        {
            try
            {
                var result = _unitOfWork.Products.GetAll()
                .Where(x => x.Category == category);
                foreach (var item in result)
                {
                    item.Cost = cost;
                    _unitOfWork.Products.Update(item);
                }
                var totalChangesCount = _unitOfWork.Save();
                return totalChangesCount == result.Count();
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Task<bool> Delete(int id)
        {
            try
            {
                var product =_unitOfWork.Products.GetById(id);
                product.IsAvailable = false;
                _unitOfWork.Products.Update(product);
                _unitOfWork.Save();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> UpdateStockByUpdatedOrder(List<ProductToInsertInput> products)
        {
            try
            {
                foreach (var productToInsert in products)
                {
                    var product = _unitOfWork.Products.GetById(productToInsert.Id);
                    product.Stock += productToInsert.Quantity;
                    _unitOfWork.Products.Update(product);
                }
                _unitOfWork.Save();
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
    }
}
