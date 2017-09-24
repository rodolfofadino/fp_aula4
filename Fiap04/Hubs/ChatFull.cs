using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiap04.Hubs
{
    public class ChatFull: Hub
    {

        public Task EnviarMensagem(Mensagem mensagem)
        {
            return Clients.All.InvokeAsync("publicarMensagem", mensagem);
        }
    }
    public class Mensagem
    {
        public string Nome { get; set; }
        public string Msg { get; set; }
    }
}
