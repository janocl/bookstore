
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/26/2015 19:06:07
-- Generated from EDMX file: C:\github\BookStore\DAL\ModeloBookStore.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BookStore];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Libros_Editoriales]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Libros] DROP CONSTRAINT [FK_Libros_Editoriales];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Editoriales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Editoriales];
GO
IF OBJECT_ID(N'[dbo].[Libros]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Libros];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Editoriales'
CREATE TABLE [dbo].[Editoriales] (
    [IDEditorial] smallint IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(50)  NULL,
    [Direccion] varchar(200)  NULL,
    [Telefono] varchar(50)  NULL,
    [Web] varchar(100)  NULL,
    [E_mail] varchar(50)  NULL
);
GO

-- Creating table 'Libros'
CREATE TABLE [dbo].[Libros] (
    [IDLibro] smallint IDENTITY(1,1) NOT NULL,
    [ISBN] char(17)  NOT NULL,
    [Titulo] varchar(50)  NOT NULL,
    [Autor] varchar(100)  NOT NULL,
    [IDEditorial] smallint  NOT NULL,
    [Paginas] smallint  NOT NULL,
    [Precio] decimal(19,4)  NOT NULL,
    [Imagen] varbinary(max)  NULL,
    [Publicacion] datetime  NOT NULL,
    [Descripcion] varchar(max)  NULL,
    [Stock] smallint  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IDEditorial] in table 'Editoriales'
ALTER TABLE [dbo].[Editoriales]
ADD CONSTRAINT [PK_Editoriales]
    PRIMARY KEY CLUSTERED ([IDEditorial] ASC);
GO

-- Creating primary key on [IDLibro] in table 'Libros'
ALTER TABLE [dbo].[Libros]
ADD CONSTRAINT [PK_Libros]
    PRIMARY KEY CLUSTERED ([IDLibro] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IDEditorial] in table 'Libros'
ALTER TABLE [dbo].[Libros]
ADD CONSTRAINT [FK_Libros_Editoriales]
    FOREIGN KEY ([IDEditorial])
    REFERENCES [dbo].[Editoriales]
        ([IDEditorial])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Libros_Editoriales'
CREATE INDEX [IX_FK_Libros_Editoriales]
ON [dbo].[Libros]
    ([IDEditorial]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------