using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock;
public record class GoodsKindSpecificDataDto
    (
    // Accessory
    //[Required()]
    [MaxLength(100)]
    // TODO add this attribute via fluent api automatically to the every string property
    // UPD: probably ef core is helpless here
    // TODO to cyrillic
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Значение поля \"Цвет\" может содержать только буквы а-я, А-Я и цифры 0-9")]
    [StringLength(100, ErrorMessage = "Значение поля \"Цвет\" не может быть длиннее {1} символов")]
    string? Color,
    [StringLength(200)]
    string? Size,
    // Sheet music edition
    [StringLength(100)]
    string? Author,
    // Musical instrument
    // TODO dynamic validation like [Max(CurrentYear)] but there're exceptions, e.g. when the instrument will be released in next year, and shop already has ordered this instrument and its release year in the next year is really possible.
    int? ReleaseYear,
    ManufacturerType? ManufacturerType,
    [StringLength(100)]
    string? Manufacturer
    );