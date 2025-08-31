IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Clientes] (
    [IdCliente] int NOT NULL IDENTITY,
    [NomeCliente] nvarchar(max) NULL,
    [SenhaCliente] nvarchar(max) NULL,
    [EmailCliente] nvarchar(max) NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([IdCliente])
);
GO

CREATE TABLE [Compras] (
    [IdCompra] int NOT NULL IDENTITY,
    [DtCompra] date NOT NULL,
    [ValorTotal] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Compras] PRIMARY KEY ([IdCompra])
);
GO

CREATE TABLE [Equipamentos] (
    [IdEquipamentos] int NOT NULL IDENTITY,
    [NomeEquipamento] nvarchar(max) NULL,
    [EquipamentoEmUso] bit NOT NULL,
    [QuantidadeEquipamento] int NOT NULL,
    CONSTRAINT [PK_Equipamentos] PRIMARY KEY ([IdEquipamentos])
);
GO

CREATE TABLE [Especies] (
    [IdEspecie] int NOT NULL IDENTITY,
    [NomeEspecie] nvarchar(max) NULL,
    [ValorEspecie] decimal(18,2) NOT NULL,
    [FornecedorEspecie] nvarchar(max) NULL,
    CONSTRAINT [PK_Especies] PRIMARY KEY ([IdEspecie])
);
GO

CREATE TABLE [Funcionarios] (
    [IdFuncionario] int NOT NULL IDENTITY,
    [NomeFuncionario] nvarchar(max) NULL,
    [SenhaFuncionario] nvarchar(max) NULL,
    CONSTRAINT [PK_Funcionarios] PRIMARY KEY ([IdFuncionario])
);
GO

CREATE TABLE [Lagos] (
    [IdLago] int NOT NULL IDENTITY,
    [NomeLago] nvarchar(max) NULL,
    CONSTRAINT [PK_Lagos] PRIMARY KEY ([IdLago])
);
GO

CREATE TABLE [Pesqueiros] (
    [IdPesqueiro] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Telefone] nvarchar(max) NULL,
    [Latitude] nvarchar(max) NULL,
    [Longitude] nvarchar(max) NULL,
    [Fotos] nvarchar(max) NULL,
    CONSTRAINT [PK_Pesqueiros] PRIMARY KEY ([IdPesqueiro])
);
GO

CREATE TABLE [ComprasClientes] (
    [IdCliente] int NOT NULL,
    [IdCompra] int NOT NULL,
    [ClienteIdCliente] int NULL,
    [CompraIdCompra] int NULL,
    CONSTRAINT [PK_ComprasClientes] PRIMARY KEY ([IdCliente], [IdCompra]),
    CONSTRAINT [FK_ComprasClientes_Clientes_ClienteIdCliente] FOREIGN KEY ([ClienteIdCliente]) REFERENCES [Clientes] ([IdCliente]),
    CONSTRAINT [FK_ComprasClientes_Compras_CompraIdCompra] FOREIGN KEY ([CompraIdCompra]) REFERENCES [Compras] ([IdCompra])
);
GO

CREATE TABLE [Produtos] (
    [IdProduto] int NOT NULL IDENTITY,
    [NomeProduto] nvarchar(max) NULL,
    [ValorProduto] decimal(18,2) NOT NULL,
    [QtdProduto] int NOT NULL,
    [Fornecedor] nvarchar(max) NULL,
    [IdCompra] int NULL,
    [CompraIdCompra] int NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([IdProduto]),
    CONSTRAINT [FK_Produtos_Compras_CompraIdCompra] FOREIGN KEY ([CompraIdCompra]) REFERENCES [Compras] ([IdCompra])
);
GO

CREATE TABLE [Alugueis] (
    [IdAluguel] int NOT NULL IDENTITY,
    [ValorAluguel] decimal(18,2) NOT NULL,
    [DataHoraRetirada] datetime2 NOT NULL,
    [DataHoraDevolucao] datetime2 NULL,
    [Observacao] nvarchar(max) NULL,
    [IdEquipamentos] int NOT NULL,
    [EquipamentoIdEquipamentos] int NULL,
    CONSTRAINT [PK_Alugueis] PRIMARY KEY ([IdAluguel]),
    CONSTRAINT [FK_Alugueis_Equipamentos_EquipamentoIdEquipamentos] FOREIGN KEY ([EquipamentoIdEquipamentos]) REFERENCES [Equipamentos] ([IdEquipamentos])
);
GO

CREATE TABLE [PeixesCapturados] (
    [IdPeixeCapturado] int NOT NULL IDENTITY,
    [Peso] decimal(18,2) NOT NULL,
    [DataPeixeCapturado] date NOT NULL,
    [IdEspecie] int NOT NULL,
    [EspecieIdEspecie] int NULL,
    CONSTRAINT [PK_PeixesCapturados] PRIMARY KEY ([IdPeixeCapturado]),
    CONSTRAINT [FK_PeixesCapturados_Especies_EspecieIdEspecie] FOREIGN KEY ([EspecieIdEspecie]) REFERENCES [Especies] ([IdEspecie])
);
GO

CREATE TABLE [Gerencias] (
    [IdFuncionario] int NOT NULL,
    [IdEquipamentos] int NOT NULL,
    [FuncionarioIdFuncionario] int NULL,
    [EquipamentoIdEquipamentos] int NULL,
    CONSTRAINT [PK_Gerencias] PRIMARY KEY ([IdFuncionario], [IdEquipamentos]),
    CONSTRAINT [FK_Gerencias_Equipamentos_EquipamentoIdEquipamentos] FOREIGN KEY ([EquipamentoIdEquipamentos]) REFERENCES [Equipamentos] ([IdEquipamentos]),
    CONSTRAINT [FK_Gerencias_Funcionarios_FuncionarioIdFuncionario] FOREIGN KEY ([FuncionarioIdFuncionario]) REFERENCES [Funcionarios] ([IdFuncionario])
);
GO

CREATE TABLE [EspeciesLagos] (
    [IdEspecie] int NOT NULL,
    [IdLago] int NOT NULL,
    [EspecieIdEspecie] int NULL,
    [LagoIdLago] int NULL,
    [QtdPeixes] int NOT NULL,
    CONSTRAINT [PK_EspeciesLagos] PRIMARY KEY ([IdEspecie], [IdLago]),
    CONSTRAINT [FK_EspeciesLagos_Especies_EspecieIdEspecie] FOREIGN KEY ([EspecieIdEspecie]) REFERENCES [Especies] ([IdEspecie]),
    CONSTRAINT [FK_EspeciesLagos_Lagos_LagoIdLago] FOREIGN KEY ([LagoIdLago]) REFERENCES [Lagos] ([IdLago])
);
GO

CREATE TABLE [Comentarios] (
    [IdComentario] int NOT NULL IDENTITY,
    [Texto] nvarchar(max) NULL,
    [Avaliacao] int NOT NULL,
    [IdPesqueiro] int NOT NULL,
    [PesqueiroIdPesqueiro] int NULL,
    CONSTRAINT [PK_Comentarios] PRIMARY KEY ([IdComentario]),
    CONSTRAINT [FK_Comentarios_Pesqueiros_PesqueiroIdPesqueiro] FOREIGN KEY ([PesqueiroIdPesqueiro]) REFERENCES [Pesqueiros] ([IdPesqueiro])
);
GO

CREATE TABLE [Favoritos] (
    [IdPesqueiro] int NOT NULL,
    [IdCliente] int NOT NULL,
    [PesqueiroIdPesqueiro] int NULL,
    [ClienteIdCliente] int NULL,
    CONSTRAINT [PK_Favoritos] PRIMARY KEY ([IdPesqueiro], [IdCliente]),
    CONSTRAINT [FK_Favoritos_Clientes_ClienteIdCliente] FOREIGN KEY ([ClienteIdCliente]) REFERENCES [Clientes] ([IdCliente]),
    CONSTRAINT [FK_Favoritos_Pesqueiros_PesqueiroIdPesqueiro] FOREIGN KEY ([PesqueiroIdPesqueiro]) REFERENCES [Pesqueiros] ([IdPesqueiro])
);
GO

CREATE TABLE [AlugueisClientes] (
    [IdCliente] int NOT NULL,
    [IdAluguel] int NOT NULL,
    [ClienteIdCliente] int NULL,
    [AluguelIdAluguel] int NULL,
    CONSTRAINT [PK_AlugueisClientes] PRIMARY KEY ([IdCliente], [IdAluguel]),
    CONSTRAINT [FK_AlugueisClientes_Alugueis_AluguelIdAluguel] FOREIGN KEY ([AluguelIdAluguel]) REFERENCES [Alugueis] ([IdAluguel]),
    CONSTRAINT [FK_AlugueisClientes_Clientes_ClienteIdCliente] FOREIGN KEY ([ClienteIdCliente]) REFERENCES [Clientes] ([IdCliente])
);
GO

CREATE TABLE [PeixesClientes] (
    [IdCliente] int NOT NULL,
    [IdPeixeCapturado] int NOT NULL,
    [ClienteIdCliente] int NULL,
    [PeixeCapturadoIdPeixeCapturado] int NULL,
    CONSTRAINT [PK_PeixesClientes] PRIMARY KEY ([IdCliente], [IdPeixeCapturado]),
    CONSTRAINT [FK_PeixesClientes_Clientes_ClienteIdCliente] FOREIGN KEY ([ClienteIdCliente]) REFERENCES [Clientes] ([IdCliente]),
    CONSTRAINT [FK_PeixesClientes_PeixesCapturados_PeixeCapturadoIdPeixeCapturado] FOREIGN KEY ([PeixeCapturadoIdPeixeCapturado]) REFERENCES [PeixesCapturados] ([IdPeixeCapturado])
);
GO

CREATE TABLE [ClientesComentarios] (
    [IdComentario] int NOT NULL,
    [IdCliente] int NOT NULL,
    [ComentarioIdComentario] int NULL,
    [ClienteIdCliente] int NULL,
    CONSTRAINT [PK_ClientesComentarios] PRIMARY KEY ([IdComentario], [IdCliente]),
    CONSTRAINT [FK_ClientesComentarios_Clientes_ClienteIdCliente] FOREIGN KEY ([ClienteIdCliente]) REFERENCES [Clientes] ([IdCliente]),
    CONSTRAINT [FK_ClientesComentarios_Comentarios_ComentarioIdComentario] FOREIGN KEY ([ComentarioIdComentario]) REFERENCES [Comentarios] ([IdComentario])
);
GO

CREATE INDEX [IX_Alugueis_EquipamentoIdEquipamentos] ON [Alugueis] ([EquipamentoIdEquipamentos]);
GO

CREATE INDEX [IX_AlugueisClientes_AluguelIdAluguel] ON [AlugueisClientes] ([AluguelIdAluguel]);
GO

CREATE INDEX [IX_AlugueisClientes_ClienteIdCliente] ON [AlugueisClientes] ([ClienteIdCliente]);
GO

CREATE INDEX [IX_ClientesComentarios_ClienteIdCliente] ON [ClientesComentarios] ([ClienteIdCliente]);
GO

CREATE INDEX [IX_ClientesComentarios_ComentarioIdComentario] ON [ClientesComentarios] ([ComentarioIdComentario]);
GO

CREATE INDEX [IX_Comentarios_PesqueiroIdPesqueiro] ON [Comentarios] ([PesqueiroIdPesqueiro]);
GO

CREATE INDEX [IX_ComprasClientes_ClienteIdCliente] ON [ComprasClientes] ([ClienteIdCliente]);
GO

CREATE INDEX [IX_ComprasClientes_CompraIdCompra] ON [ComprasClientes] ([CompraIdCompra]);
GO

CREATE INDEX [IX_EspeciesLagos_EspecieIdEspecie] ON [EspeciesLagos] ([EspecieIdEspecie]);
GO

CREATE INDEX [IX_EspeciesLagos_LagoIdLago] ON [EspeciesLagos] ([LagoIdLago]);
GO

CREATE INDEX [IX_Favoritos_ClienteIdCliente] ON [Favoritos] ([ClienteIdCliente]);
GO

CREATE INDEX [IX_Favoritos_PesqueiroIdPesqueiro] ON [Favoritos] ([PesqueiroIdPesqueiro]);
GO

CREATE INDEX [IX_Gerencias_EquipamentoIdEquipamentos] ON [Gerencias] ([EquipamentoIdEquipamentos]);
GO

CREATE INDEX [IX_Gerencias_FuncionarioIdFuncionario] ON [Gerencias] ([FuncionarioIdFuncionario]);
GO

CREATE INDEX [IX_PeixesCapturados_EspecieIdEspecie] ON [PeixesCapturados] ([EspecieIdEspecie]);
GO

CREATE INDEX [IX_PeixesClientes_ClienteIdCliente] ON [PeixesClientes] ([ClienteIdCliente]);
GO

CREATE INDEX [IX_PeixesClientes_PeixeCapturadoIdPeixeCapturado] ON [PeixesClientes] ([PeixeCapturadoIdPeixeCapturado]);
GO

CREATE INDEX [IX_Produtos_CompraIdCompra] ON [Produtos] ([CompraIdCompra]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250831220012_InitialCreate', N'8.0.6');
GO

COMMIT;
GO

