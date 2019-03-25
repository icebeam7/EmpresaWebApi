using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmpresaWebApi.Models
{
    public class Departamento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public ICollection<Empleado> Empleados { get; set; }
    }
}
