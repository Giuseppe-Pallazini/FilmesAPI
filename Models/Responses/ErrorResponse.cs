namespace apifilmes.Models.Responses
{
    public class ErrorResponse
    {

        public ErrorResponse(Exception ex, int codigo)
        {
            Codigo = codigo;
            Erro = ex.Message;
        }


        public int Codigo { get; set; }
        public string Erro { get; set; }
    }
}