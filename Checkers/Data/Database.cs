﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Checkers.Data
{
    public partial class Database : DbContext
    {
        public Database()
        {
        }

        public Database(DbContextOptions<Database> options)
            : base(options)
        {
        }

        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<AchievementOption> AchievementOptions { get; set; }
        public virtual DbSet<EventOption> EventOptions { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GamesProgress> GamesProgresses { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemOption> ItemOptions { get; set; }
        public virtual DbSet<PictureOption> PictureOptions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DELL\\SQLEXPRESS;Database=Checkers;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AchievementId).HasColumnName("achievement_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.AchievementNavigation)
                    .WithMany(p => p.Achievements)
                    .HasForeignKey(d => d.AchievementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Achieveme__achie__4BAC3F29");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Achievements)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Achieveme__user___4AB81AF0");
            });

            modelBuilder.Entity<AchievementOption>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<EventOption>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.Player1AnimationId).HasColumnName("player1_animation_id");

                entity.Property(e => e.Player1ChekersId).HasColumnName("player1_chekers_id");

                entity.Property(e => e.Player1Id).HasColumnName("player1_id");

                entity.Property(e => e.Player1RaitingChange).HasColumnName("player1_raiting_change");

                entity.Property(e => e.Player2AnimationId).HasColumnName("player2_animation_id");

                entity.Property(e => e.Player2ChekersId).HasColumnName("player2_chekers_id");

                entity.Property(e => e.Player2Id).HasColumnName("player2_id");

                entity.Property(e => e.Player2RaitingChange).HasColumnName("player2_raiting_change");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.WinnerId).HasColumnName("winner_id");

                entity.HasOne(d => d.Player1Animation)
                    .WithMany(p => p.GamePlayer1Animations)
                    .HasForeignKey(d => d.Player1AnimationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Games__player1_a__5629CD9C");

                entity.HasOne(d => d.Player1Chekers)
                    .WithMany(p => p.GamePlayer1Chekers)
                    .HasForeignKey(d => d.Player1ChekersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Games__player1_c__5441852A");

                entity.HasOne(d => d.Player1)
                    .WithMany(p => p.GamePlayer1s)
                    .HasForeignKey(d => d.Player1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Games__player1_i__52593CB8");

                entity.HasOne(d => d.Player2Animation)
                    .WithMany(p => p.GamePlayer2Animations)
                    .HasForeignKey(d => d.Player2AnimationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Games__player2_a__571DF1D5");

                entity.HasOne(d => d.Player2Chekers)
                    .WithMany(p => p.GamePlayer2Chekers)
                    .HasForeignKey(d => d.Player2ChekersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Games__player2_c__5535A963");

                entity.HasOne(d => d.Player2)
                    .WithMany(p => p.GamePlayer2s)
                    .HasForeignKey(d => d.Player2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Games__player2_i__534D60F1");

                entity.HasOne(d => d.Winner)
                    .WithMany(p => p.GameWinners)
                    .HasForeignKey(d => d.WinnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Games__winner_id__5812160E");
            });

            modelBuilder.Entity<GamesProgress>(entity =>
            {
                entity.ToTable("GamesProgress");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionDesc)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("action_desc")
                    .IsFixedLength(true);

                entity.Property(e => e.ActionId).HasColumnName("action_id");

                entity.Property(e => e.ActionNum).HasColumnName("action_num");

                entity.Property(e => e.ActionTime).HasColumnName("action_time");

                entity.Property(e => e.ActorId).HasColumnName("actor_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.GamesProgresses)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GamesProg__actio__5CD6CB2B");

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.GamesProgresses)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GamesProg__actor__5BE2A6F2");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GamesProgresses)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GamesProg__game___5AEE82B9");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ItemNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Items__item_id__4F7CD00D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Items__user_id__4E88ABD4");
            });

            modelBuilder.Entity<ItemOption>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PictureOption>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Login, "UQ__Users__7838F272D723581D")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__AB6E616446009C8B")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasDefaultValueSql("((1000))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.LastActivity)
                    .HasColumnType("datetime")
                    .HasColumnName("last_activity")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("login");

                entity.Property(e => e.Nick)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("nick");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("password");

                entity.Property(e => e.PictureId)
                    .HasColumnName("picture_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasDefaultValueSql("((1000))");

                entity.Property(e => e.SelectedAnimation)
                    .HasColumnName("selected_animation")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SelectedCheckers)
                    .HasColumnName("selected_checkers")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Picture)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PictureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__picture_i__4316F928");

                entity.HasOne(d => d.SelectedAnimationNavigation)
                    .WithMany(p => p.UserSelectedAnimationNavigations)
                    .HasForeignKey(d => d.SelectedAnimation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__selected___46E78A0C");

                entity.HasOne(d => d.SelectedCheckersNavigation)
                    .WithMany(p => p.UserSelectedCheckersNavigations)
                    .HasForeignKey(d => d.SelectedCheckers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__selected___44FF419A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
