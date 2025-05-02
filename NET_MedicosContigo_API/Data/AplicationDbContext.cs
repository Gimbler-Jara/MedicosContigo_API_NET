using Microsoft.EntityFrameworkCore;
using NET_MedicosContigo_API.DTO;
using NET_MedicosContigo_API.Models;



namespace NET_MedicosContigo_API.Data
{
    public class AplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Disponibilidad> Disponibilidades { get; set; }
        public DbSet<CitaMedica> Citas { get; set; }
        public DbSet<Hora> Horas { get; set; }
        public DbSet<DiaSemana> DiasSemana { get; set; }
        public DbSet<EstadoCita> EstadosCita { get; set; }
        public DbSet<ResultadoGenerico> ResultadosGenericos { get; set; }
        public DbSet<ResultadoCitaDTO> ResultadoCitaDTO { get; set; }
        public DbSet<CitasReservadasPorPacienteResponseDTO> CitasReservadasPorPaciente { get; set; } = null!;




        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().ToTable("tb_usuario");
            modelBuilder.Entity<Paciente>().ToTable("tb_paciente");
            modelBuilder.Entity<Rol>().ToTable("tb_rol");
            modelBuilder.Entity<DocumentType>().ToTable("tb_document_type");
            modelBuilder.Entity<Medico>().ToTable("tb_medico");
            modelBuilder.Entity<Especialidad>().ToTable("tb_especialidad");
            modelBuilder.Entity<Hora>().ToTable("tb_hora");
            modelBuilder.Entity<DiaSemana>().ToTable("tb_dia_semana");
            modelBuilder.Entity<Disponibilidad>().ToTable("tb_disponibilidad");
            modelBuilder.Entity<CitaMedica>().ToTable("tb_cita_medica");
            modelBuilder.Entity<EstadoCita>().ToTable("tb_estado_cita");
            modelBuilder.Entity<MedicosPorEspecialidadDTO>().HasNoKey();
            modelBuilder.Entity<DiasDisponiblesPorMedicoDTO>().HasNoKey();
            modelBuilder.Entity<HorasDisponiblesDeCitasDTO>().HasNoKey();
            modelBuilder.Entity<DisponibilidadCitaPorMedicoDTO>().HasNoKey();
            modelBuilder.Entity<CitasAgendadasResponseDTO>().HasNoKey();
            modelBuilder.Entity<RegistrarDisponibilidadDeCitaDTO>().HasNoKey();
            modelBuilder.Entity<ResultadoCitaDTO>().HasNoKey();
            modelBuilder.Entity<CitasReservadasPorPacienteResponseDTO>().HasNoKey().ToView(null);



            // Relaciones 1:1 necesarias
            modelBuilder.Entity<Paciente>()
                .HasKey(p => p.IdUsuario);

            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Usuario)
                .WithOne(u => u.Paciente)
                .HasForeignKey<Paciente>(p => p.IdUsuario)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Medico>()
                .HasKey(m => m.IdUsuario);

            modelBuilder.Entity<Medico>()
                .HasOne(m => m.Usuario)
                .WithOne(u => u.Medico)
                .HasForeignKey<Medico>(m => m.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
               .Property(u => u.Activo)
               .HasColumnName("activo")
               .HasConversion<bool>();
        }

    }
}
