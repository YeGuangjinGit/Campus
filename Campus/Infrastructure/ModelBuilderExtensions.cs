using Campus.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Campus.Infrastructure
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// 准备表的初始化数据，以及之间的主外键联系
        /// </summary>
        /// <param name="modelBuilder">模型定义实体结构、它们之间的关系以及它们如何映射到数据库</param>
        /// <param name="示例1：定义初始化数据">
        ///     <code>
        ///         modelBuilder.Entity&lt;Gender&gt;().HasData(new List&lt;Gender&gt;() {
        ///             new(){ GenderId=1, GenderValue = "保密"},
        ///             new(){ GenderId=2, GenderValue="男"},
        ///             new(){ GenderId=3,GenderValue="女"}
        ///         });
        ///     </code>
        /// </param>
        /// <param name="示例2：定义表和数据结构">
        ///     <code>
        ///         表SpecialColumn存在主键，是SpecialColumnId字段
        ///         modelBuilder.Entity&lt;SpecialColumn&gt;().ToTable("c_SpecialColumns").HasKey(s => s.SpecialColumnId);
        ///         表SpecialColumn字段SpecialColumnValue长度40，是必填的
        ///         modelBuilder.Entity&lt;SpecialColumn&gt;().Property(s => s.SpecialColumnValue).HasMaxLength(40).IsRequired();
        ///     </code>
        /// </param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region 专栏表
            modelBuilder.Entity<SpecialColumn>().ToTable("c_SpecialColumns").HasKey(s => s.SpecialColumnId);
            modelBuilder.Entity<SpecialColumn>().Property(s => s.SpecialColumnValue).HasMaxLength(40).IsRequired();
            #endregion
            #region 性别表
            modelBuilder.Entity<Gender>().ToTable("c_Genders").HasKey(u => u.GenderId);
            modelBuilder.Entity<Gender>().Property(g => g.GenderValue).HasMaxLength(40).IsRequired();
            #endregion
            #region 用户表
            modelBuilder.Entity<AppUser>().Property(u => u.HeadPortrait).HasMaxLength(100);
            modelBuilder.Entity<AppUser>().Property(u => u.Nickname).HasMaxLength(40);
            modelBuilder.Entity<AppUser>().Property(u => u.PersonalSignature).HasMaxLength(100);
            modelBuilder.Entity<AppUser>().Property(u => u.GenderId).HasDefaultValue(1).IsRequired();
            modelBuilder.Entity<AppUser>().Property(u => u.Birth).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<AppUser>().Property(u => u.CreateAt).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<AppUser>().Property(u => u.AuthenticationId).HasDefaultValue(null);
            modelBuilder.Entity<AppUser>().Property(u => u.HeadPortrait).HasDefaultValue("/img/head_img.png");
            modelBuilder.Entity<AppUser>()
                .HasOne<Gender>(u => u.Gender).WithMany(g => g.Users)
                .HasForeignKey(u => u.GenderId).OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region 收藏表
            modelBuilder.Entity<Collection>().ToTable("c_Collections").HasKey(c => c.CollectionId);
            modelBuilder.Entity<Collection>()
                .HasOne<AppUser>(c => c.User)
                .WithMany(u => u.Collections)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Collection>()
                .HasOne<Works>(c => c.Works)
                .WithMany(w => w.Collections)
                .HasForeignKey(c => c.WorksId)
                 .OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region 点赞表
            modelBuilder.Entity<Fabulous>().ToTable("c_Fabulous").HasKey(c => c.FabulousId);
            modelBuilder.Entity<Fabulous>()
                .HasOne<AppUser>(f => f.User)
                .WithMany(u => u.Fabulous)
                .HasForeignKey(f => f.UserId)
                 .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Fabulous>()
               .HasOne<Works>(f => f.Works)
               .WithMany(w => w.Fabulous)
               .HasForeignKey(f => f.WorksId)
                 .OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region 关注表
            modelBuilder.Entity<Follow>().ToTable("c_Follows").HasKey(f => f.FollowId);
            modelBuilder.Entity<Follow>()
                .HasOne<AppUser>(f => f.User)
                .WithMany(u => u.Follows)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Follow>()
                .HasOne<AppUser>(f => f.Target)
                .WithMany(u => u.Targets)
                .HasForeignKey(f => f.TargetId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region 作品表
            modelBuilder.Entity<Works>().ToTable("c_Works").HasKey(w => w.WorksId);
            modelBuilder.Entity<Works>().Property(w => w.Title).IsRequired();
            modelBuilder.Entity<Works>().Property(w => w.Content).IsRequired();
            modelBuilder.Entity<Works>().Property(w => w.ReleaseTime).HasDefaultValueSql("GetDate()").IsRequired();
            modelBuilder.Entity<Works>().Property(w => w.SpecialColumnId).HasDefaultValue(1).IsRequired();
            modelBuilder.Entity<Works>().Property(w => w.Browse).HasDefaultValue(0).IsRequired();
            modelBuilder.Entity<Works>().Property(w => w.IsDelete).HasDefaultValue(false).IsRequired();
            modelBuilder.Entity<Works>()
                .HasOne<AppUser>(w => w.User)
                .WithMany(u => u.Works)
                .HasForeignKey(w => w.UserId)
                 .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Works>()
                .HasOne<SpecialColumn>(w => w.SpecialColumn)
                .WithMany(s => s.Works)
                .HasForeignKey(w => w.SpecialColumnId)
                 .OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region 校园活动表
            modelBuilder.Entity<Activity>().ToTable("c_Activitys").HasKey(p => p.ActivityId);
            modelBuilder.Entity<Activity>().Property(a => a.ActivityTitle).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Activity>().Property(a => a.ActivityContent).HasMaxLength(1000).IsRequired();
            modelBuilder.Entity<Activity>().Property(a => a.ActivityLocale).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Activity>().Property(a => a.ActivityNumber).HasDefaultValue(2).IsRequired();
            modelBuilder.Entity<Activity>().Property(a => a.ActivityTime).HasDefaultValueSql("GetDate()").IsRequired();
            modelBuilder.Entity<Activity>().Property(a => a.ReleaseTime).HasDefaultValueSql("GetDate()").IsRequired();
            modelBuilder.Entity<Activity>()
                .HasOne<AppUser>(a => a.User)
                .WithMany(u => u.Activities)
                .HasForeignKey(a => a.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region 活动登记表
            modelBuilder.Entity<Enroll>().ToTable("c_Enrolls").HasKey(e => e.EnrolId);
            modelBuilder.Entity<Enroll>().Property(e => e.Participate).HasDefaultValue(false).IsRequired();
            modelBuilder.Entity<Enroll>()
                .HasOne<AppUser>(e => e.User)
                .WithMany(u => u.Enrolls)
                .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Enroll>()
                .HasOne<Activity>(e => e.Activity)
                .WithMany(a => a.Enrolls)
                .HasForeignKey(e => e.ActivityId)
                 .OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region 意见表
            modelBuilder.Entity<Opinion>().ToTable("c_Opinions").HasKey(e => e.OpinionId);
            modelBuilder.Entity<Opinion>().Property(o => o.OpinionTitle).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Opinion>().Property(o => o.OpinionContent).HasMaxLength(1000).IsRequired();
            modelBuilder.Entity<Opinion>().Property(o => o.ReleaseTime).HasDefaultValueSql("GetDate()").IsRequired();
            modelBuilder.Entity<Opinion>().Property(o => o.Result).HasMaxLength(1000);
            modelBuilder.Entity<Opinion>().Property(o => o.HandleId).IsRequired(false);
            modelBuilder.Entity<Opinion>()
                .HasOne<AppUser>(o => o.User)
                .WithMany(u => u.Opinions)
                .HasForeignKey(o => o.UserId)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Opinion>()
                .HasOne<AppUser>(o => o.Handle)
                .WithMany(u => u.Handles);

            #endregion
            #region 评论表
            modelBuilder.Entity<Comment>().ToTable("c_Comments").HasKey(e => e.CommentId);
            modelBuilder.Entity<Comment>().Property(c => c.WorksId).IsRequired(false);
            modelBuilder.Entity<Comment>().Property(c => c.Content).HasMaxLength(1000).IsRequired();
            modelBuilder.Entity<Comment>().Property(c => c.ReleaseTime).HasDefaultValueSql("GetDate()").IsRequired();
            modelBuilder.Entity<Comment>().Property(c => c.Like).HasDefaultValue(0).IsRequired();
            modelBuilder.Entity<Comment>()
                .HasOne<AppUser>(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Comment>()
                .HasOne<Comment>(c => c.TComment)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.Parentid)
				 .OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<Comment>()
				.HasOne<Works>(c => c.Works)
				.WithMany(w => w.Comments)
				.HasForeignKey(c => c.WorksId)
				.OnDelete(DeleteBehavior.NoAction);
			#endregion
			#region 认证信息表
			modelBuilder.Entity<Authentication>().ToTable("c_Authentications").HasKey(a => a.AuthenticationId);
            modelBuilder.Entity<Authentication>().Property(a => a.IdCard).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Authentication>().Property(a => a.Photo).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Authentication>().Property(a => a.CreateAt).HasDefaultValueSql("GetDate()").IsRequired();
            modelBuilder.Entity<Authentication>().Property(a => a.IsPass).HasDefaultValue(null);
            modelBuilder.Entity<Authentication>().Property(a => a.Handle).HasDefaultValue(null);
            modelBuilder.Entity<Authentication>()
                .HasOne(a => a.User)
                .WithOne(a => a.Authentication)
                .HasForeignKey<AppUser>(u => u.AuthenticationId)
                 .OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region 账号信息变更表
            modelBuilder.Entity<AccountInformationChange>().ToTable("c_AccountInformationChange").HasKey(a => a.AccountChangeId);
            modelBuilder.Entity<AccountInformationChange>().Property(a => a.ChangeReason).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<AccountInformationChange>().Property(a => a.Code).HasDefaultValueSql("NewId()");
            modelBuilder.Entity<AccountInformationChange>().Property(a => a.CreateAt).HasDefaultValueSql("GetDate()");
            modelBuilder.Entity<AccountInformationChange>()
                .HasOne<AppUser>(a => a.User)
                .WithMany(u => u.InformationChanges)
                .HasForeignKey(a => a.UserId)
                 .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AccountInformationChange>()
                .Property(a => a.Code)
                .HasDefaultValueSql("NewID()");
            #endregion

            modelBuilder.Entity<Gender>().HasData(new List<Gender>() {
                new(){ GenderId=1, GenderValue = "保密"},
                new(){ GenderId=2, GenderValue="男"},
                new(){ GenderId=3,GenderValue="女"}
            });
            modelBuilder.Entity<SpecialColumn>().HasData(new List<SpecialColumn>()
            {
                new(){ SpecialColumnId=1,SpecialColumnValue="校园天地" },
                new(){ SpecialColumnId=2,SpecialColumnValue="学生论坛" },
                new(){ SpecialColumnId=3,SpecialColumnValue="社团活动" },
                new(){ SpecialColumnId=4,SpecialColumnValue="校园热卖" },
                new(){ SpecialColumnId=5,SpecialColumnValue="校园防疫" },
                new(){ SpecialColumnId=6,SpecialColumnValue="校园活动" },
            });

            // 初始化管理员
            // var admin = new AppUser
            // {
            //     Id = Guid.NewGuid().ToString(),
            //     UserName = "Admin",
            //     NormalizedUserName = "Admin",
            //     Email = "2337533962@qq.com",
            //     PhoneNumber = "18571846920",
            // };
            // var adminRole = new IdentityRole()
            // {
            //     Id = Guid.NewGuid().ToString(),
            //     Name = "管理员",
            //     NormalizedName = "管理员",
            // };
            // 
            // modelBuilder.Entity<AppUser>().HasData(admin);
            // modelBuilder.Entity<IdentityRole>().HasData(adminRole);
            // 
            // modelBuilder.Entity<IdentityUserRole<string>>().HasData(new List<IdentityUserRole<string>>
            // {
            //     new IdentityUserRole<string>
            //     {
            //         UserId = admin.Id,
            //         RoleId = adminRole.Id,
            //     }
            // });
        }
    }
}
