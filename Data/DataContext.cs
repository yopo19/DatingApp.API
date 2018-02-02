using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;


namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        private readonly IUserSession _user;
        public DataContext(DbContextOptions<DataContext> options, IUserSession user)
        : base(options)         
        {
            _user = user;
         } 
         
    
               
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Auditoria> Audits { get; set; }


        public override int SaveChanges()
        {
            //ChangeTracker.DetectChanges(); // Important!

            var entities = this.ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Modified
                || x.State == EntityState.Added &&
                x.Entity != null && !(x.Entity is Auditoria))
                .ToList();

            foreach (var entidad in entities)
            {
                Auditoria audit = new Auditoria();
                audit.Id = Guid.NewGuid().ToString();
                audit.FechaRevision = DateTime.Now;
                audit.NombreTabla = entidad.Metadata.Name;
                audit.UserName = _user.UserId;
                audit.RemoteIpAddress = _user.UserIP;
                if (entidad.State == EntityState.Added)
                {
                    audit.Accion = AccionesAuditoria.I.ToString();
                    audit.DataOriginal = "";
                    audit.DataNueva = this.obtenerData(entidad, false);

                }
                else if (entidad.State == EntityState.Deleted)
                {
                    audit.Accion = AccionesAuditoria.D.ToString();
                    audit.DataOriginal = this.obtenerData(entidad, true);
                    audit.DataNueva = "";
                }
                else
                {
                    audit.Accion = AccionesAuditoria.U.ToString();
                    audit.DataOriginal = this.obtenerData(entidad, true);
                    audit.DataNueva = this.obtenerData(entidad, false);
                }
                Audits.Add(audit);

            }

            return base.SaveChanges();
        }

        private string obtenerData(EntityEntry ent, bool original)
        {
            var dic = new Dictionary<string, object>();
            foreach (var propiedad in ent.CurrentValues.Properties)
            {
                //propiedad.GetType
                var campo = propiedad.Name;
                var valor = original ? ent.OriginalValues[campo] : ent.CurrentValues[campo];
                dic.Add(campo, valor);
            }

            return JsonConvert.SerializeObject(dic, Formatting.None);

        }
        private enum AccionesAuditoria
        {
            I,
            U,
            D

        }



    }
}