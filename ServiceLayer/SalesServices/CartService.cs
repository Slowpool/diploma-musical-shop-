using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.SalesServices;

public interface ICartService
{
    Task<string> MoveGoodsBackToCart(Guid saleId);
}
public class CartService(ISaleManagementService saleService) : ICartService
{
    public async Task<string> MoveGoodsBackToCart(Guid saleId)
    {
        saleService.
    }
}
