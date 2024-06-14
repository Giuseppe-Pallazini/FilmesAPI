using System;

namespace apifilmes.Models.Request
{
    public class FilmeAtorJuntosRequest : FilmeAtorRequest
    {
        public string? NmFIlme { get; set; }
        public string? DsGenero { get; set; }
        public int NrDuracao { get; set; }
        public decimal VlAvaliacao { get; set; }
        public bool BtDisponivel { get; set; }
        public DateOnly DtLancamento { get; set; }
    }
}