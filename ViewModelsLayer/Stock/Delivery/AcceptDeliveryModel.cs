using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock.Delivery;
public record AcceptDeliveryModel(Guid DeliveryId, bool Confirmation);