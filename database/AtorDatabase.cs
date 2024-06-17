namespace apifilmes.Database
{
    public class AtorDatabase
    {
        Models.ApiDbContext ctx = new Models.ApiDbContext();
        
        public Models.TbAtor Salvar(Models.TbAtor ator)
        {
            
            ctx.TbAtors.Add(ator);
            ctx.SaveChanges();

            return ator;
        }
    }
}