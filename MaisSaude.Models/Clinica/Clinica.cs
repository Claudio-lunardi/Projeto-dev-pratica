using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Models.Clinica
{
    public class Clinica
    {
        public int ID { get; set; }
        public int ClinicaUser { get; set; }
        public DateTime data_fundacao { get; set; }
        public int capacidade_leitos { get; set; }

    }
}
