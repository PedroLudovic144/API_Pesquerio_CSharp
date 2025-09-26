using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PesqueiroNetApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenhaCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailCliente = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    IdCompra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DtCompra = table.Column<DateOnly>(type: "date", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.IdCompra);
                });

            migrationBuilder.CreateTable(
                name: "Equipamentos",
                columns: table => new
                {
                    IdEquipamentos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeEquipamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    QuantidadeEquipamento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamentos", x => x.IdEquipamentos);
                });

            migrationBuilder.CreateTable(
                name: "Especies",
                columns: table => new
                {
                    IdEspecie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeEspecie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorEspecie = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FornecedorEspecie = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especies", x => x.IdEspecie);
                });

            migrationBuilder.CreateTable(
                name: "Lagos",
                columns: table => new
                {
                    IdLago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeLago = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lagos", x => x.IdLago);
                });

            migrationBuilder.CreateTable(
                name: "Pesqueiros",
                columns: table => new
                {
                    IdPesqueiro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fotos = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pesqueiros", x => x.IdPesqueiro);
                });

            migrationBuilder.CreateTable(
                name: "ComprasClientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdCompra = table.Column<int>(type: "int", nullable: false),
                    ClienteIdCliente = table.Column<int>(type: "int", nullable: true),
                    CompraIdCompra = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprasClientes", x => new { x.IdCliente, x.IdCompra });
                    table.ForeignKey(
                        name: "FK_ComprasClientes_Clientes_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_ComprasClientes_Compras_CompraIdCompra",
                        column: x => x.CompraIdCompra,
                        principalTable: "Compras",
                        principalColumn: "IdCompra");
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    IdProduto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProduto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorProduto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtdProduto = table.Column<int>(type: "int", nullable: false),
                    Fornecedor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCompra = table.Column<int>(type: "int", nullable: true),
                    CompraIdCompra = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.IdProduto);
                    table.ForeignKey(
                        name: "FK_Produtos_Compras_CompraIdCompra",
                        column: x => x.CompraIdCompra,
                        principalTable: "Compras",
                        principalColumn: "IdCompra");
                });

            migrationBuilder.CreateTable(
                name: "PeixesCapturados",
                columns: table => new
                {
                    IdPeixeCapturado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Peso = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataPeixeCapturado = table.Column<DateOnly>(type: "date", nullable: false),
                    IdEspecie = table.Column<int>(type: "int", nullable: false),
                    EspecieIdEspecie = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeixesCapturados", x => x.IdPeixeCapturado);
                    table.ForeignKey(
                        name: "FK_PeixesCapturados_Especies_EspecieIdEspecie",
                        column: x => x.EspecieIdEspecie,
                        principalTable: "Especies",
                        principalColumn: "IdEspecie");
                });

            migrationBuilder.CreateTable(
                name: "EspeciesLagos",
                columns: table => new
                {
                    IdEspecie = table.Column<int>(type: "int", nullable: false),
                    IdLago = table.Column<int>(type: "int", nullable: false),
                    EspecieIdEspecie = table.Column<int>(type: "int", nullable: true),
                    LagoIdLago = table.Column<int>(type: "int", nullable: true),
                    QtdPeixes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspeciesLagos", x => new { x.IdEspecie, x.IdLago });
                    table.ForeignKey(
                        name: "FK_EspeciesLagos_Especies_EspecieIdEspecie",
                        column: x => x.EspecieIdEspecie,
                        principalTable: "Especies",
                        principalColumn: "IdEspecie");
                    table.ForeignKey(
                        name: "FK_EspeciesLagos_Lagos_LagoIdLago",
                        column: x => x.LagoIdLago,
                        principalTable: "Lagos",
                        principalColumn: "IdLago");
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    IdComentario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avaliacao = table.Column<int>(type: "int", nullable: false),
                    DataComentario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdPesqueiro = table.Column<int>(type: "int", nullable: false),
                    PesqueiroIdPesqueiro = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.IdComentario);
                    table.ForeignKey(
                        name: "FK_Comentarios_Pesqueiros_PesqueiroIdPesqueiro",
                        column: x => x.PesqueiroIdPesqueiro,
                        principalTable: "Pesqueiros",
                        principalColumn: "IdPesqueiro");
                });

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    IdPesqueiro = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    PesqueiroIdPesqueiro = table.Column<int>(type: "int", nullable: true),
                    ClienteIdCliente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => new { x.IdPesqueiro, x.IdCliente });
                    table.ForeignKey(
                        name: "FK_Favoritos_Clientes_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_Favoritos_Pesqueiros_PesqueiroIdPesqueiro",
                        column: x => x.PesqueiroIdPesqueiro,
                        principalTable: "Pesqueiros",
                        principalColumn: "IdPesqueiro");
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    IdFuncionario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeFuncionario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenhaFuncionario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPesqueiro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.IdFuncionario);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Pesqueiros_IdPesqueiro",
                        column: x => x.IdPesqueiro,
                        principalTable: "Pesqueiros",
                        principalColumn: "IdPesqueiro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeixesClientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdPeixeCapturado = table.Column<int>(type: "int", nullable: false),
                    ClienteIdCliente = table.Column<int>(type: "int", nullable: true),
                    PeixeCapturadoIdPeixeCapturado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeixesClientes", x => new { x.IdCliente, x.IdPeixeCapturado });
                    table.ForeignKey(
                        name: "FK_PeixesClientes_Clientes_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_PeixesClientes_PeixesCapturados_PeixeCapturadoIdPeixeCapturado",
                        column: x => x.PeixeCapturadoIdPeixeCapturado,
                        principalTable: "PeixesCapturados",
                        principalColumn: "IdPeixeCapturado");
                });

            migrationBuilder.CreateTable(
                name: "ClientesComentarios",
                columns: table => new
                {
                    IdComentario = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    ComentarioIdComentario = table.Column<int>(type: "int", nullable: true),
                    ClienteIdCliente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesComentarios", x => new { x.IdComentario, x.IdCliente });
                    table.ForeignKey(
                        name: "FK_ClientesComentarios_Clientes_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_ClientesComentarios_Comentarios_ComentarioIdComentario",
                        column: x => x.ComentarioIdComentario,
                        principalTable: "Comentarios",
                        principalColumn: "IdComentario");
                });

            migrationBuilder.CreateTable(
                name: "Alugueis",
                columns: table => new
                {
                    IdAluguel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorAluguel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataHoraRetirada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataHoraDevolucao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    IdEquipamento = table.Column<int>(type: "int", nullable: false),
                    EquipamentoIdEquipamentos = table.Column<int>(type: "int", nullable: true),
                    IdFuncionario = table.Column<int>(type: "int", nullable: false),
                    FuncionarioIdFuncionario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alugueis", x => x.IdAluguel);
                    table.ForeignKey(
                        name: "FK_Alugueis_Equipamentos_EquipamentoIdEquipamentos",
                        column: x => x.EquipamentoIdEquipamentos,
                        principalTable: "Equipamentos",
                        principalColumn: "IdEquipamentos");
                    table.ForeignKey(
                        name: "FK_Alugueis_Funcionarios_FuncionarioIdFuncionario",
                        column: x => x.FuncionarioIdFuncionario,
                        principalTable: "Funcionarios",
                        principalColumn: "IdFuncionario");
                });

            migrationBuilder.CreateTable(
                name: "Gerencias",
                columns: table => new
                {
                    IdFuncionario = table.Column<int>(type: "int", nullable: false),
                    IdEquipamentos = table.Column<int>(type: "int", nullable: false),
                    FuncionarioIdFuncionario = table.Column<int>(type: "int", nullable: true),
                    EquipamentoIdEquipamentos = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerencias", x => new { x.IdFuncionario, x.IdEquipamentos });
                    table.ForeignKey(
                        name: "FK_Gerencias_Equipamentos_EquipamentoIdEquipamentos",
                        column: x => x.EquipamentoIdEquipamentos,
                        principalTable: "Equipamentos",
                        principalColumn: "IdEquipamentos");
                    table.ForeignKey(
                        name: "FK_Gerencias_Funcionarios_FuncionarioIdFuncionario",
                        column: x => x.FuncionarioIdFuncionario,
                        principalTable: "Funcionarios",
                        principalColumn: "IdFuncionario");
                });

            migrationBuilder.CreateTable(
                name: "AlugueisClientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdAluguel = table.Column<int>(type: "int", nullable: false),
                    ClienteIdCliente = table.Column<int>(type: "int", nullable: true),
                    AluguelIdAluguel = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlugueisClientes", x => new { x.IdCliente, x.IdAluguel });
                    table.ForeignKey(
                        name: "FK_AlugueisClientes_Alugueis_AluguelIdAluguel",
                        column: x => x.AluguelIdAluguel,
                        principalTable: "Alugueis",
                        principalColumn: "IdAluguel");
                    table.ForeignKey(
                        name: "FK_AlugueisClientes_Clientes_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_EquipamentoIdEquipamentos",
                table: "Alugueis",
                column: "EquipamentoIdEquipamentos");

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_FuncionarioIdFuncionario",
                table: "Alugueis",
                column: "FuncionarioIdFuncionario");

            migrationBuilder.CreateIndex(
                name: "IX_AlugueisClientes_AluguelIdAluguel",
                table: "AlugueisClientes",
                column: "AluguelIdAluguel");

            migrationBuilder.CreateIndex(
                name: "IX_AlugueisClientes_ClienteIdCliente",
                table: "AlugueisClientes",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_ClientesComentarios_ClienteIdCliente",
                table: "ClientesComentarios",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_ClientesComentarios_ComentarioIdComentario",
                table: "ClientesComentarios",
                column: "ComentarioIdComentario");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_PesqueiroIdPesqueiro",
                table: "Comentarios",
                column: "PesqueiroIdPesqueiro");

            migrationBuilder.CreateIndex(
                name: "IX_ComprasClientes_ClienteIdCliente",
                table: "ComprasClientes",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_ComprasClientes_CompraIdCompra",
                table: "ComprasClientes",
                column: "CompraIdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_EspeciesLagos_EspecieIdEspecie",
                table: "EspeciesLagos",
                column: "EspecieIdEspecie");

            migrationBuilder.CreateIndex(
                name: "IX_EspeciesLagos_LagoIdLago",
                table: "EspeciesLagos",
                column: "LagoIdLago");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_ClienteIdCliente",
                table: "Favoritos",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_PesqueiroIdPesqueiro",
                table: "Favoritos",
                column: "PesqueiroIdPesqueiro");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_IdPesqueiro",
                table: "Funcionarios",
                column: "IdPesqueiro");

            migrationBuilder.CreateIndex(
                name: "IX_Gerencias_EquipamentoIdEquipamentos",
                table: "Gerencias",
                column: "EquipamentoIdEquipamentos");

            migrationBuilder.CreateIndex(
                name: "IX_Gerencias_FuncionarioIdFuncionario",
                table: "Gerencias",
                column: "FuncionarioIdFuncionario");

            migrationBuilder.CreateIndex(
                name: "IX_PeixesCapturados_EspecieIdEspecie",
                table: "PeixesCapturados",
                column: "EspecieIdEspecie");

            migrationBuilder.CreateIndex(
                name: "IX_PeixesClientes_ClienteIdCliente",
                table: "PeixesClientes",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_PeixesClientes_PeixeCapturadoIdPeixeCapturado",
                table: "PeixesClientes",
                column: "PeixeCapturadoIdPeixeCapturado");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CompraIdCompra",
                table: "Produtos",
                column: "CompraIdCompra");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlugueisClientes");

            migrationBuilder.DropTable(
                name: "ClientesComentarios");

            migrationBuilder.DropTable(
                name: "ComprasClientes");

            migrationBuilder.DropTable(
                name: "EspeciesLagos");

            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DropTable(
                name: "Gerencias");

            migrationBuilder.DropTable(
                name: "PeixesClientes");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Alugueis");

            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Lagos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "PeixesCapturados");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Equipamentos");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Especies");

            migrationBuilder.DropTable(
                name: "Pesqueiros");
        }
    }
}
