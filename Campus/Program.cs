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

// ����ILog��־���
builder.Services.AddLogging(config =>
{
    config.AddConsole();
});

// ��������ӵ������С� ��������Ӧ�ó����п���Authorize����
builder.Services.AddControllersWithViews();

// ��DbContextע��Ϊ����
builder.Services.AddDbContext<CampusDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CampusDb"));
    // �����ӳټ��ش���
    options.UseLazyLoadingProxies();
    // ��־��ӡ ���������̨
    options.LogTo(str =>
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        // _logger.LogDebug("��{time}���̱߳�ţ���{threadId}��{str}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Environment.CurrentManagedThreadId, str);
        Console.WriteLine($"��{DateTime.Now:yyyy-MM-dd HH:mm:ss}���̱߳�ţ���{Environment.CurrentManagedThreadId}��{str}");
        Console.ForegroundColor = ConsoleColor.White;
    }, LogLevel.Debug, DbContextLoggerOptions.SingleLine);
});

// ע��Session���� ��ʱ15��
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(15);
});

// ����identity���� ����Ĭ�ϵĴ�����ʾ
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddEntityFrameworkStores<CampusDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // ����Ҫȷ������ſ��Ե�½
    options.SignIn.RequireConfirmedEmail = false;
    // ��С����6
    options.Password.RequiredLength = 6;
    // ������һ������ĸ
    options.Password.RequireNonAlphanumeric = false;
    // �����������
    options.Password.RequireDigit = false;
    // �������Сд��ĸ
    options.Password.RequireLowercase = false;
    // �����������С�ַ���
    options.Password.RequiredUniqueChars = 0;
    // ���������д�ַ�
    options.Password.RequireUppercase = false;
});
// ע���ַ���������
builder.Services.AddSingleton<DataProtectionPurposeStrings>();

var app = builder.Build();

// ����HTTP����ܵ��� ��� ���ǿ�������
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Ĭ�ϵ�HSTSֵΪ30�졣����������������������Ҫ���Ĵ�ѡ������https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}
// ʹ��Session
app.UseSession();

app.UseStaticFiles();

app.UseHttpsRedirection();

// ��֤(����Identity���ʹ�ã������û������֤)
app.UseAuthentication();

app.UseRouting();

// ��Ȩ(����Identity���ʹ�ã�����ʹδ��¼���û��޷����ʲ�����Դ)
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

app.Run();
