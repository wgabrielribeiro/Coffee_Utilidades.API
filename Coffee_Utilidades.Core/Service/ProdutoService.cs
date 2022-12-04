using Coffee_Utilidades.Core.Entity.Request;
using Coffee_Utilidades.Core.Interface;
using Coffee_Utilidades.DataModel;
using Coffee_Utilidades.DataModel.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coffee_Utilidades.Core.Service
{
    public class ProdutoService
    {
        private readonly DataContext _dataContext;

        public ProdutoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ProdutosCadastrados> AddProduto(ProdutosCadastrados inserir)
        {
            try
            {
                var produto = _dataContext.ProdutosCadastrados.Where(prod => prod.Nm_Produto == inserir.Nm_Produto).FirstOrDefault();

                if (produto != null)
                {
                    return produto;
                }

                _dataContext.Add(inserir);
                await _dataContext.SaveChangesAsync();

                var result = (from id in _dataContext.ProdutosCadastrados
                              orderby id.Cod_Produto descending
                              select id).First();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar no banco: {ex.Message}");
            }
        }

        public object GetProdutosByCodigo(string cd_Produto)
        {
            try
            {
                var produto = _dataContext.ProdutosCadastrados.Where(id => id.Categoria == cd_Produto).ToList();

                if (produto == null) return null;

                return produto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetProdutos()
        {
            try
            {
                var produtos = _dataContext.ProdutosCadastrados.ToList();

                if (produtos.Count == 0)
                {
                    return "Nenhum registro encontrado.";
                }

                return produtos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> SelecionaProduto(ProdutosSelecionados selecionado)
        {
            try
            {
                if (selecionado.QntSelecionado == 0)
                {
                    selecionado.QntSelecionado = 1;
                    selecionado.vlTotal = Convert.ToInt32(selecionado.Preco);
                }

                var produto = _dataContext.ProdutosSelecionados
                    .Where(prod => prod.Nm_Produto == selecionado.Nm_Produto && prod.Cod_Produto == selecionado.Cod_Produto)
                    .FirstOrDefault();

                if (produto != null)
                {
                    int count = produto.QntSelecionado;
                    produto.QntSelecionado = 1 + count;

                    int total = produto.QntSelecionado * Convert.ToInt32(produto.Preco);

                    produto.vlTotal = total;

                    await _dataContext.SaveChangesAsync();

                    return "Inserido com sucesso";
                }

                _dataContext.Add(selecionado);
                await _dataContext.SaveChangesAsync();

                return "Inserido com sucesso";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeteleProduto(int Cod_Produto, string usuario)
        {
            try
            {
                var existeProduto = _dataContext.ProdutosSelecionados
                    .Where(prod => prod.Cod_Produto == Cod_Produto && prod.usuario == usuario)
                    .FirstOrDefault();

                if (existeProduto == null)
                {
                    return "O registro para delete não foi encontrado!";
                }
                else if (existeProduto.QntSelecionado > 1)
                {
                    existeProduto.QntSelecionado = existeProduto.QntSelecionado - 1;
                    await _dataContext.SaveChangesAsync();

                    return "Deletado com sucesso";
                }

                _dataContext.ProdutosSelecionados.Remove(existeProduto);
                await _dataContext.SaveChangesAsync();

                return "Deletado com sucesso";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> ObterProdSelecionado(string usuario, string categoria)
        {
            try
            {
                var existeProduto = _dataContext.ProdutosSelecionados
                    .Where(prod => prod.usuario == usuario && prod.Categoria == categoria)
                    .ToList();

                if (existeProduto == null)
                {
                    return null;
                }

                return existeProduto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetAll(string usuario)
        {
            try
            {
                var existeProduto = _dataContext.ProdutosSelecionados
                    .Where(prod => prod.usuario == usuario)
                    .ToList();

                if (existeProduto == null)
                {
                    return null;
                }

                return existeProduto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CalculoCarrinho(string user)
        {
            try
            {
                var result = (from vt in _dataContext.ProdutosSelecionados
                              where vt.usuario == user
                              select vt.vlTotal);

                if (result.Count() > 0)
                {
                    return result.Sum();
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsereFormaDePagamento(FormaPagamento formaDePagamento)
        {
            try
            {
                var existeCartao = _dataContext.FormaPagamento.Where(numero => numero.numeroCartao == formaDePagamento.numeroCartao).FirstOrDefault();

                if (formaDePagamento.isDinheiro == true && existeCartao != null)
                {
                    var ultimoresult = (from id in _dataContext.FormaPagamento
                                         orderby id.numeroCartao descending
                                         select id).First();

                    formaDePagamento.numeroCartao = 1 + ultimoresult.numeroCartao;
                }

                if (existeCartao != null && formaDePagamento.isDinheiro == false)
                {
                    return false;
                }

                _dataContext.Add(formaDePagamento);
                await _dataContext.SaveChangesAsync();


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> HistoricoPedidos(ProdutosComprados[] comprados)
        {
            try
            {
                for (int i = 0; i < comprados.Length; i++)
                {
                    _dataContext.Add(comprados[i]);
                    await _dataContext.SaveChangesAsync();

                    var existeProduto = _dataContext.ProdutosSelecionados
                       .Where(prod => prod.usuario == comprados[i].usuario)
                       .FirstOrDefault();

                    if (existeProduto != null)
                    {
                        _dataContext.Remove(existeProduto);
                        await _dataContext.SaveChangesAsync();
                    }                    
                }

                return "OK";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object MostrarHistorico(string usuario)
        {
            try
            {
                var retorno = _dataContext.ProdutosComprados.Where(user => user.usuario == usuario).ToList();

                if (retorno.Count() < 1)
                {
                    return null;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
