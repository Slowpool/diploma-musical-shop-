using DataLayer.NotMapped;
using DataLayer.SupportClasses;

namespace DataLayer.Models;
internal class Synthesizer : Goods
{
    public int SynthesizerId { get; set; }
    // > 0
    public int KeysNumber { get; set; }
}
