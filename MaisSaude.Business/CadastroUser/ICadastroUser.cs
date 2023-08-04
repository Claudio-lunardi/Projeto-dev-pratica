﻿using MaisSaude.Models.tUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Business.CadastroUser
{
    public interface ICadastroUser
    {
        Task<List<tUser>> GetUsers();
        Task<bool> InsertTitular(tUserRetorno tUserRetorno);
    }
}