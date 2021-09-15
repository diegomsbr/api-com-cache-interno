using System;

namespace ApiComCache
{
    public class Produto
    {
        public int CodigoProduto { get; set; }

        public string NomeProduto { get; set; }

        public string DescricaoProduto { get; set; }
        public bool AtualizaCache { get; set; }

    }
}
