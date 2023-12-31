using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using SwiftCode.BBS.EntityFramework.EfContext;
using SwiftCode.BBS.Extensions.ServiceExtensions;
using SwiftCode.BSS.Common.Helper;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AddResponseHeadersFilter>();
    c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

    c.OperationFilter<SecurityRequirementsOperationFilter>();
    c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格",
        Name = "Authjorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v0.1.0",
        Title = "SwiftCode.BBS.API",
        Description = "框架说明文档",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "SwiftCode",
            Email = "SwiftCode@xxx.com"
        }
    });
    var basePath = AppContext.BaseDirectory;
    var xmlPath = Path.Combine(basePath, "SwiftCode.BBS.API.xml");
    c.IncludeXmlComments(xmlPath, true);

    //var xmlModelPath = Path.Combine(basePath, "SwiftCode.BBS.Model.xml");
    //c.IncludeXmlComments(xmlModelPath, true);
});

builder.Services.AddSingleton(new Appsettings(builder.Configuration));
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Client", policy => policy.RequireRole("Client").Build())
    .AddPolicy("Admin", policy => policy.RequireRole("Admin").Build())
    .AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"))
    .AddPolicy("SystemAndAdmin", policy => policy.RequireRole("Admin").RequireRole("System"));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(o =>
    {
        var audienceConfig = builder.Configuration["Audience:Audience"];
        var symmetricKeyAsBase64 = builder.Configuration["Audience:Secret"];
        var iss = builder.Configuration["Audience:Issuer"];
        var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
        var signingKey = new SymmetricSecurityKey(keyByteArray);
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateIssuer = true,
            ValidIssuer = iss,
            ValidateAudience = true,
            ValidAudience = audienceConfig,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RequireExpirationTime = true
        };
    });

builder.Services.AddDbContext<SwiftCodeBbsContext>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerbuilder=>
{
    containerbuilder.RegisterModule<AutofacModuleRegister>();
}));

builder.Services.AddAutoMapperSetup();
builder.Services.AddMemoryCacheSetup();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

