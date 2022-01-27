using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MvvxSandboxApp.Core.Models;

namespace MvvxSandboxApp.Core.Services.Implementations
{
   internal sealed class UsaStateDetailEntityConfig : IEntityTypeConfiguration<UsaStateDetail>
   {
      public void Configure(EntityTypeBuilder<UsaStateDetail> builder)
      {
         // Usa state has always it's detail (1..1), so merge it to one table
         builder
            .ToTable(nameof(UsaStateDbContext.UsaStates))
            .HasKey(stateDetail => stateDetail.UsaStateDetailId);

         builder.HasIndex(stateDetail => stateDetail.StateUri).IsUnique();
         builder.HasIndex(stateDetail => stateDetail.FlagImageUri).IsUnique();
      }
   }
}