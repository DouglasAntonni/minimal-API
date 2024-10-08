using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal_api.Dominio.Enums;

namespace minimal_api.Dominio.ModelViews
{
    public record AdministradorModelViews
    {
        public int Id { get; set; } = default;
        public string Email { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
    }
}