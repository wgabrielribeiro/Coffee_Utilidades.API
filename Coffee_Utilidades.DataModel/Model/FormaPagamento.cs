using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee_Utilidades.DataModel.Model
{
    [Table("MetodoPagamento")]
    public class FormaPagamento
    {
        public bool isDinheiro { get; set; }
        public decimal qntTroco { get; set; }
        public string nmCartao { get; set; }
        [Key]
        public int numeroCartao { get; set; }
        public string validade { get; set; }
        public int cvv { get; set; }
        public string parcelamento { get; set; }
        public string usuario { get; set; }

    }
}
