using Microsoft.EntityFrameworkCore;

namespace apifilmes.Database
{
    public class DiretorDatabase
    {
        Models.ApiDbContext ctx = new Models.ApiDbContext();

        public Models.TbDiretor Salvar(Models.TbDiretor diretor)
        {
            ctx.TbDiretors.Add(diretor);
            ctx.SaveChanges();

            return diretor;
        }


        public List<Models.TbDiretor> Listar()
        {
            List<Models.TbDiretor> diretors = ctx.TbDiretors.Include(x => x.IdFilmeNavigation).ToList();
            return diretors;
        }
    
    
        public void Alterar(Models.TbDiretor diretor)
        {
            Models.TbDiretor atual = ctx.TbDiretors.First(x => x.IdDiretor == diretor.IdDiretor);
            atual.NmDiretor = diretor.NmDiretor;
            atual.DtNascimento = diretor.DtNascimento;
            atual.IdFilme = diretor.IdFilme;

            ctx.SaveChanges();
        }
    

        public void Deletar(Models.TbDiretor diretor)
        {
            Models.TbDiretor atual = ctx.TbDiretors.First(x => x.IdDiretor == diretor.IdDiretor);

            ctx.Remove(atual);
            ctx.SaveChanges();
        }
    
    }
}