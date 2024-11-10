using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock;
public record class GoodsKindSpecificDataDto
    (
    // Accessory
    string? Color, string? Size,
    // Sheet music edition
    string? Author,
    // Musical instrument
    int? ReleaseYear, ManufacturerType? ManufacturerType, string? Manufacturer
    );