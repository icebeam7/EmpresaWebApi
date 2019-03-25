using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmpresaWebApi.Models
{
    public class Empleado
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; }

        [Required, MaxLength(50)]
        public string ApellidoPaterno { get; set; }

        [Required, MaxLength(50)]
        public string ApellidoMaterno { get; set; }

        [ForeignKey("FK_DepartamentoEmpleado")]
        public int IdDepartamento { get; set; }
        public Departamento FK_DepartamentoEmpleado { get; set; }
    }
}
