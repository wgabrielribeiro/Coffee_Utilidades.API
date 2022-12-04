using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee_Utilidades.DataModel.Model
{
    [Table("ProdutosCadastrados")]
    public class ProdutosCadastrados
    {
        [Key]
        public int Cod_Produto { get; set; }
        public decimal Preco { get; set; }
        public string Nm_Produto { get; set; }
        public string Descricao_Produto { get; set; }
        public string Nm_Imagem { get; set; }
        public string Categoria { get; set; }
        public DateTime DTCADASTRO { get; set; }
    }
}
