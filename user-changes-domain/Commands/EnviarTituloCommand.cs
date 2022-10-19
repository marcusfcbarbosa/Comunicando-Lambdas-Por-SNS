using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using user_changes_domain.Interfaces;

namespace user_changes_domain.Commands
{
    public class EnviarTituloCommand : IRequest<string>
    {
        public string idLinha { get; set; }
        public string codEspecieDoc { get; set; }
        public string seuNumero { get; set; }
        public DateTime dataVencimento { get; set; }
    }
    public class EnviarTituloHandler : IRequestHandler<EnviarTituloCommand, string>
    {
        private readonly ITituloRepository _tituloRepository;

        public EnviarTituloHandler(ITituloRepository tituloRepository)
        {
            _tituloRepository = tituloRepository;
        }

        public async Task<string> Handle(EnviarTituloCommand request, CancellationToken cancellationToken)
        {
            return await _tituloRepository.SalvarTitulo(new EnvioTitulo
            {
                codEspecieDoc = request.codEspecieDoc,
                dataVencimento = request.dataVencimento,
                idLinha = request.idLinha,
                seuNumero = request.seuNumero,
            });
        }
    }
}
