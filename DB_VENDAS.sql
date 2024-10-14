CREATE DATABASE GestaoVendas;

USE GestaoVendas;

-- Tabela Produto
CREATE TABLE Produto (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100),
    Preco DECIMAL(18, 2),
    Quantidade INT
);

-- Tabela Venda
CREATE TABLE Venda (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProdutoId INT FOREIGN KEY REFERENCES Produto(Id),
    Data DATETIME,
    QuantidadeVendida INT,
    ValorTotal DECIMAL(18, 2)
);

-- Stored Procedure para cadastrar Produto
CREATE PROCEDURE sp_InserirProduto
    @Nome NVARCHAR(100),
    @Preco DECIMAL(18,2),
    @Quantidade INT
AS
BEGIN
    INSERT INTO Produto (Nome, Preco, Quantidade) 
    VALUES (@Nome, @Preco, @Quantidade);
END;

-- Stored Procedure para registrar Venda
CREATE PROCEDURE sp_RegistrarVenda
    @ProdutoId INT,
    @QuantidadeVendida INT,
    @Data DATETIME
AS
BEGIN
    DECLARE @Preco DECIMAL(18,2);
    SET @Preco = (SELECT Preco FROM Produto WHERE Id = @ProdutoId);
    DECLARE @ValorTotal DECIMAL(18,2) = @QuantidadeVendida * @Preco;
    
    INSERT INTO Venda (ProdutoId, Data, QuantidadeVendida, ValorTotal) 
    VALUES (@ProdutoId, @Data, @QuantidadeVendida, @ValorTotal);
    
    -- Atualiza estoque
    UPDATE Produto 
    SET Quantidade = Quantidade - @QuantidadeVendida 
    WHERE Id = @ProdutoId;
END;

-- Stored Procedure para relatório de Vendas
CREATE PROCEDURE sp_RelatorioVendas
AS
BEGIN
    SELECT * FROM Venda;
END;

-- Stored Procedure para relatório de Estoque
CREATE PROCEDURE sp_RelatorioEstoque
AS
BEGIN
    SELECT * FROM Produto;
END;
