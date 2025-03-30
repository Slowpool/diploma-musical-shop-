
namespace ViewModelsLayer.Sales;

public record SaleReturnModel(Guid SaleId, bool CustomerConfirmation, bool RefundConfirmation) : ISaleModel;