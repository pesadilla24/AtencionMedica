using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtencionMedica_v1.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Consultorio> Consultorio { get; set; }
        public DbSet<TipoTriage> TipoTriage { get; set; }

        public DbSet<Atencion> Atencion { get; set; }

    }


    public class Paciente
    {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }

        public string CodigoADN { get; set; }

        public string Diagnostico { get; set; }
        public int Estado { get; set; }

        //FK
        public int TipoTriageId { get; set; }
        public TipoTriage TipoTriage { get; set; }
        // Fin fk 

        public ICollection<Atencion> Atenciones { get; set; }
        //public ICollection<TiposTriage> TiposTriages { get; set; }
    }
    public class Consultorio
    {
        public int Id { get; set; }
        public string NombreMedico { get; set; }
        public int Estado { get; set; }
        public ICollection<Atencion> Atenciones { get; set; }

    }
    public class TipoTriage
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public ICollection<Paciente> Paciente { get; set; }

    }
    public class Atencion
    {
        public int Id { get; set; }

        public int ConsultorioId { get; set; }
        public Consultorio Consultorio { get; set; }


        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }
    }

}
