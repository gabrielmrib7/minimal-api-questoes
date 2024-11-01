using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using minimal_api.Context;
using minimal_api.Entities;
using minimal_api.Entities.ModelViews;
using minimal_api.Interfaces;

#region Builder

var builder = WebApplication.CreateBuilder(args);

var key = builder.Configuration.GetSection("Jwt").ToString();
if (string.IsNullOrEmpty(key)) key = "123456";

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false

    };
});

builder.Services.AddAuthorization();

builder.Services.AddScoped<IAdminServico, AdminServico>();
builder.Services.AddScoped<IQuestaoServico, QuestaoServico>();

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("mysql"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("mysql"))
    );
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o TokenJWT:"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});




var app = builder.Build();

#endregion

#region Home
app.MapGet("/", () => Results.Json(new Home())).AllowAnonymous().WithTags("Home");
#endregion

#region Administradores

string GerarTokenJwt(Admin admin)
{
    if (string.IsNullOrEmpty(key)) return string.Empty;

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim>()
        {
            new Claim("Email", admin.Email),
            new Claim(ClaimTypes.Role, admin.Perfil.ToString()),
            new Claim("Perfil", admin.Perfil.ToString())
        };

    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.Now.AddDays(1),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);

}


app.MapPost("/administradores/login", ([FromBody] LoginDTO loginDTO, IAdminServico adminServico) =>
{

    var admin = adminServico.Login(loginDTO);

    if (admin != null)

    {
        string token = GerarTokenJwt(admin);
        return Results.Ok(new AdmLogado
        {
            Email = admin.Email,
            Perfil = admin.Perfil.ToString(),
            Token = token
        });
    }

    else
        return Results.Unauthorized();
}).AllowAnonymous().WithTags("Administradores");

app.MapPost("/administradores", ([FromBody] AdminDTO adminDTO, IAdminServico adminServico) =>
{

    var adm = new Admin
    {
        Email = adminDTO.Email,
        Senha = adminDTO.Senha,
        Perfil = adminDTO.Perfil

    };

    adminServico.Incluir(adm);

    return Results.Created($"/administradores/{adm.Id}", new AdminModelView
    {
        Email = adm.Email,
        Perfil = adm.Perfil.ToString(),
        Id = adm.Id
    });
}).RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
.WithTags("Administradores");

app.MapGet("/administradores", ([FromQuery] int? pagina, IAdminServico adminServico) =>
{

    var adm = new List<AdminModelView>();
    var adms = adminServico.Todos(pagina);

    foreach (var admins in adms)
    {
        adm.Add(new AdminModelView
        {
            Email = admins.Email,
            Perfil = admins.Perfil.ToString(),
            Id = admins.Id
        });
    }

    return Results.Ok(adm);
}).RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
.WithTags("Administradores");

app.MapGet("/administradores/{id}", ([FromRoute] int id, IAdminServico adminServico) =>
{


    var admin = adminServico.BuscaPorId(id);

    if (admin == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(new AdminModelView
    {
        Email = admin.Email,
        Perfil = admin.Perfil.ToString(),
        Id = admin.Id
    });
}).RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
.WithTags("Administradores");




#endregion

#region Questoes

Erros mensagensDTO(QuestaoDTO questaoDTO)
{

    var mensagens = new Erros
    {
        Mensagem = new List<string>()
    };

    if (string.IsNullOrEmpty(questaoDTO.Enunciado))
        mensagens.Mensagem.Add("O Enunciado não pode ser vazio");
    if (string.IsNullOrEmpty(questaoDTO.A))
        mensagens.Mensagem.Add("A Alternativa A não pode ser vazia");
    if (string.IsNullOrEmpty(questaoDTO.B))
        mensagens.Mensagem.Add("A Alternativa B não pode ser vazia");
    if (string.IsNullOrEmpty(questaoDTO.B))
        mensagens.Mensagem.Add("A Alternativa B não pode ser vazia");
    if (string.IsNullOrEmpty(questaoDTO.B))
        mensagens.Mensagem.Add("A Alternativa B não pode ser vazia");


    return mensagens;

}


app.MapPost("/questoes", ([FromBody] QuestaoDTO questaoDTO, IQuestaoServico questaoServico) =>
{


    var mensagens = mensagensDTO(questaoDTO);


    if (mensagens.Mensagem.Count() > 0)
        return Results.BadRequest(mensagens);



    var questao = new Questao
    {
        Enunciado = questaoDTO.Enunciado,
        A = questaoDTO.A,
        B = questaoDTO.B,
        C = questaoDTO.C,
        D = questaoDTO.D,
        AlternativaCorreta = questaoDTO.AlternativaCorreta,
        Materia = questaoDTO.Materia
    };

    questaoServico.Incluir(questao);

    return Results.Created($"/questoes/{questao.Id}", questao);
}).WithTags("Questoes")
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" });


app.MapGet("/questoes", ([FromQuery] int? pagina, IQuestaoServico questaoServico) =>
{


    var questao = questaoServico.Todos(pagina);

    return Results.Ok(questao);
}).RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
.WithTags("Questoes");

app.MapGet("/questoes/{id}", ([FromRoute] int id, IQuestaoServico questaoServico) =>
{


    var questao = questaoServico.BuscaPorId(id);

    if (questao == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(questao);
}).RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
.WithTags("Questoes");

app.MapGet("/materia/{materia}", ([FromRoute] string materia, IQuestaoServico questaoServico) =>
{


    var questao = questaoServico.BuscaPorMateria(materia);

    if (questao == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(questao);
}).RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
.WithTags("Questoes");


app.MapPut("/questoes/{id}", ([FromRoute] int id, QuestaoDTO questaoDTO, IQuestaoServico questaoServico) =>
{

    var questao = questaoServico.BuscaPorId(id);

    if (questao == null)
        return Results.NotFound();


    var mensagens = mensagensDTO(questaoDTO);
    if (mensagens.Mensagem.Count() > 0)
        return Results.BadRequest(mensagens);


    questao.Enunciado = questaoDTO.Enunciado;
    questao.Materia = questaoDTO.Materia;
    questao.AlternativaCorreta = questaoDTO.AlternativaCorreta;
    questao.A = questaoDTO.A;
    questao.B = questaoDTO.B;
    questao.C = questaoDTO.C;
    questao.D = questaoDTO.D;
    questaoServico.Atualizar(questao);

    return Results.Ok(questao);
}).RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
.WithTags("Questoes");

app.MapDelete("/questoes/{id}", ([FromRoute] int id, IQuestaoServico questaoServico) =>
{


    var questao = questaoServico.BuscaPorId(id);

    if (questao == null)
    {
        return Results.NotFound();
    }

    questaoServico.Apagar(questao);

    return Results.NoContent();
}).RequireAuthorization()
.RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
.WithTags("Questoes");

#endregion

#region App


app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();




app.Run();

#endregion

