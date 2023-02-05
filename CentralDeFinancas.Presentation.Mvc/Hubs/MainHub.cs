using CentralDeFinancas.Presentation.Mvc.Models;
using Microsoft.AspNetCore.SignalR;

namespace CentralDeFinancas.Presentation.Mvc.Hubs
{
    /// <summary>
    /// Hub principal do projeto
    /// </summary>
    public class MainHub : Hub
    {
        public async Task CadastroDeContas(ContaViewModel model)
        {
            //criando um RECEIVER (componente para retornar conteudo
            //para as funções que "escutam" este hub)
            await Clients.All.SendAsync("ReceiveMessage", model);
        }
    }
}
