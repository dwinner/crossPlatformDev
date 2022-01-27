using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MvvxSandboxApp.Core.Models;

namespace MvvxSandboxApp.Core.Services.Implementations
{
   internal sealed class UsaStateEntityConfig : IEntityTypeConfiguration<UsaState>
   {
      public void Configure(EntityTypeBuilder<UsaState> builder)
      {
         builder
            .ToTable(nameof(UsaStateDbContext.UsaStates))
            .HasKey(state => state.UsaStateId);

         builder.Property(state => state.UsaStateId).ValueGeneratedOnAdd();
         builder.Property(state => state.StateName).IsRequired();
         builder.Property(state => state.Abbr).IsRequired();
         builder.Property(state => state.HoodYear).IsRequired();
         builder.Property(state => state.Capital).IsRequired();
         builder.Property(state => state.CapitalSinceYear).IsRequired();
         builder.HasIndex(state => state.StateName).IsUnique();
         builder.HasIndex(state => state.Abbr).IsUnique();
         builder.HasIndex(state => state.Capital).IsUnique();

         builder
            .HasOne(usaState => usaState.UsaStateDetail)
            .WithOne(detail => detail.UsaState)
            .HasForeignKey<UsaStateDetail>(detail => detail.UsaStateDetailId);
      }
   }
}