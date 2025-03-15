using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Sales.Dto;

public record CreateReservationDto(List<Goods> GoodsForReservation);