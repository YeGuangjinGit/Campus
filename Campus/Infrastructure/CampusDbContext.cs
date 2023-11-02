using Campus.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Campus.Infrastructure;

/// <summary>
/// EntityFrameWork Core的数据库上下文
/// </summary>
/// <list type="bullet">
///     <item>因为使用了Microsoft.AspNetCore.Identity框架，所以继承自IdentityDbContext</item>
///     <item>我们就不用创建User表了，Microsoft.AspNetCore.Identity框架自带用户表、角色表、用户角色关系表等</item>
/// </list>
public class CampusDbContext : IdentityDbContext<AppUser>
{
    public DbSet<AccountInformationChange> InformationChanges { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Authentication> Authentications { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Enroll> Enrolls { get; set; }
    public DbSet<Fabulous> Fabulous { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Opinion> Opinions { get; set; }
    public DbSet<SpecialColumn> SpecialColumns { get; set; }
    public DbSet<Works> Works { get; set; }
    //public DbSet<AppUser> Users { get; set; }

    public CampusDbContext(DbContextOptions<CampusDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// 这里可以配置上下文，使用options.
    /// </summary>
    /// <param name="options"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // 配置log日志的输出 打印的时候使用黄色
        // options.LogTo(str =>
        // {
        //     if (!str.StartsWith("Executed DbCommand"))
        //     {
        //         return;
        //     }
        //     Console.ForegroundColor = ConsoleColor.Yellow;
        //     _logger.LogDebug("【{time}】线程编号：【{threadId}】{str}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Environment.CurrentManagedThreadId, str);
        //     // Console.WriteLine($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】线程编号：【{Environment.CurrentManagedThreadId}】{str}");
        //     Console.ForegroundColor = ConsoleColor.White;
        // }, LogLevel.Debug, DbContextLoggerOptions.SingleLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // 准备种子数据，也就是项目启动开始就有的数据
        modelBuilder.Seed();
    }
}
