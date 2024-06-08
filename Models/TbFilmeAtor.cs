using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apifilmes.Models;

[Table("tb_filme_ator")]
[Index("IdAtor", Name = "id_ator")]
[Index("IdFilme", Name = "id_filme")]
public partial class TbFilmeAtor
{
    [Key]
    [Column("id_filme_ator")]
    public int IdFilmeAtor { get; set; }

    [Column("nm_personagem")]
    [StringLength(100)]
    public string? NmPersonagem { get; set; }

    [Column("id_filme")]
    public int? IdFilme { get; set; }

    [Column("id_ator")]
    public int? IdAtor { get; set; }

    [ForeignKey("IdAtor")]
    [InverseProperty("TbFilmeAtors")]
    public virtual TbAtor? IdAtorNavigation { get; set; }

    [ForeignKey("IdFilme")]
    [InverseProperty("TbFilmeAtors")]
    public virtual TbFilme? IdFilmeNavigation { get; set; }
}
