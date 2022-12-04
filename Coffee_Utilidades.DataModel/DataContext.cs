using Coffee_Utilidades.DataModel.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Coffee_Utilidades.DataModel
{
    public class DataContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProdutosCadastrados>().HasKey(id => id.Cod_Produto);

            builder.Entity<ProdutosSelecionados>().HasKey(id => id.Cod_Produto);

            builder.Entity<FormaPagamento>().HasKey(id => id.numeroCartao);

            builder.Entity<ProdutosComprados>().HasKey(id => id.id);
        }

        public DbSet<ProdutosCadastrados> ProdutosCadastrados { get; set; }
        public DbSet<ProdutosSelecionados> ProdutosSelecionados { get; set; }
        public DbSet<FormaPagamento> FormaPagamento { get; set; }
        public DbSet<ProdutosComprados> ProdutosComprados { get; set; }
    }
}
