using Campus.CustomerMiddlewares;
using Campus.Infrastructure;
using Campus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// 启用ILog日志框架
builder.Services.AddLogging(config =>
{
    config.AddConsole();
});

// 将服务添加到容器中。 并在整个应用程序中开启Authorize属性
builder.Services.AddControllersWithViews();

// 将DbContext注册为服务
builder.Services.AddDbContext<CampusDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CampusDb"));
    // 启用延迟加载代理
    options.UseLazyLoadingProxies();
    // 日志打印 输出到控制台
    options.LogTo(str =>
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        // _logger.LogDebug("【{time}】线程编号：【{threadId}】{str}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Environment.CurrentManagedThreadId, str);
        Console.WriteLine($"【{DateTime.Now:yyyy-MM-dd HH:mm:ss}】线程编号：【{Environment.CurrentManagedThreadId}】{str}");
        Console.ForegroundColor = ConsoleColor.White;
    }, LogLevel.Debug, DbContextLoggerOptions.SingleLine);
});

// 注册Session服务 超时15秒
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(15);
});

// 配置identity服务 覆盖默认的错误提示
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddEntityFrameworkStores<CampusDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // 必须要确认邮箱才可以登陆
    options.SignIn.RequireConfirmedEmail = false;
    // 最小长度6
    options.Password.RequiredLength = 6;
    // 必须有一个非字母
    options.Password.RequireNonAlphanumeric = false;
    // 必须包含数字
    options.Password.RequireDigit = false;
    // 必须包含小写字母
    options.Password.RequireLowercase = false;
    // 必须包含的最小字符数
    options.Password.RequiredUniqueChars = 0;
    // 必须包含大写字符
    options.Password.RequireUppercase = false;
});
// 注册字符串保护类
builder.Services.AddSingleton<DataProtectionPurposeStrings>();

var app = builder.Build();

// 配置HTTP请求管道。 如果 不是开发环境
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // 默认的HSTS值为30天。对于生产场景，您可能需要更改此选项，请参阅https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}
// 使用Session
app.UseSession();

app.UseStaticFiles();

app.UseHttpsRedirection();

// 认证(搭配Identity框架使用，控制用户身份认证)
app.UseAuthentication();

app.UseRouting();

// 授权(搭配Identity框架使用，可以使未登录的用户无法访问部分资源)
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

app.Run();
