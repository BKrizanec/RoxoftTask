using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Models.Models;

public partial class RoxoftLibraryContext : DbContext
{
    public RoxoftLibraryContext()
    {
    }

    public RoxoftLibraryContext(DbContextOptions<RoxoftLibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BorrowedBook> BorrowedBooks { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<PlaceOfResidence> PlaceOfResidences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-6MH2HTI\\SQLEXPRESS;Initial Catalog=RoxoftLibrary;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Author__70DAFC345EDC58B1");

            entity.ToTable("Author");

            entity.Property(e => e.FirstName).HasMaxLength(40);
            entity.Property(e => e.LastName).HasMaxLength(40);
            entity.Property(e => e.ShortBio).HasMaxLength(255);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__3DE0C20707F55822");

            entity.ToTable("Book");

            entity.Property(e => e.Genre).HasMaxLength(40);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Author)
                .HasConstraintName("FK__Book__Author__5EBF139D");
        });

        modelBuilder.Entity<BorrowedBook>(entity =>
        {
            entity.HasKey(e => e.BorrowedBookId).HasName("PK__Borrowed__53B3FBE863FEC838");

            entity.ToTable("BorrowedBook");

            entity.HasOne(d => d.BookNavigation).WithMany(p => p.BorrowedBooks)
                .HasForeignKey(d => d.Book)
                .HasConstraintName("FK__BorrowedBo__Book__6477ECF3");

            entity.HasOne(d => d.MemberNavigation).WithMany(p => p.BorrowedBooks)
                .HasForeignKey(d => d.Member)
                .HasConstraintName("FK__BorrowedB__Membe__656C112C");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Member__0CF04B18115A81C5");

            entity.ToTable("Member");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Contact).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(40);
            entity.Property(e => e.LastName).HasMaxLength(40);

            entity.HasOne(d => d.PlaceOfResidenceNavigation).WithMany(p => p.Members)
                .HasForeignKey(d => d.PlaceOfResidence)
                .HasConstraintName("FK__Member__PlaceOfR__619B8048");
        });

        modelBuilder.Entity<PlaceOfResidence>(entity =>
        {
            entity.HasKey(e => e.PlaceOfResidenceId).HasName("PK__PlaceOfR__88C7EA199FD7E023");

            entity.ToTable("PlaceOfResidence");

            entity.Property(e => e.Name).HasMaxLength(40);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
