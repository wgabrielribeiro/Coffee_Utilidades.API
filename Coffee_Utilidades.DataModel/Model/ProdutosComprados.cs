using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Utilidades.DataModel.Model
{
    [Table("ProdutosComprados")]
    public class ProdutosComprados
    {
        [Key]
        public int id { get; set; }
        public int Cod_Produto { get; set; }
        public int QntSelecionado { get; set; }
        public decimal Preco { get; set; }
        public int vlTotal { get; set; }
        public string Nm_Produto { get; set; }
        public string Nm_Imagem { get; set; }
        public string Categoria { get; set; }
        public string usuario { get; set; }
    }
}
