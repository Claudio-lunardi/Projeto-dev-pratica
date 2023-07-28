using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Models.Agendamento
{
    public class tAgendamento
    {
        public int ID { get; set; }
        public int PacienteID { get; set; }
        public string Paciente { get; set; } = string.Empty;
        public string Clinica { get; set; } = string.Empty;
        public DateTime DataConsulta { get; set; }
        public string MedicoEspecialidade { get; set; } = string.Empty;
        public bool Enable { get; set; }

    }
}
