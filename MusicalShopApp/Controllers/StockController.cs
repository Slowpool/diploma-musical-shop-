using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.StockServices;

namespace MusicalShopApp.Controllers
{
    public class StockController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GoodsAddingInWarehouse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GoodsAddingInWarehouse([FromServices] IFindSpecificTypeService findSpecificTypeService, [FromServices] IAddNewGoodsService addNewGoodsService, string goodsName, KindOfGoods kindOfGoods, string specificType, int price, GoodsStatus status, string description, int numberOfUnits)
        {
            try
            {
                var specificTypeEntity = await findSpecificTypeService.FindSpecificType(specificType);
                await addNewGoodsService.AddNewGoods(goodsName, kindOfGoods, specificTypeEntity, price, status, description, numberOfUnits);
            }
            catch
            {

            }
            return View();
        }
    }
}
