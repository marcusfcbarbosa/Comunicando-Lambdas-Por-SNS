using System;

namespace user_changes_domain.Data.Entities
{
    public class Titulo : Entity
    {
        public string idLinha { get; private set; }
        public string codEspecieDoc { get; private set; }
        public string seuNumero { get; private set; }
        public DateTime dataVencimento { get; private set; }
        protected Titulo() { }
        public Titulo(string idLinha, string codEspecieDoc, string seuNumero, DateTime dataVencimento)
        {
            this.idLinha = idLinha;
            this.codEspecieDoc = codEspecieDoc;
            this.seuNumero = seuNumero;
            this.dataVencimento = dataVencimento;
        }
    }
}
