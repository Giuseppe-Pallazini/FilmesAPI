using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apifilmes.Models;

[Table("tb_filme")]
public partial class TbFilme
{
    [Key]
    [Column("id_filme")]
    public int IdFilme { get; set; }


    [Column("nm_filme")]
    [StringLength(100)]
    public string? NmFilme { get; set; }


    [Column("ds_genero")]
    [StringLength(100)]
    public string? DsGenero { get; set; }


    [Column("nr_duracao")]
    public int? NrDuracao { get; set; }


    [Column("vl_avaliacao")]
    [Precision(10, 2)]
    public decimal? VlAvaliacao { get; set; }


    [Column("bt_disponivel")]
    public bool? BtDisponivel { get; set; }


    [Column("dt_lancamento")]
    public DateOnly? DtLancamento { get; set; }



    [InverseProperty("IdFilmeNavigation")]
    public virtual ICollection<TbDiretor> TbDiretors { get; set; } = new List<TbDiretor>();



    [InverseProperty("IdFilmeNavigation")]
    public virtual ICollection<TbFilmeAtor> TbFilmeAtors { get; set; } = new List<TbFilmeAtor>();
}
