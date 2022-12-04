using System;

namespace Coffee_Utilidades.Core.Entity.Request
{
    public class InserirProduto
    {
        public float Preco { get; set; }
        public string Nm_Produto { get; set; }
        public string Descricao_Produto { get; set; }
        public string Nm_Imagem { get; set; }
        public string Categoria { get; set; }        
        public DateTime DTCADASTRO { get; set; }
    }
}
