using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmpresaWebApi.Models;

namespace EmpresaWebApi.DTOs
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Departamento, DepartamentoDTO>()
                    .ReverseMap();

                cfg.CreateMap<Empleado, EmpleadoDTO>()
                    .ReverseMap();
            });
        }
    }
}
