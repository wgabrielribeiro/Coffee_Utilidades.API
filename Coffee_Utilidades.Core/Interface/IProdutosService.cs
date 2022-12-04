using Coffee_Utilidades.Core.Entity.Request;
using Coffee_Utilidades.DataModel.Model;
using System;
using System.Threading.Tasks;

namespace Coffee_Utilidades.Core.Interface
{
    public interface IProdutosService
    {
        Task<ProdutosCadastrados> AddProduto(ProdutosCadastrados produto);
        //Task<ProdutosCadastrados> UpdateProduto(int Cd_Produto, ProdutosCadastrados produto);
        //Task<ProdutosCadastrados[]> GetProdutos();
        Task<ProdutosCadastrados> GetProdutosByCodigo(int Cd_Produto);

    }
}
