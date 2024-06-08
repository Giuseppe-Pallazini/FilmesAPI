using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apifilmes.Models;

[Table("tb_diretor")]
[Index("IdFilme", Name = "id_filme")]
public partial class TbDiretor
{
    [Key]
    [Column("id_diretor")]
    public int IdDiretor { get; set; }

    [Column("nm_diretor")]
    [StringLength(100)]
    public string? NmDiretor { get; set; }

    [Column("dt_nascimento")]
    public DateOnly? DtNascimento { get; set; }

    [Column("id_filme")]
    public int? IdFilme { get; set; }

    [ForeignKey("IdFilme")]
    [InverseProperty("TbDiretors")]
    public virtual TbFilme? IdFilmeNavigation { get; set; }
}
