using ApiInventario.DTOs;
using ApiInventario.Input;

namespace ApiInventario.Services.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAll();
        //Task<ProductDTO> Get(Guid Id);
        //Task<IEnumerable<ProductDTO>> GetByFilter(InputGetByFilter InputFilter);
        Task<ProductDTO> Update(ProductDTO dto);
        Task<ProductDTO> Insert(ProductToAddInput dto);
        Task<bool> Delete(int id);
        List<string> errors { get; set; }
        Task<bool> UpdateCost(int category, decimal cost);
        Task<bool> UpdateStockByUpdatedOrder(List<ProductToInsertInput> products);
    }
}
