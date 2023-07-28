using MaisSaude.Models.Agendamento;
using MaisSaude.Models.tUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.Agendamento
{
    public interface IAgendamento
    {
        Task<bool> CriarAgendamento(tAgendamento agendamento);

        Task<IEnumerable<tUser>> GetMedico();
        Task<IEnumerable<tAgendamento>> GetAgendamentos(int ID);

    }
}
