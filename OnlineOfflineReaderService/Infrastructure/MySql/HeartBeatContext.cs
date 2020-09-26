﻿using System;
using OnlineOfflineReaderService.Domain;
using Microsoft.EntityFrameworkCore;
using OnlineOfflineReaderService.Infrastructure.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace OnlineOfflineReaderService.Infrastructure.MySql
{
    public class HeartBeatContext : DbContext, IHeartBeatContext
    {
        public DbSet<HeartBeatModel> HeartBeat { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=10.0.0.3;database=Inter;user=user;password=pass");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HeartBeatModel>(entity =>
            {
                
                entity.HasKey(_ => _.name);
                entity.Property(_ => _.timestamp);
            });
        }
        public async Task Save()
        {
            await this.SaveChangesAsync();
        }
        //The fact that this isn't handled automatically is a little sad 
        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            var type = entity.GetType();
            var et = this.Model.FindEntityType(type);
            var key = et.FindPrimaryKey();

            var keys = new object[key.Properties.Count];
            var x = 0;
            foreach (var keyName in key.Properties)
            {
                var keyProperty = type.GetProperty(keyName.Name, BindingFlags.Public | BindingFlags.Instance);
                keys[x++] = keyProperty.GetValue(entity);
            }

            var originalEntity = Find(type, keys);
            if (Entry(originalEntity).State == EntityState.Modified)
            {
                return base.Update(entity);
            }

            Entry(originalEntity).CurrentValues.SetValues(entity);
            return Entry((TEntity)originalEntity);
        }
    }
}
