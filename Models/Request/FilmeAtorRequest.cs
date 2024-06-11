using System;

namespace apifilmes.Models.Request
{
    public class FilmeAtorRequest
    {
        public int IdFilme { get; set; }

        public List<FilmeAtorItemRequest> Atores { get; set; }
    }
}